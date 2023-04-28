import axios from 'axios';

export function getJwtConfiguredAxios(jsonWebToken: string) {
    return axios.create({
        baseURL: 'https://localhost:7016/api',
        headers: {
            Authorization: `Bearer ${jsonWebToken}`
        }
    });
}

export function getConfiguredAxios() {
    return axios.create({
        baseURL: 'https://localhost:7016/api'
    });
}
