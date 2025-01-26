import { useEffect, useState } from 'react';
import { Client, CreateReactionDto, ReactionLookupDto } from '../api/api';
import likesImg from '../images/likes.png';
import likePressedImg from '../images/like_pressed.png';

const apiClient = new Client('https://localhost:44327');

function Reactions(props: any) {
    const [reactions, setReactions] = useState<ReactionLookupDto[] | undefined>(undefined);
    const [userReaction, setUserReaction] = useState(false);
    
    async function addReaction() {
        const createReactionDto: CreateReactionDto = {
            postId: props.postId
        };
        setUserReaction(true);
        await apiClient.createReaction('1.0', createReactionDto);
        getReactions();
    }

    async function removeReaction() {
        const reaction = reactions?.find(r => r.userId === localStorage.getItem('user_id'));
        if (reaction !== undefined) {
            setUserReaction(false);
            await apiClient.deleteReaction(reaction.id, '1.0');
        }
        getReactions();
    }

    async function getReactions() {
        const reactionListVm = await apiClient.getAllReactions(props.postId, '1.0');
        setReactions(reactionListVm.reactions);
        if (reactionListVm.reactions?.find(r => r.userId === localStorage.getItem('user_id')) !== undefined) {
            setUserReaction(true);
        }
    }

    useEffect(() => {
        getReactions();
    }, []);

    let length = '';
    if (reactions !== undefined && reactions.length > 0) {
        length = reactions.length.toString();
    }

    return (
        <>
            {
                userReaction 
                ?
                <input type="image" className="like_button" src={likePressedImg} onClick={removeReaction}/>
                :
                <input type="image" className="like_button" src={likesImg} onClick={addReaction}/>
            }
            <div className="post__footer-text">{length}</div>
        </>
    );
};
export default Reactions;