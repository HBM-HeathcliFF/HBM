import { useContext, useEffect, useState } from 'react';
import { Client, PostLookupDto } from '../api/api'; 
import PostFooter from './PostFooter';
import { useLocation } from 'react-router-dom';
import { SearchContext } from '../header/search-provider';

const apiClient = new Client('https://localhost:44327');

function PostList() {
    const [posts, setPosts] = useState<PostLookupDto[] | undefined>(undefined);

    const { text } = useContext(SearchContext);
    const location = useLocation();

    useEffect(() => {
        if (text !== '') {
            var filteredList = posts?.filter(function(post) {
                return post.title?.toLowerCase().search(text.toLowerCase()) !== -1 || post.details?.toLowerCase().search(text.toLowerCase()) !== -1;
            }); 
            setPosts(filteredList);
        } else {
            getPosts();
        }
    }, [text]);

    async function getPosts() {
        const postListVm = await apiClient.getAllPosts('1.0');

        let sorted = postListVm.posts;
        
        if (postListVm.posts !== undefined) {
            if (location.pathname === '/popular') {
                sorted = postListVm.posts.sort((a, b) => a.commentsCount! < b.commentsCount! ? 1 : -1);
            } else if (location.pathname === '/the-best') {
                sorted = postListVm.posts.sort((a, b) => a.reactionsCount! < b.reactionsCount! ? 1 : -1);
            }
        }

        setPosts(postListVm.posts);
    }

    useEffect(() => {
        getPosts();
    }, []);

    return (
        <>
            {posts?.map((post) => (
                <div className="post" key={post.id}>
                
                    <ul>
                        <a href={`/posts/${post.id}`}><li className="post__title">{post.title}</li></a>
                    </ul>
                    <div className="post_date">{post.creationDate}</div>
                            
                    <div className="post-text">{post.details}</div>
                            
                    <PostFooter postId={post.id}/>
                          
                </div>
            ))}
        </>
    );
};
export default PostList;