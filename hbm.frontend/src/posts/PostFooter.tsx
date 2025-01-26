import { useState } from 'react';
import CommentList from '../comments/CommentList';
import commentsImg from '../images/comments.png';
import Reactions from '../reactions/reactions';

function PostFooter(props: any) {
    const [commentsCount, setCommentsCount] = useState('');

    return (
        <>
            <div className="post__footer">
                <div className="post__footer_left">
                    <Reactions postId={props.postId}/>
                </div>
                                
                <div className="post__footer_left">
                    <img className="post__footer_icon" src={commentsImg}/>
                    <div className="post__footer-text">{commentsCount !== '0' ? commentsCount : ''}</div>
                </div>
            </div>

            <CommentList postId={props.postId} setCommentsCount={setCommentsCount}/>
        </>
    );
};
export default PostFooter;