<script setup lang="ts">
import Field from '@/components/form/Field.vue';
import Button from '@/components/Button.vue';
import PlaylistVue from '@/components/Playlist.vue';
import { onMounted, reactive, ref } from 'vue';
import type { Page, PagedEntities, PlaylistRaw, PlaylistSong } from '@/assets/types/types';
import Pagination from '@/components/Pagination.vue';
import { getJwtConfiguredAxios } from '@/assets/axios';
import { useAccountStore } from '@/stores/account';
import { Playlist } from '@/assets/classes';
import z from 'zod';
import { formatErrors } from '@/assets/errorFormatter';
import LoadSpinner from '@/components/LoadSpinner.vue'; 
import { useRouter } from 'vue-router';

const accountStore = useAccountStore();
const router = useRouter();
const axios = getJwtConfiguredAxios(accountStore.jwt.token);
interface Props {
    page: number;
    songId?: number;
    returnUrl?: string;
    userId?: string
}
const props = defineProps<Props>();
const isOwner = ref<boolean>(true);
let userId = accountStore.userId;
if (props.userId && accountStore.isAdmin) {
    userId = props.userId;
    isOwner.value = false;
}
const loading = ref<boolean>(false);
const playlists = ref<Playlist[]>([]);
const page: Page = reactive({
    currentPage: props.page,
    pageSize: 6,
    totalPages: -1
});
const schema = z.string().min(3, "Playlist name must be at least 3 characters long");
const newPlaylistName = ref<string>("");
const error = ref<string[]>([]);
let query: any = null;

async function pressPlaylist(playlist: Playlist) {
    if (props.songId) {
        const playlistSong: PlaylistSong = {
            playlistId: playlist.id!,
            songId: props.songId
        }
        try {
            await axios.post("/music/playlist/add/song", playlistSong);
        } catch (error) {
            console.log(error);
        }

        router.push({ path: props.returnUrl || "/" });
        return;
    }

    router.push({
        name: "playlistSongs", 
        params: {
            playlistId: playlist.id
        } 
    });
}
async function DeletePlaylist(playlist: Playlist) {
    if (!accountStore.isAdmin && !isOwner.value) {
        return;
    }

    try {
        await axios.post("/music/playlist/remove", playlist);
    } catch (error) {
        console.log(error);
    }

    if (playlists.value.length == 1 && page.currentPage !== 1) {
        updatePage(page.currentPage - 1);
        return;
    }

    updatePage(page.currentPage);
}
async function AddPlaylist() {
    if (!isOwner.value) {
        return;
    }

    error.value.length = 0;
    newPlaylistName.value = newPlaylistName.value.trim();
    const result = schema.safeParse(newPlaylistName.value);
    if (!result.success) {
        error.value = result.error.flatten().formErrors;
        return;
    }

    let response = await axios.get<boolean>("/music/playlist/present", {
        params: {
            userId: accountStore.userId,
            playlistName: newPlaylistName.value
        }
    });
    const isPresent = response.data;
    if (isPresent) {
        error.value.push(`A playlist called '${newPlaylistName.value}' already exists`);
        return;
    }

    error.value.length = 0;
    const playlistRaw: PlaylistRaw = {
        name: newPlaylistName.value,
        userId: accountStore.userId
    };
    const playlistResponse = await axios.post<PlaylistRaw>("/music/playlist/add", playlistRaw);
    if (page.currentPage < page.totalPages) {
        updatePage(page.totalPages);
        return;
    }

    const playlist = new Playlist(playlistResponse.data);
    if (playlists.value.length + 1 > page.pageSize) {
        page.totalPages++;
        return;
    }

    playlists.value.push(playlist);
}
async function updatePage(newPage: number) {
    page.currentPage = newPage;
    await loadPlaylists();
}
async function loadPlaylists() {
    playlists.value.length = 0;
    try {
        loading.value = true;
        const response = await axios.get<PagedEntities<PlaylistRaw>>(`/music/${encodeURIComponent(userId)}/playlists`, {
            params: {
                page: page.currentPage,
                pageSize: page.pageSize
            }
        });
        const pagedRawPlaylists = response.data;
        pagedRawPlaylists.results.forEach(rawPlaylist => {
            playlists.value.push(new Playlist(rawPlaylist));
        });
        page.totalPages = pagedRawPlaylists.totalPages || 1;
    } catch (error) {
        console.log(error);
    }

    loading.value = false;
}
onMounted(async () => {
    if (props.songId) {
        query = { songId: props.songId };
    }
    await loadPlaylists();
    if (playlists.value.length === 0) {
        return;
    }
    try {
        const response = await axios.get<boolean>('music/playlist/belongs/to/user', {
            params: {
                userId: accountStore.userId,
                playlistId: playlists.value[0].id
            }
        });
        isOwner.value = response.data;
    } catch (error) {
        console.log(error);
        return;
    }
})
</script>
<template>
    <main class="flex flex-col grow items-center max-w-4xl mx-auto">
        <div class="max-w-4xl">
            <form action="" class="w-full border-b mb-6 pb-2 border-green-300 mt-4">
                <Field type="text" class="w-full" name="Add Playlist" v-model="newPlaylistName" placeholder="Name"/>
                <p v-if="error" class="text-red-500 font-bold" >{{ formatErrors(error) }}</p>
                <Button name="Add" @click="AddPlaylist" class="mt-1" :disabled="!isOwner" :class="{ 'disabled:opacity-10': !isOwner }" />
            </form>
            <LoadSpinner v-if="loading" />
            <PlaylistVue class="pb-2" v-for="playlist in playlists" :playlist="playlist" 
                @click-delete-playlist="DeletePlaylist" @click-playlist="pressPlaylist" />
        </div>
        <div class="h-10 w-full grow flex flex-col-reverse pb-4">
            <div class="">
                <Pagination route-name="playlists" :page="page" :query="query" @page-click="updatePage" />
            </div>
         </div>
    </main> 
</template>