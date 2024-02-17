import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import Vueform from '@vueform/vueform'
import vueformConfig from './../vueform.config'

createApp(App).use(Vueform, vueformConfig).use(router).mount('#app')
