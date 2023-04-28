<script setup lang="ts">
import { ref, watch } from 'vue';
import type { Page } from '@/assets/types/types.js';

interface Props {
    routeName: string;
    page: Page;
    query?: any;
}
const props = defineProps<Props>();

defineEmits<{
    (emit: 'page-click', page: number): void;
}>();
const pages = ref<number[]>([]);


// Sets up the pages array when the parent component loads new data.
watch(
    () => props.page,
    (newPage: Page) => {
        pages.value.length = 0;
        const currentPage = props.page.currentPage;
        const visiblePages = props.page.pageSize;
        const totalPages = newPage.totalPages;
        if (currentPage >= visiblePages && currentPage < totalPages) {
            const leftShift = currentPage - visiblePages;
            for (let i = 2 + leftShift; i <= currentPage + 1; i++) {
                pages.value.push(i);
            }
            return;
        }
        if (currentPage == totalPages) {
            let i = totalPages;
            const leftShift = totalPages - visiblePages;
            while (i >= 1 && i > leftShift) {
                pages.value.unshift(i--);
            }
            return;
        }

        const rightmostPage = visiblePages < totalPages ? visiblePages : totalPages;
        for (let i = 1; i <= rightmostPage; i++) {
            pages.value.push(i);
        }
    },
    {
        deep: true
    }
);
</script>
<template>
    <!-- Pagination -->
    <div class="flex flex-row">
        <div class="flex">
            <router-link
                tag="button"
                v-for="page in pages"
                :to="{
                    name: props.routeName,
                    params: { page: page },
                    query
                }"
                :class="{
                    'border border-indigo-300 bg-indigo-50': page === props.page.currentPage
                }"
                @click="$emit('page-click', page)"
                class="bg-indigo-100 py-1 px-3"
            >
                {{ page }}
            </router-link>
        </div>
    </div>
</template>
