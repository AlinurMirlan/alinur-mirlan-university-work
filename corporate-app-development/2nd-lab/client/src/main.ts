import { createApp, watch } from 'vue';
import App from '@/App.vue';
import '@/assets/style.css';
import router from '@/router/router';
import { createPinia } from 'pinia';

const pinia = createPinia();
const app = createApp(App);
app.use(pinia);
app.use(router);
app.mount('#app');