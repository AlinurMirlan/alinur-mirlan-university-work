<script setup lang="ts">
interface Props {
    name: string;
}
defineProps<Props>();
defineEmits<{
    (emit: 'click'): void;
}>();
</script>
<template>
    <button
        class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 focus:outline-none focus:shadow-outline rounded-none"
        type="button"
        @click.prevent="$emit('click')"
    >
        {{ name }}
    </button>
</template>
