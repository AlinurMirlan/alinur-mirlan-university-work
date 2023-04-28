<script setup lang="ts">
import InputField from '@/components/form/Field.vue';
import Select from '@/components/form/Select.vue';
import Button from '@/components/Button.vue';
import z from 'zod';
import ErrorMessage from '@/components/form/ErrorMessage.vue';
import { useAccountStore } from '@/stores/account';
import { getJwtConfiguredAxios } from '@/assets/axios.js';
import type { UserCedentials } from '@/assets/types/types.js';
import { formatErrors } from '@/assets/errorFormatter';
import { useShapeErrors } from '@/composables/userErrors';
import { useRouter } from 'vue-router';

// STATE
const router = useRouter();
const accountStore = useAccountStore();
const axios = getJwtConfiguredAxios(accountStore.jwt.token);
const nonAlphanumericRegex = /[^a-zA-Z0-9]/;
const uppercaseRegex = /[A-Z]/;
const lowercaseRegex = /[a-z]/;
let userSchema = z
    .object({
        email: z.string().email(),
        password: z.string().min(8, { message: 'Password must be at least 8 characters long' })
            .regex(nonAlphanumericRegex, 'Password must contain at least one non-alphanumeric character')
            .regex(uppercaseRegex, 'Password must contain at least one uppercase letter')
            .regex(lowercaseRegex, 'Password must contain at least one lowercase letter'),
        confirmPassword: z
            .string()
            .min(8, { message: 'Password must be at least 8 characters long' }),
        name: z.string().min(1, { message: 'Name must contain at least 1 character' }),
        age: z.number().gt(0).lt(120).nullable(),
        sex: z.enum(['m', 'f', 'u'])
    })
    .refine((schema) => schema.password === schema.confirmPassword, {
        message: 'Passwords do not match',
        path: ['confirmPassword']
    });

const { shape, errors } = useShapeErrors<typeof userSchema>({
    email: '',
    password: '',
    confirmPassword: '',
    name: '',
    age: null,
    sex: 'u'
});

// METHODS
async function onFormSubmit() {
    const result = userSchema.safeParse(shape);
    if (!result.success) {
        errors.value = result.error.flatten().fieldErrors;
        if (!errors.value.email) {
            await setEmailErrorIfItsAlreadyInUse();
        }
        return;
    }

    if (await setEmailErrorIfItsAlreadyInUse()) {
        return;
    }

    const { confirmPassword, ...userDto } = shape;
    try {
        const response = await axios.post<UserCedentials>('/account/register', userDto);
        accountStore.setJwt(response.data);
    } catch (error) {
        console.log(error);
    }

    const redirect = accountStore.getRedirect();
    router.push(redirect);
}
async function setEmailErrorIfItsAlreadyInUse() {
    const doesExist = await doesEmailExist(shape.email);
    if (doesExist) {
        errors.value.email = ['Email is already taken'];
    }

    return doesExist;
}
async function doesEmailExist(email: string) {
    const repsonse = await axios.get('/account/present', {
        params: {
            email
        }
    });

    return repsonse.data as boolean;
}
</script>

<template>
    <main>
        <form class="max-w-lg mx-auto mt-6">
            <InputField class="mt-4" name="Email" type="email" v-model="shape.email" />
            <ErrorMessage v-if="errors.email" :message="formatErrors(errors.email)" />

            <InputField class="mt-4" name="Password" type="password" v-model="shape.password" />
            <ErrorMessage v-if="errors.password" :message="formatErrors(errors.password)" />
            <InputField
                class="mt-4"
                name="Confirm Password"
                placeholder="Password"
                type="password"
                v-model="shape.confirmPassword"
            />
            <ErrorMessage
                v-if="errors?.confirmPassword"
                :message="formatErrors(errors.confirmPassword)"
            />

            <InputField class="mt-4" name="Name" type="text" v-model="shape.name" />
            <ErrorMessage v-if="errors.name" :message="formatErrors(errors.name)" />

            <InputField class="mt-4" name="Age" type="number" v-model.number="shape.age" />
            <ErrorMessage v-if="errors.age" :message="formatErrors(errors.age)" />

            <Select
                v-model="shape.sex"
                class="mt-4"
                :options="[
                    [{ name: 'Male', value: 'm' }, false],
                    [{ name: 'Female', value: 'f' }, false],
                    [{ name: 'Unknown', value: 'u' }, true]
                ]"
                label="Sex"
            />

            <div class="flex justify-between mt-4">
                <Button class="" name="Sign in" @click="onFormSubmit()" />
                <p class="">
                    Have an account?<br>
                    <router-link class="text-green-500" :to="{ name: 'userLogin' }">Log in</router-link>
                </p>
            </div>
        </form>
    </main>
</template>
