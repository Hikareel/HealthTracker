import {createRouter, createWebHistory} from 'vue-router'
import Home from '../components/views/home/Home.vue'
import About from '../components/views/about/About.vue'
import Diary from '../components/views/diary/Diary.vue'
import TreningsPlanner from '../components/views/treningsPlanner/TreningsPlanner.vue'
import Health from '../components/views/health/Health.vue'
import Goals from '../components/views/goals/Goals.vue'
import Community from '../components/views/community/Community.vue'
import Register from '../components/views/register/Register.vue'
import Login from '../components/views/login/Login.vue'

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
    },
    {
      path: '/diary',
      component: Diary
    },
    {
      path: '/planner',
      component: TreningsPlanner
    },
    {
      path: '/health',
      component: Health
    },
    {
      path: '/goals',
      component: Goals
    },
    {
      path: '/community',
      component: Community
    },
    {
      path: '/register',
      component: Register
    },
    {
      path: '/login',
      component: Login
    }
  ]
})
export default router