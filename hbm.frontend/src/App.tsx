import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { Component } from 'react';
import userManager from './auth/user-service';
import AuthProvider from './auth/auth-provider';
import SignInOidc from './auth/SigninOidc';
import SignOutOidc from './auth/SignoutOidc';
import Header from './header/header'
import PostList from './posts/PostList';
import CreatePost from './posts/CreatePost';

class App extends Component<any, any> {
    render()
    {
        return (
            <div className="App">

                <Header/>
                    
                <div className="page">
                    <div className="main__block __container">
    
                        <AuthProvider userManager={userManager}>
                            <Router>
                                <Routes>
                                    <Route path='/create-post' element={<CreatePost/>}/>
                                    <Route path="/" element={<PostList/>}/>
                                    <Route path='/signout-oidc' element={<SignOutOidc/>}/>
                                    <Route path='/signin-oidc' element={<SignInOidc/>} />
                                </Routes>
                            </Router>
                        </AuthProvider>
    
                    </div>
                </div>
    
            </div>
        );
    };
}

export default App;