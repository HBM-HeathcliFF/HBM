import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SignInOidc from './auth/SigninOidc';
import SignOutOidc from './auth/SignoutOidc';
import Header from './header/header'
import PostList from './posts/PostList';
import CreatePost from './posts/CreatePost';

export default function App() {
    return (
        <Router>
            <div className="App">

                <Header/>
    
                <div className="page">
                    <div className="main__block __container">
                        <Routes>
                            <Route path='/create-post' element={<CreatePost/>}/>
                            <Route path='/' element={<PostList/>}/>
                            <Route path='/signout-oidc' element={<SignOutOidc/>}/>
                            <Route path='/signin-oidc' element={<SignInOidc/>} />
                        </Routes>
                    </div>
                </div>

            </div>
        </Router>
    );
}