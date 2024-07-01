import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import Vueform from '@vueform/vueform'
import vueformConfig from './../vueform.config'
import VueAxios from 'vue-axios'
import axios from 'axios'
import { createPinia } from 'pinia';
import 'bootstrap-icons/font/bootstrap-icons.css'

createApp(App)
  .use(Vueform, vueformConfig)
  .use(VueAxios, axios)
  .use(createPinia())
  .use(router)
.mount('#app')
