import { reactive, ref } from 'vue';
import type z from 'zod';

export function useShapeErrors<TUser extends z.ZodTypeAny>(userShape: z.infer<TUser>) {
    type UserShape = typeof userShape;
    type UserErrors = Partial<Record<keyof UserShape, string[]>>;

    const shape: UserShape = reactive(userShape);
    let errors = ref<UserErrors>({});
    return { shape, errors };
}
