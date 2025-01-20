import {JwtHelper} from '../helpers/jwt_helper';

export function setAuthHeader(token: string | null | undefined) {
    localStorage.setItem('token', token ? token : '');
}

export function setIdToken(token: string | null | undefined) {
    localStorage.setItem('id_token', token ? token : '');
}

export function setUserRole(token: string | null | undefined) {
    const helper = new JwtHelper();
    localStorage.setItem('user_role', token ? helper.decodeToken(token).role : '');
}