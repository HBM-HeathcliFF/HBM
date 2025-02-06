import logoutImg from '../images/logout.png';

export function LoginButton(props: any) {
    return (
        <ul className='header_auth_panel'>
            <button className='auth_button' onClick={props.onClick}>
                Войти
            </button>
        </ul>
    );
}
  
export function LogoutButton(props: any) {
    return (
    <div className='header_auth_panel'>
		<a className='username'>{props.name}</a>
		<input type='image' className='logout_button' src={logoutImg} onClick={props.onClick}/>
	</div>
    );
}

export function MenuButtons(props: any) {
    if (props.role === 'Owner' ||
        props.role === 'Admin') {
        return <AdminHButtons/>;
    }
    return <UserHButtons/>;
}

function UserHButtons() {
    return(
    <ul className='header_buttons_panel'>
        <a href='/new'><li className='menu_item'>Новое</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/popular'><li className='menu_item'>Популярное</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/the-best'><li className='menu_item'>Лучшее</li></a>
    </ul>)
}
  
function AdminHButtons() {
    return(
    <ul className='header_buttons_panel'>
        <a href='/new'><li className='menu_item'>Новое</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/popular'><li className='menu_item'>Популярное</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/the-best'><li className='menu_item'>Лучшее</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/create-post'><li className='menu_item'>Новый пост</li></a>
    </ul>)
}