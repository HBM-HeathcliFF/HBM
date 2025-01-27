import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SignInOidc from './auth/SigninOidc';
import SignOutOidc from './auth/SignoutOidc';
import Header from './header/header'
import PostList from './posts/PostList';
import CreatePost from './posts/CreatePost';
import PrivateRoute from './routes/private-route'
import Post from './posts/Post';

export default function App() {
    return (
        <Router>
            <div className="App">

                <Header/>
    
                <div className="page">
                    <div className="main__block __container">
                        <Routes>
                            <Route element={<PrivateRoute/>}>
                                <Route path='/create-post' element={<CreatePost/>}/>
                            </Route>
                            <Route path='/' element={<PostList/>}/>
                            <Route path='/new' element={<PostList/>}/>
                            <Route path='/popular' element={<PostList/>}/>
                            <Route path='/the-best' element={<PostList/>}/>
                            <Route path='/posts/:id' element={<Post/>}/>
                            <Route path='/signout-oidc' element={<SignOutOidc/>}/>
                            <Route path='/signin-oidc' element={<SignInOidc/>} />
                        </Routes>
                    </div>
                </div>

            </div>
        </Router>
    );
}