import { FC, ReactElement, useEffect, useState } from 'react';
import { Client, PostLookupDto } from '../api/api';
import likesImg from '../images/likes.png';
import commentsImg from '../images/comments.png';
import viewsImg from '../images/views.png';

const apiClient = new Client('https://localhost:44327');

const PostList: FC<{}> = (): ReactElement => {
    const [posts, setPosts] = useState<PostLookupDto[] | undefined>(undefined);

    async function getPosts() {
        if (localStorage.getItem('token') !== '') {
            const postListVm = await apiClient.getAll2('1.0');
            setPosts(postListVm.posts);
        }
    }

    useEffect(() => {
        setTimeout(getPosts, 500);
    }, []);

    return (
        <section>
            {posts?.map((post) => (
                <div className="post">
                
                    <ul>
                        <a href="/"><li className="post__title">{post.title}</li></a>
                    </ul>
                            
                    <div className="post-text">{post.details}</div>
                            
                    <div className="post__footer">
                        <div className="post__footer_left">
                            <img className="post__footer_icon" src={likesImg}/>
                            <div className="post__footer-text">7</div>
                        </div>
                                
                        <div className="post__footer_left">
                            <img className="post__footer_icon" src={commentsImg}/>
                            <div className="post__footer-text">1</div>
                        </div>

                        <div className="post__footer_right">
                            <img className="post__footer_icon" src={viewsImg}/>
                            <div className="post__footer-text">398</div>
                        </div>
                    </div>
                          
                </div>
            ))}
        </section>
    );
};
export default PostList;