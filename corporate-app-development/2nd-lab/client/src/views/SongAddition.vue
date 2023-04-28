<script setup lang="ts">
import InputField from '@/components/form/Field.vue';
import Button from '@/components/Button.vue';
import z from 'zod';
import ErrorMessage from '@/components/form/ErrorMessage.vue';
import { useAccountStore } from '@/stores/account';
import { getJwtConfiguredAxios } from '@/assets/axios.js';
import { formatErrors } from '@/assets/errorFormatter';
import { useShapeErrors } from '@/composables/userErrors';
import { useRouter } from 'vue-router';
import { ref, type Ref } from 'vue';
import type { Album, Author, Genre } from '@/assets/types/types';
import axios from 'axios';

// STATE
interface SongModel {
    title: string;
    authors: Author[];
    genres: Genre[];
    album: Album;
    imageFile: string;
    songFile: string;
};
const success = ref<boolean>(false);
const router = useRouter();
const accountStore = useAccountStore();
const axiosBase = getJwtConfiguredAxios(accountStore.jwt.token);
const file = ref<File>();
let songSchema = z
    .object({
        title: z.string().min(1, 'Title must at least be 1 character long'),
        album: z.string().min(1, 'Title must at least be 1 character long'),
        authors: z.string().min(1, 'Authors field must at least be 1 characters long'),
        genres: z.string().min(2, { message: 'Genres field must at least be 2 characters long' })
    })

const { shape, errors } = useShapeErrors<typeof songSchema>({
    title: '',
    album: '',
    authors: '',
    genres: ''
});
const files = {
    songFile: ref<File | undefined>(),
    imageFile: ref<File | undefined>()
}
const fileErrors: { songFile: Ref<string[]>, imageFile: Ref<string[]> } = {
    songFile: ref<string[]>([]),
    imageFile: ref<string[]>([])
}
const songErrors = ref<string[]>([]);

// METHODS
function addFile(event: Event, file: Ref<File | undefined>) {
    file.value = (event.target as HTMLInputElement).files?.item(0)!;
}
async function addSong() {
    success.value = false;
    let filesValidationFailed = false;
    if (!files.songFile.value) {
        fileErrors.songFile.value = ["Song file has to be loaded"];
        filesValidationFailed = true;
    }
    if (!files.imageFile.value) {
        fileErrors.imageFile.value = ["Image file has to be loaded"];
        filesValidationFailed = true;
    }
    const result = songSchema.safeParse(shape);
    if (!result.success) {
        errors.value = result.error.flatten().fieldErrors;
        return;
    }
    for (const propertyName in errors.value) {
        errors.value[propertyName as keyof (typeof errors.value)] = [];
    }

    if (filesValidationFailed) {
        return;
    }
    for (const propertyName in fileErrors) {
        fileErrors[propertyName as keyof (typeof fileErrors)].value = [];
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
        songFile: files.songFile.value!.name,
        imageFile: files.imageFile.value!.name
    };
    try {
        await axiosBase.post("music/song/add", songModel);
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
    formData.append("songFile", files.songFile.value!);
    formData.append("imageFile", files.imageFile.value!);
    try {
        await axiosBase.post("music/song/add/assets", formData);
    } catch (error) {
        console.log(error);
        return;
    }

    for (const propertyName in shape) {
        shape[propertyName as keyof (typeof shape)] = "";
    }
    success.value = true;
}

</script>
<template>
    <main>
        <form class="max-w-lg mx-auto mt-6">
            <div v-if="success" class="text-green-500 text-xl font-bold">Successfully added</div>
            <ErrorMessage v-if="songErrors" :message="formatErrors(songErrors)" />
            <InputField class="mt-4" name="Title" type="text" v-model="shape.title" />
            <ErrorMessage v-if="errors.title" :message="formatErrors(errors.title)" />

            <InputField class="mt-4" name="Album" type="text" v-model="shape.album" />
            <ErrorMessage v-if="errors.album" :message="formatErrors(errors.album)" />

            <label for="imagefile" class="block text-gray-700 font-bold mb-2 mt-4">Choose the album cover in PNG or JPEG format:</label>
            <input type="file" id="imagefile" name="imagefile" accept="image/png, image/jpeg" class="py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline block"
                @change="addFile($event, files.imageFile)">
            <ErrorMessage v-if="fileErrors.imageFile" :message="formatErrors(fileErrors.imageFile.value)" />

            <InputField class="mt-4" name="Authors" placeholder="Authors separated by commas" type="text" v-model="shape.authors" />
            <ErrorMessage v-if="errors.authors" :message="formatErrors(errors.authors)" />

            <InputField class="mt-4" name="Genres" placeholder="Genres separated by commas" type="text" v-model="shape.genres" />
            <ErrorMessage v-if="errors.genres" :message="formatErrors(errors.genres)" />

            <label for="mp3file" class="block text-gray-700 font-bold mb-2 mt-4">Choose an MP3 song file:</label>
            <input type="file" id="mp3file" name="mp3file" accept="audio/mp3" class="py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline block"
                @change="addFile($event, files.songFile)">
                <ErrorMessage class="block" v-if="fileErrors.songFile" :message="formatErrors(fileErrors.songFile.value)" />

            <Button class="mt-4" name="Add" @click="addSong" />
        </form>
    </main>
</template>