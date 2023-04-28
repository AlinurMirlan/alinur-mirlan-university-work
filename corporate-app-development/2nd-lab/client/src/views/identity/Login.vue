<script setup lang="ts">
import InputField from '@/components/form/Field.vue';
import Button from '@/components/Button.vue';
import z from 'zod';
import ErrorMessage from '@/components/form/ErrorMessage.vue';
import { getConfiguredAxios } from '@/assets/axios.js';
import { useAccountStore } from '@/stores/account';
import type { UserCedentials } from '@/assets/types/types.js';
import { formatErrors } from '@/assets/errorFormatter';
import { useShapeErrors } from '@/composables/userErrors';
import { useRouter } from 'vue-router';
import axios from 'axios';

// STATE
const axiosBase = getConfiguredAxios();
const accountStore = useAccountStore();
const router = useRouter();

const userSchema = z.object({
    email: z.string().email(),
    password: z.string().min(8, { message: 'Password must be at least 8 characters long' })
});
const { shape, errors } = useShapeErrors<typeof userSchema>({
    email: '',
    password: ''
});

// METHODS
async function onFormSubmit() {
    const result = userSchema.safeParse(shape);
    if (result.success) {
    } else {
        errors.value = result.error.flatten().fieldErrors;
        return;
    }

    try {
        const response = await axiosBase.post<UserCedentials>('/account/login', shape);
        accountStore.setJwt(response.data);
        router.push(accountStore.getRedirect());
    } catch (error) {
        if (!axios.isAxiosError(error)) {
            console.log(error);
            throw error;
        }

        if (error.response?.status === 404) {
            errors.value.email = ['User with the given email does not exist'];
            return;
        }
        if (error.response?.status === 400) {
            errors.value.password = ['Incorrect password'];
            return;
        }
    }
}
</script>

<template>
    <main>
        <form class="max-w-lg mx-auto mt-6">
            <InputField class="mt-4" name="Email" type="email" v-model="shape.email" />
            <ErrorMessage v-if="errors?.email" :message="formatErrors(errors.email)" />

            <InputField class="mt-4" name="Password" type="password" v-model="shape.password" />
            <ErrorMessage v-if="errors?.password" :message="formatErrors(errors.password)" />

            <div class="flex justify-between mt-4">
                <Button class="block" name="Sign in" @click="onFormSubmit()" />
                <p class="">
                    Don't have an account?<br>
                    <router-link class="text-green-500" :to="{ name: 'userRegister' }">Register</router-link>
                </p>
            </div>
        </form>
    </main>
</template>
