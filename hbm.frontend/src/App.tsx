import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import userManager, { loadUser, signinRedirect, signoutRedirect } from './auth/user-service';
import AuthProvider from './auth/auth-provider';
import SignInOidc from './auth/SigninOidc';
import SignOutOidc from './auth/SignoutOidc';
import PostList from './posts/PostList';
import logoImg from './images/logo.png';
import { Component } from 'react';

class App extends Component<any, any> {
    constructor(props: any) {
        super(props);
        this.handleLoginClick = this.handleLoginClick.bind(this);
        this.handleLogoutClick = this.handleLogoutClick.bind(this);
        this.state = {
            isLoggedIn: localStorage.getItem('isLoggedIn') ? localStorage.getItem('isLoggedIn') === 'true' : false,
            role: localStorage.getItem('user_role') ? localStorage.getItem('user_role') : ''
        };
    }
    
    handleLoginClick() {
        localStorage.setItem('isLoggedIn', 'true');
        signinRedirect();
    }
    
    handleLogoutClick() {
        localStorage.setItem('isLoggedIn', 'false');
        localStorage.setItem('user_role', '');
        signoutRedirect({'id_token_hint': localStorage.getItem('id_token')});
    }
    render()
    {
        loadUser();

        const isLoggedIn = this.state.isLoggedIn;
        const role = this.state.role;
        let button;
        if (isLoggedIn) {
            button = <LogoutButton onClick={this.handleLogoutClick} />;
        } else {
            button = <LoginButton onClick={this.handleLoginClick} />;
        }
        return (
            <div className="App">
                <header className="App-header">
                    
                    <div className="header__block">
                            
                        <img className="icon" src={logoImg}/>
                        
                        <Greeting role={role} />
                                
                        <div className="header__right_part">
                            <input type="search" className="header__search"/>
                            <ul className="header__auth_panel">
                                {button}
                            </ul>
                        </div>
                    </div>
                </header>
                    
                <div className="page">
                    <div className="main__block __container">
    
                        <AuthProvider userManager={userManager}>
                            <Router>
                                <Routes>
                                    <Route path="/" element={<PostList/>} />
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

function LoginButton(props: any) {
    return (
    <button className="auth_button" onClick={props.onClick}>
        Войти
    </button>
    );
}
  
function LogoutButton(props: any) {
    return (
    <button className="auth_button" onClick={props.onClick}>
        Выйти
    </button>
    );
}

function Greeting(props: any) {
    const role = props.role;
    if (role === "Owner" ||
        role === "Admin") {
        return <AdminGreeting />;
    }
    return <UserGreeting />;
}

function UserGreeting(props: any) {
    return(
    <ul className="header__buttons_panel">
        <a href="/"><li className="menu__item">Новое</li></a>
        <a><li className="menu__item">|</li></a>
        <a href="/"><li className="menu__item">Популярное</li></a>
        <a><li className="menu__item">|</li></a>
        <a href="/"><li className="menu__item">Лучшее</li></a>
    </ul>)
}
  
function AdminGreeting(props: any) {
    return(
    <ul className="header__buttons_panel">
        <a href="/"><li className="menu__item">Новое</li></a>
        <a><li className="menu__item">|</li></a>
        <a href="/"><li className="menu__item">Популярное</li></a>
        <a><li className="menu__item">|</li></a>
        <a href="/"><li className="menu__item">Лучшее</li></a>
        <a><li className="menu__item">|</li></a>
        <a href="/"><li className="menu__item">Новый пост</li></a>
    </ul>)
}