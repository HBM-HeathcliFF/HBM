import { useState } from 'react';
import { Client, CreatePostDto } from '../api/api';

const apiClient = new Client('https://localhost:44327');

export default function CreatePost() {
    const [inputTitle, setInputTitle] = useState('');
    const [inputDetails, setInputDetails] = useState('');

    async function CreatePost() {
        const createPostDto: CreatePostDto = {
            title: inputTitle,
            details: inputDetails
        };

        await apiClient.createPost('1.0', createPostDto);
    }

    return (
        <section>
            <div className="post">
                    
                <form>
                    <div className="input_block">
                        <div className="post-text">
                            Название:
                        </div>
                        <input
                            type='text'
                            name='title'
                            className='input_title'
                            value={inputTitle}
                            onChange={(event) => setInputTitle(event.target.value)}
                        />
                    </div>

                    <div className="input_block">
                        <div className="post-text">
                            Содержимое:
                        </div>
                        <textarea
                            name='details'
                            value={inputDetails}
                            onChange={(event) => setInputDetails(event.target.value)}
                            className="input_details"
                        ></textarea>
                    </div>
                    
                    <div className="button_panel">
                        <input type="submit" value="Создать" className="create_button" onClick={CreatePost}/>
                    </div>
                </form>

            </div>
        </section>
    );
};