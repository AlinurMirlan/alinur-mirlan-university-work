<script setup lang="ts">
import Button from '@/components/Button.vue';
import ButtonDanger from '@/components/ButtonDanger.vue';
import Pagination from '@/components/Pagination.vue';
import MusicCard from '@/components/MusicCard.vue';
import LoadSpinner from '@/components/LoadSpinner.vue';
import Select from '@/components/Select.vue';
import { onMounted, reactive, ref } from 'vue';
import type { Genre, Song, PagedSongs, Page } from '@/assets/types/types.js';
import { useAccountStore } from '@/stores/account';
import { useSongStore } from '@/stores/song';
import { getJwtConfiguredAxios } from '@/assets/axios.js';
import { onBeforeRouteUpdate, useRoute, useRouter } from 'vue-router';

const router = useRouter();
const route = useRoute();
const accountStore = useAccountStore();
const songStore = useSongStore();
const axios = getJwtConfiguredAxios(accountStore.jwt.token);
type OrderByProperties = 'Popularity' | 'ReleaseDate';
type orderByDescending = 'true' | 'false';
const props = defineProps<{
    page: number;
    search: {
        term?: string;
        orderByProperty?: OrderByProperties;
        orderByDescending?: orderByDescending;
    };
}>();
const search = {
    gennreIds: ref<number[]>([]),
    term: ref<string>(''),
    orderByProperty: ref<OrderByProperties>('Popularity'),
    orderByDescending: ref<orderByDescending>('true')
};
const genreDialog = ref<HTMLDialogElement | null>(null);
let loading = ref<boolean>(false);
let genres = ref<Genre[]>([]);
let songs = ref<Song[]>([]);
const page: Page = reactive({
    currentPage: props.page,
    pageSize: 6,
    totalPages: -1
});
const musicCardOptions = [
    {name: 'Add to playlist', emitName: 'click-add-to-playlist'}
];
if (accountStore.isAdmin) {
    musicCardOptions.push({ name: 'Delete song', emitName: 'click-delete-song' });
    musicCardOptions.push({ name: 'Edit song', emitName: 'click-edit-song' });
}

function onDialogReset() {
    search.gennreIds.value = [];
}
function onDialogOpen() {
    genreDialog.value?.showModal();
}
function onDialogClose() {
    genreDialog.value?.close();
}
function onGenreChecked(genre: Genre) {
    const index = search.gennreIds.value.indexOf(genre.id);
    if (index !== -1) {
        search.gennreIds.value.splice(index, 1);
    } else {
        search.gennreIds.value.push(genre.id);
    }
}
async function setSongsAndImages(rawSongs: Song[]) {
    // On a note: array.forEach() doesn't wait for an async operation to finish.
    for (const song of rawSongs) {
        try {
            const response = await axios.get(`/music/image/${song.id}`, { responseType: 'blob' });
            const blob = response.data;
            song.imageFile = URL.createObjectURL(blob);
        } catch (error) {
            console.log(error);
        }
    }

    songs.value = rawSongs;
}
async function searchSong() {
    changePage(1);
}
function changePage(newPage: number) {
    router.push({
        name: 'recommendations',
        query: {
            searchTerm: search.term.value,
            orderBy: search.orderByProperty.value,
            descending: search.orderByDescending.value
        },
        params: {
            page: newPage
        }
    });
}
async function loadSongs(): Promise<Page | null> {
    songs.value.length = 0;
    let pageInfo: Page | null = null;
    try {
        let response = await axios.get<PagedSongs>(
            `/music/${encodeURIComponent(search.term.value)}`,
            {
                params: {
                    genreIds: search.gennreIds.value,
                    page: page.currentPage,
                    pageSize: page.pageSize,
                    orderByProperty: search.orderByProperty.value,
                    orderByDescending: search.orderByDescending.value
                },
                paramsSerializer: { indexes: null }
            }
        );

        const pagedSongs = response.data;
        pageInfo = pagedSongs;
        await setSongsAndImages(pagedSongs.results);
    } catch (error) {
        console.log(error);
    }

    return pageInfo;
}
async function setPagedSongs() {
    const pageInfo = await loadSongs();
    page.totalPages = pageInfo?.totalPages || 1;
}
async function getGenres() {
    try {
        const response = await axios.get<Genre[]>('/music/genres');
        const genresData = response.data;
        genres.value = genresData;
    } catch (error) {
        console.log(error);
    }
}
function onAddToPlaylist(song: Song) {
    router.push({ name: "playlists", query: { songId: song.id, returnUrl: route.fullPath } });
}
async function onDeleteSong(song: Song) {
    if (!accountStore.isAdmin) {
        return;
    }

    try {
        await axios.post("music/song/delete", null, {
            params: {
                songId: song.id
            }
        });
    } catch (error) {
        console.log(error);
    }

    router.go(0);
}
function onEditSong(song: Song) {
    accountStore.redirectPath = route.fullPath;
    songStore.song = song;
    router.push({ name: "editSong", params: {
        songId: song.id
    }});
}

