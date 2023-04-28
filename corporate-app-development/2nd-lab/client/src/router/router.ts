import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import AppLayout from '@/views/shared/AppLayout.vue';
import IdentityLayout from '@/views/shared/IdentityLayout.vue';
import Recommendations from '@/views/Recommendations.vue';
import Registration from '@/views/identity/Registration.vue';
import Login from '@/views/identity/Login.vue';
import { useAccountStore } from '@/stores/account';
import Playlists from '@/views/Playlists.vue';
import PlaylistSongs from '@/views/PlaylistSongs.vue';
import SongAddition from '@/views/SongAddition.vue';
import SongEdit from '@/views/SongEdit.vue';
import Users from '@/views/Users.vue';

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        redirect: '/recommendations',
        component: AppLayout,
        children: [
            {
                name: 'recommendations',
                path: 'recommendations/:page(\\d+)?',
                component: Recommendations,
                meta: {
                    requiresAuth: true
                },
                props: (route) => ({
                    page: Number(route.params.page || 1),
                    search: {
                        term: route.query.searchTerm,
                        orderByProperty: route.query.orderBy,
                        orderByDescending: route.query.descending
                    }
                })
            },
            {
                name: 'playlistSongs',
                path: 'playlist/:playlistId(\\d+)/:page(\\d+)?',
                component: PlaylistSongs,
                meta: {
                    requiresAuth: true
                },
                props: (route) => ({
                    playlistId: Number(route.params.playlistId),
                    page: Number(route.params.page || 1),
                    search: {
                        term: route.query.searchTerm,
                        orderByProperty: route.query.orderBy,
                        orderByDescending: route.query.descending
                    }
                })
            },
            {
                name: 'playlists',
                path: 'playlists/:page(\\d+)?',
                component: Playlists,
                meta: {
                    requiresAuth: true
                },
                props: (route) => ({
                    page: Number(route.params.page || 1),
                    songId: Number(route.query.songId),
                    returnUrl: route.query.returnUrl,
                    userId: route.query.userId
                })
            }, 
            {
                name: 'users',
                path: 'users/:page(\\d+)?',
                component: Users,
                meta: {
                    requiresAuth: true,
                    requiresAdmin: true
                },
                props: (route) => ({
                    page: Number(route.params.page || 1)
                })
            }, 
            {
                name: 'addSong',
                path: "song/add",
                component: SongAddition,
                meta: {
                    requiresAuth: true,
                    requiresAdmin: true
                }
            },
            {
                name: 'editSong',
                path: "song/edit/:songId(\\d+)",
                component: SongEdit,
                props: (route) => ({
                    songId: Number(route.params.songId)
                }),
                meta: {
                    requiresAuth: true,
                    requiresAdmin: true
                }
            }
        ]
    },
    {
        path: '/account',
        redirect: '/account/login',
        component: IdentityLayout,
        children: [
            {
                path: 'register',
                name: 'userRegister',
                component: Registration
            },
            {
                path: 'login',
                name: 'userLogin',
                component: Login
            }
        ]
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
    scrollBehavior() {
        return { top: 0 };
    }
});

router.beforeEach((to, from) => {
    if (to.meta.requiresAuth) {
        const accountStore = useAccountStore();
        if (!accountStore.isAuthenticated) {
            accountStore.redirectPath = to.path;
            return { name: 'userLogin' };
        }

        if (to.meta.requiresAdmin && !accountStore.isAdmin) {
            return { path: from.fullPath };
        }
    }
});

export default router;
