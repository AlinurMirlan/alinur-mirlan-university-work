<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import type { Page, PagedEntities, PlaylistRaw, PlaylistSong, User } from '@/assets/types/types';
import Pagination from '@/components/Pagination.vue';
import { getJwtConfiguredAxios } from '@/assets/axios';
import { useAccountStore } from '@/stores/account';
import LoadSpinner from '@/components/LoadSpinner.vue'; 
import { useRouter } from 'vue-router';
import UserVue from '@/components/User.vue';
import ErrorMessage from '@/components/form/ErrorMessage.vue';

const accountStore = useAccountStore();
const router = useRouter();
const axios = getJwtConfiguredAxios(accountStore.jwt.token);
interface Props {
    page: number;
}
const props = defineProps<Props>();
const loading = ref<boolean>(false);
const users = ref<User[]>([]);
const page: Page = reactive({
    currentPage: props.page,
    pageSize: 6,
    totalPages: -1
});
const error = ref<string>("");

async function pressUser(user: User) {
    router.push({
        name: "playlists",
        query: {
            userId: user.id
        }
    });
}
async function DeleteUser(user: User) {
    if (user.id === accountStore.userId) {
        error.value = "You cannot delete yourself";
        return;
    }
    try {
        await axios.post("/account/remove", {}, {
            params: {
                userId: user.id
            }
        });
    } catch (error) {
        console.log(error);
    }

    if (users.value.length == 1 && page.currentPage !== 1) {
        updatePage(page.currentPage - 1);
        return;
    }

    updatePage(page.currentPage);
}
async function updatePage(newPage: number) {
    page.currentPage = newPage;
    await loadUsers();
}
async function loadUsers() {
    users.value.length = 0;
    try {
        loading.value = true;
        const response = await axios.get<PagedEntities<User>>(`/account/users`, {
            params: {
                page: page.currentPage,
                pageSize: page.pageSize
            }
        });
        const pagedUsers = response.data;
        users.value = pagedUsers.results;
        page.totalPages = pagedUsers.totalPages || 1;
    } catch (error) {
        console.log(error);
    }

    loading.value = false;
}
onMounted(async () => {
    await loadUsers();
})
</script>
<template>
    <main class="flex flex-col grow items-center max-w-4xl mx-auto">
        <div class="max-w-4xl mt-8">
            <ErrorMessage v-if="error" :message="error" class="block mb-4"/>
            <LoadSpinner v-if="loading" />
            <UserVue class="pb-2" v-for="user in users" :user="user" 
                @click-delete-user="DeleteUser" @click-user="pressUser" />
        </div>
        <div class="h-10 w-full grow flex flex-col-reverse pb-4">
            <div class="">
                <Pagination route-name="users" :page="page" @page-click="updatePage" />
            </div>
         </div>
    </main> 
</template>