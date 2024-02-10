import {createRouter, createWebHistory} from 'vue-router'
import Home from '../components/pages/home/Home.vue'
import About from '../components/pages/about/About.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      component: Home
    },
    {
      path: '/about',
      component: About
    }
  ]
})
export default router