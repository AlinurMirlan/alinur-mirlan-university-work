import { defineStore } from 'pinia';
import type { Song } from '@/assets/types/types.js';

export const useSongStore = defineStore("songStore", {
    state: (): { song: Song | undefined } => {
        return {
            song: undefined
        }
    }
})