onMounted(async () => {
    search.term.value = props.search.term || '';
    search.orderByDescending.value = props.search.orderByDescending ?? 'true';
    if (props.search.orderByProperty) {
        search.orderByProperty.value = props.search.orderByProperty;
    }
    loading.value = true;
    await getGenres();
    await setPagedSongs();
    loading.value = false;
});
onBeforeRouteUpdate(async (to, from) => {
    search.term.value = to.query.searchTerm as string || '';
    search.orderByDescending.value = to.query.descending as orderByDescending ?? 'true';
    if (to.query.orderBy) {
        search.orderByProperty.value = to.query.orderBy as OrderByProperties;
    }
    page.currentPage = Number(to.params.page || 1);
    loading.value = true;
    await setPagedSongs();
    loading.value = false;
});
</script>

<template>
    <main id="index" class="h-full p-5 flex-grow flex flex-col">
        <!-- Control panel -->
        <section>
            <form class="flex flex-row gap-x-3" action="">
                <!-- Search panel -->
                <div class="flex text-[0px]">
                    <form @submit.prevent="searchSong" class="">
                        <Button class="text-base h-full" name="Search" @click="searchSong"></Button>
                        <input
                            v-model="search.term.value"
                            class="h-full text-base py-1 px-2 outline-none focus:outline-none bg-slate-100"
                            type="text"
                            name="searchTerm"
                            placeholder="Title, Author(s), Album"
                        />
                    </form>
                </div>
                <!-- Genre dialog -->
                <dialog ref="genreDialog" id="genreDialog" class="max-w-lg">
                    <div class="flex flex-row flex-wrap gap-2">
                        <!-- Genre tag -->
                        <label
                            v-for="genre in genres"
                            :for="genre.id.toString()"
                            :class="{
                                'bg-indigo-400 border-white text-slate-100':
                                    search.gennreIds.value.includes(genre.id)
                            }"
                            class="border border-indigo-300 rounded-lg py-1 px-2 select-none"
                        >
                            <input
                                type="checkbox"
                                name="checkbox"
                                :id="genre.id.toString()"
                                v-on:change="onGenreChecked(genre)"
                                class="hidden"
                            />
                            {{ genre.name }}
                        </label>
                    </div>
                    <div class="flex flex-row space-x-2 mt-3">
                        <ButtonDanger
                            name="Reset"
                            @click="onDialogReset"
                            class="text-base px-3"
                        />
                        <Button name="Ok" @click="onDialogClose" class="text-base px-3 py-1" />
                    </div>
                </dialog>
                <Button name="Genres" id="genreDialogButton" @click="onDialogOpen" class="px-3" />
                <div class="">
                    <Button name="Order by" @click="searchSong" />
                    <Select
                        v-model="search.orderByProperty.value"
                        :options="[
                            [{ name: 'Popularity', value: 'Popularity' }, true],
                            [{ name: 'Release date', value: 'ReleaseDate' }, false]
                        ]"
                        class="pl-1"
                    />
                    <Select
                        v-model="search.orderByDescending.value"
                        :options="[
                            [{ name: 'Descending', value: 'true' }, true],
                            [{ name: 'Ascending', value: 'false' }, false]
                        ]"
                    />
                </div>
            </form>
        </section>
        <div class="w-full h-[1px] bg-teal-300 mb-4 mt-2"></div>
        <!-- Recommendation page -->
        <section class="flex flex-col justify-between flex-grow">
            <LoadSpinner v-if="loading" />
            <!-- Recommended songs -->
            <div class="flex flex-row flex-wrap gap-2">
                <MusicCard :options="musicCardOptions" 
                v-for="song in songs" :song="song"
                @click-add-to-playlist="onAddToPlaylist(song)"
                @click-delete-song="onDeleteSong(song)"
                @click-edit-song="onEditSong(song)" />
            </div>
            <Pagination
                route-name="recommendations"
                :page="page"
                :query="{ 
                    searchTerm: search.term.value,
                    orderBy: search.orderByProperty.value,
                    descending: search.orderByDescending.value,
                }"
            />
        </section>
    </main>
</template>