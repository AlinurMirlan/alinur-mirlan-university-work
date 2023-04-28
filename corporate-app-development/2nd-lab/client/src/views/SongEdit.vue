<script setup lang="ts">
import InputField from '@/components/form/Field.vue';
import Button from '@/components/Button.vue';
import z from 'zod';
import ErrorMessage from '@/components/form/ErrorMessage.vue';
import { useAccountStore } from '@/stores/account';
import { useSongStore } from '@/stores/song';
import { getJwtConfiguredAxios } from '@/assets/axios.js';
import { formatErrors } from '@/assets/errorFormatter';
import { useShapeErrors } from '@/composables/userErrors';
import { useRouter } from 'vue-router';
import { mergeProps, ref, type Ref } from 'vue';
import type { Album, Author, Genre } from '@/assets/types/types';
import axios from 'axios';

// STATE
interface Props {
    songId: number;
}
const props = defineProps<Props>();
interface SongModel {
    title: string;
    authors: Author[];
    genres: Genre[];
    album: Album;
    imageFile: string;
    songFile: string;
};
const router = useRouter();
const accountStore = useAccountStore();
const songStore = useSongStore();
const axiosBase = getJwtConfiguredAxios(accountStore.jwt.token);
let songSchema = z
    .object({
        title: z.string().min(1, 'Title must at least be 1 character long'),
        album: z.string().min(1, 'Title must at least be 1 character long'),
        authors: z.string().min(1, 'Authors field must at least be 1 characters long'),
        genres: z.string().min(2, { message: 'Genres field must at least be 2 characters long' })
    })

const song = songStore.song;
const { shape, errors } = useShapeErrors<typeof songSchema>({
    title: song?.title || "",
    album: song?.album.name || '',
    authors: song?.authors.reduce((nicknames, a) => nicknames + `${a.nickname}, `, "").slice(0, -2) || '',
    genres: song?.genres.reduce((genres, g) => genres + `${g.name}, `, "").slice(0, -2) || ''
});
const files = {
    songFile: ref<File | undefined>(),
    imageFile: ref<File | undefined>()
}
const songErrors = ref<string[]>([]);

// METHODS
function addFile(event: Event, file: Ref<File | undefined>) {
    file.value = (event.target as HTMLInputElement).files?.item(0)!;
}
async function editSong() {
    const result = songSchema.safeParse(shape);
    if (!result.success) {
        errors.value = result.error.flatten().fieldErrors;
        return;
    }
    for (const propertyName in errors.value) {
        errors.value[propertyName as keyof (typeof errors.value)] = [];
    }

    const authors: Author[] = [];
    shape.authors.split(",").forEach(authorName => {
        authors.push({
            id: 0,
            nickname: authorName
        })
    });
    const genres: Genre[] = [];
    shape.genres.split(",").forEach(genreName => {
        genres.push({
            id: 0,
            name: genreName
        })
    });

    songErrors.value.length = 0;
    const songModel: SongModel = {
        title: shape.title,
        album: {
            id: 0,
            name: shape.album
        },
        authors,
        genres,
        songFile: files.songFile.value?.name || "",
        imageFile: files.imageFile.value?.name || ""
    };
    try {
        await axiosBase.post(`music/song/edit/${props.songId}`, songModel);
    } catch (error) {
        if (!axios.isAxiosError(error)) {
            console.log(error);
            return;
        }


        if (error.code === axios.AxiosError.ERR_BAD_REQUEST) {
            songErrors.value = ["There is already a song with the same signature (title, authors). Please, make sure the signature of your song is unique"];
        }
        return;
    }

    const formData = new FormData();
    formData.append("songFile", files.songFile.value || "");
    formData.append("imageFile", files.imageFile.value || "");

    try {
        await axiosBase.post("music/song/add/assets", formData);
    } catch (error) {
        console.log(error);
        return;
    }

    router.push(accountStore.getRedirect());
}

</script>
<template>
    <main v-if="accountStore.isAdmin">
        <form class="max-w-lg mx-auto mt-6">
            <ErrorMessage v-if="songErrors" :message="formatErrors(songErrors)" />
            <InputField class="mt-4" name="Title" type="text" v-model="shape.title" />
            <ErrorMessage v-if="errors.title" :message="formatErrors(errors.title)" />

            <InputField class="mt-4" name="Album" type="text" v-model="shape.album" />
            <ErrorMessage v-if="errors.album" :message="formatErrors(errors.album)" />

            <label for="imagefile" class="block text-gray-700 font-bold mb-2 mt-4">Choose a new album cover in PNG or JPEG format:</label>
            <input type="file" id="imagefile" name="imagefile" accept="image/png, image/jpeg" class="py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline block"
                @change="addFile($event, files.imageFile)">

            <InputField class="mt-4" name="Authors" placeholder="Authors separated by commas" type="text" v-model="shape.authors" />
            <ErrorMessage v-if="errors.authors" :message="formatErrors(errors.authors)" />

            <InputField class="mt-4" name="Genres" placeholder="Genres separated by commas" type="text" v-model="shape.genres" />
            <ErrorMessage v-if="errors.genres" :message="formatErrors(errors.genres)" />

            <label for="mp3file" class="block text-gray-700 font-bold mb-2 mt-4">Choose a new MP3 song file:</label>
            <input type="file" id="mp3file" name="mp3file" accept="audio/mp3" class="py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline block"
                @change="addFile($event, files.songFile)">

            <Button class="mt-4" name="Edit" @click="editSong" />
        </form>
    </main>
</template>