<script setup lang="ts">
import type { Song } from '@/assets/types/types.js';
import ButtonSuccess from './Button.vue';
import { ref } from 'vue';
import { getJwtConfiguredAxios } from '@/assets/axios';
import { useAccountStore } from '@/stores/account';
const accountStore = useAccountStore();
const axios = getJwtConfiguredAxios(accountStore.jwt.token);
interface Props {
    song: Song;
    options?: {
        name: string,
        emitName: string
    }[];
}
defineProps<Props>();
defineEmits<{
    (emit: 'click-play', song: Song): void;
}>();
const optionsVisible = ref(false);

function nameosOfAuthors(song: Song) {
    return song.authors.map((a) => a.nickname).join(', ');
}
function showOptions() {
    optionsVisible.value = !optionsVisible.value;
}
async function onPlayMusic(song: Song) {
    try {
        const response = await axios.get(`/music/song/${encodeURIComponent(song.id)}`, { responseType: 'blob' });
        const blob = response.data;
        song.songFile = URL.createObjectURL(blob);
    } catch (error) {
        console.log(error);
    }

    try {
        await axios.post(`music/song/${encodeURIComponent(song.id)}/increment/popularity`);
    } catch (error) {
        console.log(error);
    }
}
</script>

<template>
    <div class="max-w-[460px] text-indigo-950">
        <button @click="showOptions" class="w-7 h-[10px] block ml-auto pb-[12px] relative
            before:content-[''] 
            before:block
            before:absolute
            before:top-0
            before:left-0
            before:w-[8px] 
            before:h-[8px]
            before:rounded
            before:bg-indigo-900
            after:content-['']                
            after:block
            after:absolute
            after:top-0
            after:right-0
            after:w-[8px] 
            after:h-[8px]
            after:rounded
            after:bg-indigo-900
        ">
            <span class="bg-indigo-900 block absolute right-[37.5%] top-0 rounded w-[8px] h-[8px]"></span>
        </button>
        <div class="border border-indigo-300 rounded-sm pt-3 px-3 pb-2 relative">
            <Transition name="slide-fade">
                <div v-if="optionsVisible" class="z-10 absolute right-0 top-0 text-indig-900 flex flex-col">
                    <button 
                        v-for="option in options"
                        class="border-b bg-indigo-100 border-white px-3 py-1 hover:bg-indigo-200"
                        @click="$emit(`${option.emitName}`, song)">
                        {{ option.name }}
                    </button>
                </div>
            </Transition>

            <div class="flex h-28 max-w-max">
                <img class="" :src="song.imageFile" alt="Song cover" />
                <div class="flex flex-col justify-between px-3">
                    <div class="flex flex-row justify-between pt-1">
                        <p class="font-bold">
                            {{ nameosOfAuthors(song) }}<br />
                            <span class="text-sm">Album: {{ song.album.name }}</span>
                        </p>
                        <span class="font-bold inline-block pl-1">{{ song.title }}</span>
                    </div>
                    <audio class="z-0" :src="song.songFile" controls>Audio is not supported.</audio>
                </div>
            </div>
            <div class="mt-2 flex flex-wrap items-center gap-1">
                Genres:
                <span
                    v-for="genre in song.genres"
                    class="inline-block bg-indigo-400 text-slate-100 rounded-lg p-1 px-2"
                    >{{ genre.name }}</span
                >
                <ButtonSuccess
                    class="ml-auto !py-1 !px-3"
                    name="Play"
                    @click="onPlayMusic(song)"
                />
            </div>
        </div>

        <span class="float-right text-slate-400 text-xs">Played: {{ song.popularity }}</span>
    </div>
</template>
<style>
.slide-fade-enter-active {
  transition: all 0.3s ease-out;
}

.slide-fade-leave-active {
  transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
}

.slide-fade-enter-from,
.slide-fade-leave-to {
  transform: translateY(20px);
  opacity: 0;
}
</style>