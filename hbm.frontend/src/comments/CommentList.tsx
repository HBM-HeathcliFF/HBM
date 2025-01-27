import { useContext, useEffect, useState } from 'react';
import { Client, CommentLookupDto, CreateCommentDto } from '../api/api';
import SendComment from '../images/send_comment.png';
import { AuthContext } from '../auth/auth-provider';

const apiClient = new Client('https://localhost:44327');

function CommentList(props: any) {
    const [comments, setComments] = useState<CommentLookupDto[] | undefined>(undefined);
    const [inputText, setInputText] = useState('');

    const { isAuthenticated } = useContext(AuthContext);
    
    async function CreateComment() {
        setInputText('');
        const createCommentDto: CreateCommentDto = {
            postId: props.postId,
            text: inputText
        };
        const response = await apiClient.createComment('1.0', createCommentDto);
        getComments();
    }

    async function getComments() {
        const commentListVm = await apiClient.getAllComments(props.postId, '1.0');
        setComments(commentListVm.comments);
        props.setCommentsCount(commentListVm.comments?.length.toString());
    }

    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            CreateComment();
        }
    };

    useEffect(() => {
        getComments();
    }, []);

    let isFirst = true;

    return (
        <>
            {comments?.length! > 0
            ? 
            <div className="comments__block">

                {comments?.map((comment) => (
                    isFirst
                    ?
                    <div className="comment" key={comment.id}>
                        <div className="comment_username">{comment.userName}</div>
                        <div className="comment-text">{comment.text}</div>
                        <div className="comment_date">{comment.creationDate}</div>
                    </div>
                    :
                    <>
                        <div className="comment_splitter"></div>
                        <div className="comment" key={comment.id}>
                            <div className="comment_username">{comment.userName}</div>
                            <div className="comment-text">{comment.text}</div>
                            <div className="comment_date">{comment.creationDate}</div>
                        </div>
                    </>
                    
                ))}
                {
                    isAuthenticated
                    ?
                    <div className="comment_input_block">
                        <input
                        type='text'
                        name='title'
                        className='comment_input'
                        placeholder='Написать комментарий...'
                        value={inputText}
                        onChange={(event) => setInputText(event.target.value)}
                        onKeyDown={handleKeyDown}
                    />
                        <input type="image" className="send_comment_button" src={SendComment} onClick={CreateComment}/>
                    </div>
                    :
                    <></>
                }
                
            </div>
            : 
            <></>}
            
        </>
    );
};
export default CommentList;