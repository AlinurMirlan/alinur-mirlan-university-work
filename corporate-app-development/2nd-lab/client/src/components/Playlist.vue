<script setup lang="ts">
import type { Playlist } from '@/assets/classes';
import ButtonDanger from './ButtonDanger.vue';
interface Props {
    playlist: Playlist
}
defineProps<Props>();
defineEmits<{
    (emit: "click-playlist", playlist: Playlist): void,
    (emit: "click-delete-playlist", playlist: Playlist): void
}>();
</script>
<template>
    <div class="flex w-full">
        <button class="w-full border border-indigo-300 rounded-sm pt-3 px-3 pb-2 font-bold text-indigo-950"
            @click="$emit('click-playlist', playlist)">
            {{ playlist.name }}
        </button>   
        <ButtonDanger name="Delete" @click="$emit('click-delete-playlist', playlist)" class=""/>
    </div>

</template>