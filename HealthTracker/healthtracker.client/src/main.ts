import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import Vueform from '@vueform/vueform'
import vueformConfig from './../vueform.config'
import VueAxios from 'vue-axios'
import axios from 'axios'
import 'bootstrap-icons/font/bootstrap-icons.css'

createApp(App)
  .use(Vueform, vueformConfig)
  .use(VueAxios, axios)
  .use(router)
.mount('#app')

// import { createApp } from 'vue'
// import App from './App.vue'
// import router from './router'
// import Vueform from '@vueform/vueform'
// import vueformConfig from './../vueform.config'
// import axios from 'axios'

// // Utwórz instancję aplikacji Vue
// const app = createApp(App);

// // Użyj Vueform z konfiguracją
// app.use(Vueform, vueformConfig);

// // Ustaw Axios globalnie
// app.config.globalProperties.axios = axios;
// app.config.globalProperties.$axios = axios; // Jeśli chcesz, aby było dostępne jako $axios

// // Użyj Vue Router
// app.use(router);

// // Jeśli BootstrapVue jest dostępny dla Vue 3, użyj go tutaj
// // app.use(BootstrapVue);
// // app.use(BootstrapVueIcons);

// // Zamontuj aplikację
// app.mount('#app');
