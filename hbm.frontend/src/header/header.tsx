import { FC, ReactElement, useEffect, useState } from 'react';
import { MenuButtons, LoginButton, LogoutButton } from './header-buttons'
import { loadUser, signinRedirect, signoutRedirect } from '../auth/user-service';
import logoImg from '../images/logo.png';
import { setUserName, setUserRole } from '../auth/auth-headers';

const Header: FC<{}> = (): ReactElement => {
    const [role, setRole] = useState('');
    const [name, setName] = useState('');
    const [isLoggedIn, setAuthStatus] = useState(
        localStorage.getItem('isLoggedIn') ? localStorage.getItem('isLoggedIn') === 'true' : false
    );

    function handleLoginClick() {
        localStorage.setItem('isLoggedIn', 'true');
        setAuthStatus(true);
        signinRedirect();
    }

    function handleLogoutClick() {
        localStorage.setItem('isLoggedIn', 'false');
        setAuthStatus(false);
        signoutRedirect({'id_token_hint': localStorage.getItem('id_token')});
    }

    function getUserData() {
        loadUser().then(res => {
            setUserName(res);
            setUserRole(res);
            let userRole = localStorage.getItem('user_role');
            let userName = localStorage.getItem('user_name');
            setRole(userRole ? userRole : '');
            setName(userName ? userName : '');
        });
    }

    useEffect(() => {
        setTimeout(getUserData, 500);
    }, []);

    return (
        <header className="App-header">
                    
            <div className="header__block">
                            
                <img className="icon" src={logoImg}/>

                <MenuButtons role={role}/>
                                
                <div className="header__right_part">
                    <input type="search" className="header__search" placeholder='Поиск'/>
                    {
                        isLoggedIn
                        ?
                        <LogoutButton onClick={handleLogoutClick} name={name}/>
                        :
                        <LoginButton onClick={handleLoginClick}/>
                    }
                </div>
            </div>
        </header>
    );
};
export default Header;