<script setup lang="ts">
import { useAccountStore } from '@/stores/account';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const profileExpanded = ref<boolean>(false);
const accountStore = useAccountStore();
const router = useRouter();
function logout() {
    accountStore.$reset();
    window.localStorage.removeItem('account');
    router.push({ name: "userLogin" });
}
function mainPageLink() {
    router.push({ name: 'recommendations' });
}
function playlistsLink() {
    profileExpanded.value = false;
    router.push({ name: 'playlists' });
}
function expandProfile() {
    profileExpanded.value = !profileExpanded.value;
}

</script>

<template>
    <div class="min-h-screen flex flex-col">
        <!-- Shared layout -->
        <header class="p-5 bg-green-500">
            <nav class="flex flex-row justify-between text-white font-medium text-xl">
                <button @click="mainPageLink">Music</button>
                <div class="relative flex gap-x-8 flex-wrap">
                    <router-link v-if="accountStore.isAdmin" class="" :to="{ name: 'users' }">Users</router-link>
                    <router-link v-if="accountStore.isAdmin" class="" :to="{ name: 'addSong' }">Add Song</router-link>
                    <button id="profileExpandButton" @click="expandProfile">Profile</button>
                    <div
                        v-if="profileExpanded"
                        id="profileExpand"
                        class="absolute top-[40px] w-32 px-4 py-2 bg-green-500 z-20 right-0 text-lg"
                    >
                        <button
                            class="block after:block after:content-[''] after:h-[2px] after:w-full after:bg-green-400"
                            @click="playlistsLink"
                            >Playlists</button
                        >
                        <button
                            class="after:block after:content-[''] after:w-full after:bg-green-400 w-full text-left"
                            @click="logout"
                        >
                            Logout
                        </button>
                    </div>
                </div>
            </nav>
        </header>
        <router-view></router-view>
    </div>
</template>
