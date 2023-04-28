<script setup lang="ts">
interface Props {
    modelValue: any;
    name: string;
    type: string;
    placeholder?: string;
    label?: string;
    modelModifiers?: { [modifier: string]: boolean };
}
const props = defineProps<Props>();
const emit = defineEmits<{
    (event: 'update:modelValue', value: string | number): void;
}>();

function emitValue(event: Event) {
    let value: string | number = (event.target as HTMLInputElement).value;
    if (props.modelModifiers?.number) {
        value = Number(value);
    }
    emit('update:modelValue', value);
}
</script>

<template>
    <div>
        <label class="block text-gray-700 font-bold mb-2" :for="name">
            {{ label ?? name }}
        </label>
        <input
            class="appearance-none border border-gray-300 w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            :name="name"
            :id="name"
            :type="type"
            :placeholder="placeholder ?? name"
            :value="modelValue"
            @input="emitValue"
        />
    </div>
</template>
