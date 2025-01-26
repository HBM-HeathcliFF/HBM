import { useEffect, useState } from 'react';
import { Client, PostLookupDto } from '../api/api'; 
import PostFooter from './PostFooter';

const apiClient = new Client('https://localhost:44327');

function PostList() {
    const [posts, setPosts] = useState<PostLookupDto[] | undefined>(undefined);

    useEffect(() => {
        async function getPosts() {
            const postListVm = await apiClient.getAllPosts('1.0');
            setPosts(postListVm.posts);
        }

        getPosts();
    }, []);

    return (
        <section>
            {posts?.map((post) => (
                <div className="post" key={post.id}>
                
                    <ul>
                        <a href="/"><li className="post__title">{post.title}</li></a>
                    </ul>
                            
                    <div className="post-text">{post.details}</div>
                            
                    <PostFooter postId={post.id}/>
                          
                </div>
            ))}
        </section>
    );
};
export default PostList;