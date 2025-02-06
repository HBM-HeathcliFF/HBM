import { useEffect, useState } from 'react';
import { Client, PostVm } from '../api/api'; 
import PostFooter from './PostFooter';
import { useParams } from 'react-router-dom';

const apiClient = new Client('https://localhost:44327');

function Post() {
    const [post, setPost] = useState<PostVm | undefined>(undefined);

    const params = useParams();
    const id = params.id;

    useEffect(() => {
        async function getPost() {
            const postDetailsVm = await apiClient.getPost(id!, '1.0');
            setPost(postDetailsVm);
        }

        getPost();
    }, []);

    return (
        <>
            {
                post !== undefined
                ?
                <div className='post'>
                
                    <ul>
                        <a href={'/posts/${post.id}'}><li className='post_title'>{post.title}</li></a>
                    </ul>
                    <div className='post_date'>{post.creationDate}</div>
                            
                    <div className='post_text'>{post.text}</div>
                            
                    <PostFooter postId={post.id}/>
                          
                </div>
                :
                <></>
            }
        </>
        
    );
};
export default Post;