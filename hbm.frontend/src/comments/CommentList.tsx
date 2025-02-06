import { useContext, useEffect, useState } from 'react';
import { Client, CommentLookupDto, CreateCommentDto } from '../api/api';
import SendComment from '../images/send_comment.png';
import { AuthContext } from '../auth/auth-provider';
import DeleteButton from '../images/delete.png';

const apiClient = new Client('https://localhost:44327');

function CommentList(props: any) {
    const [comments, setComments] = useState<CommentLookupDto[] | undefined>(undefined);
    const [inputText, setInputText] = useState('');

    let isFirstComment = true;

    const { isAuthenticated, role, name } = useContext(AuthContext);

    async function deleteComment(id: string) {
        const response = await apiClient.deleteComment(id, '1.0');
        getComments();
    }
    
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

    function Comment(props: any) {
        return(
            <div className='comment' key={props.comment.id}>
                <div className='comment_head_container'>
                    <div className='comment_username'>{props.comment.userName}</div>
                    {
                        role === 'Owner' || role === 'Admin' || props.comment.userName === name
                        ?
                        <input type='image' className='comment_delete_button' src={DeleteButton} onClick={() => deleteComment(props.comment.id!)}/>
                        :
                        <></>
                    }
                </div>
                <div className='comment_text'>{props.comment.text}</div>
                <div className='comment_date'>{props.comment.creationDate}</div>
            </div>
        );
    }

    useEffect(() => {
        getComments();
    }, []);

    return (
        <>
            {comments?.length! > 0
            ?
            <>
                <div className='splitter'/>
                <div className='comments_block'>
                    {comments?.map((comment) => (
                        isFirstComment
                        ?
                        <>
                            <Comment comment={comment}/>
                            {isFirstComment = false}
                        </>
                        :
                        <>
                            <div className='comment_splitter'></div>
                            <Comment comment={comment}/>
                        </>
                    ))}
                    {isAuthenticated
                    ?
                    <div className='comment_input_block'>
                        <input
                            type='text'
                            name='title'
                            className='comment_input'
                            placeholder='Написать комментарий...'
                            value={inputText}
                            onChange={(event) => setInputText(event.target.value)}
                            onKeyDown={handleKeyDown}
                        />
                        <input type='image' className='send_comment_button' src={SendComment} onClick={CreateComment}/>
                    </div>
                    :
                    <></>}
                </div>
            </>
            : 
            <>
                {isAuthenticated
                ?
                <div className='comments_block'>
                    <div className='splitter'/>
                    <div className='comment_input_block'>
                        <input
                            type='text'
                            name='title'
                            className='comment_input'
                            placeholder='Написать комментарий...'
                            value={inputText}
                            onChange={(event) => setInputText(event.target.value)}
                            onKeyDown={handleKeyDown}
                        />
                        <input type='image' className='send_comment_button' src={SendComment} onClick={CreateComment}/>
                    </div>
                </div>
                :
                <></>}
            </>}
        </>
        
    );
};
export default CommentList;