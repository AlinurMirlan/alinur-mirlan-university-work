<script setup lang="ts">
import ButtonDanger from './ButtonDanger.vue';
import type { User } from '@/assets/types/types';
interface Props {
    user: User
}
defineProps<Props>();
defineEmits<{
    (emit: "click-user", user: User): void,
    (emit: "click-delete-user", user: User): void
}>();
</script>
<template>
    <div class="flex w-full">
        <button class="w-full border border-indigo-300 rounded-sm pt-3 px-3 pb-2 font-bold text-indigo-950"
            @click="$emit('click-user', user)">
            <table class="">
                <tr>
                    <th class="border-r px-4 border-indigo-500">Email</th>
                    <td class="text-right px-4">{{ user.email }}</td>
                </tr>
                <tr>
                    <th class="border-r px-4 border-indigo-500">Name</th>
                    <td class="text-right px-4">{{ user.name }}</td>
                </tr>
            </table>
        </button>   
        <ButtonDanger name="Delete" @click="$emit('click-delete-user', user)" class=""/>
    </div>

</template>