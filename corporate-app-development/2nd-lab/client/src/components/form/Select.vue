<script setup lang="ts">
interface Props {
    modelValue: string;
    label: string;
    options: [{ value: string; name: string }, boolean][];
}
defineProps<Props>();
defineEmits(['update:modelValue']);
</script>
<template>
    <div>
        <label class="block text-gray-700 font-bold mb-2" :for="label"> {{ label }} </label>
        <select
            class="appearance-none border border-gray-300 w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            :id="label"
            @change="$emit('update:modelValue', ($event.target as HTMLSelectElement).value)"
        >
            <option v-for="option in options" :selected="option[1]" :value="option[0].value" >{{ option[0].name }}</option>
        </select>
    </div>
</template>
