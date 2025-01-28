import { useContext } from 'react';
import { MenuButtons, LoginButton, LogoutButton } from './header-buttons'
import { signinRedirect, signoutRedirect } from '../auth/user-service';
import logoImg from '../images/logo.png';
import { AuthContext } from '../auth/auth-provider';
import Search from './search';
import SearchProvider from './search-provider';

function Header() {
    const { role, name, isAuthenticated } = useContext(AuthContext);

    function handleLoginClick() {
        signinRedirect();
    }

    function handleLogoutClick() {
        signoutRedirect({'id_token_hint': localStorage.getItem('id_token')});
    }

    return (
        <header className="App-header">
                    
            <div className="header__block">
                            
                <img className="icon" src={logoImg}/>

                <MenuButtons role={role}/>
                                
                <div className="header__right_part">
                    <Search/>
                    {
                        isAuthenticated
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