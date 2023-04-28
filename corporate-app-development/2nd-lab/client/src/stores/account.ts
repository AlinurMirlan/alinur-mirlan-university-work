import { Jwt } from '@/assets/classes';
import type { UserCedentials } from '@/assets/types/types';
import { defineStore } from 'pinia';

interface State {
    redirectPath: string;
    jwt: Jwt;
    userId: string;
    isAdmin: boolean | null;
}
interface StateRaw {
    redirectPath: string;
    jwt: { [P in keyof Jwt]: string };
    userId: string;
    isAdmin: boolean;
}

export const useAccountStore = defineStore('account', {
    state: (): State => {
        let accountState = localStorage.getItem('account');
        if (accountState) {
            const accountRaw: StateRaw = JSON.parse(accountState);
            const account: State = {
                ...accountRaw,
                jwt: {
                    token: accountRaw.jwt.token,
                    expiration: new Date(Date.parse(accountRaw.jwt.expiration))
                }
            };
            return account;
        }
        return {
            redirectPath: '',
            jwt: {
                token: '',
                expiration: new Date()
            },
            isAdmin: false,
            userId: ''
        };
    },
    getters: {
        isAuthenticated: (state) => state.jwt.token !== '' && state.jwt.expiration > new Date(),
        isAdminGet: (state) => state.isAdmin
    },
    actions: {
        setJwt(jwtRaw: UserCedentials) {
            this.jwt = new Jwt(jwtRaw);
            this.userId = jwtRaw.userId;
            this.isAdmin = jwtRaw.isAdmin;
            localStorage.setItem('account', JSON.stringify(this.$state));
        },
        getRedirect() {
            let redirect = '/recommendations';
            if (this.redirectPath) {
                redirect = this.redirectPath;
                this.redirectPath = '';
            }
            return redirect;
        }
    }
});
