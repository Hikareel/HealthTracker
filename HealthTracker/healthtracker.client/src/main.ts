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
