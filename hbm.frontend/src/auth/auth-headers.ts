import {JwtHelper} from '../helpers/jwt-helper';

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

export function setUserName(token: string | null | undefined) {
    const helper = new JwtHelper();
    localStorage.setItem('user_name', token ? helper.decodeToken(token).name : '');
}

export function setUserId(token: string | null | undefined) {
    const helper = new JwtHelper();
    localStorage.setItem('user_id', token ? helper.decodeToken(token).sub : '');
}