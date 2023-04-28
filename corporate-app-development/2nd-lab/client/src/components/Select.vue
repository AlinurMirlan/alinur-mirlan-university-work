<script setup lang="ts">
interface Props {
    options: [{ name: string; value: string }, boolean][];
    modelValue: any;
}
defineProps<Props>();
defineEmits(['update:modelValue']);
</script>
<template>
    <select
        class="border-b border-green-500 text-gray-500 h-full"
        @change="$emit('update:modelValue', ($event.target as HTMLSelectElement).value)"
    >
        <option class="hover:bg-green-700 hover:text-white" v-for="option in options" :value="option[0].value" :selected="option[1]">
            {{ option[0].name }}
        </option>
    </select>
</template>
