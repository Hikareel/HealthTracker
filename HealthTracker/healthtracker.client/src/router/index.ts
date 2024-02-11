import {createRouter, createWebHistory} from 'vue-router'
import Home from '../components/pages/home/Home.vue'
import About from '../components/pages/about/About.vue'
import Diary from '../components/pages/diary/Diary.vue'
import TreningsPlanner from '../components/pages/treningsPlanner/TreningsPlanner.vue'
import Health from '../components/pages/health/Health.vue'
import Goals from '../components/pages/goals/Goals.vue'
import Community from '../components/pages/community/Community.vue'
import Register from '../components/pages/register/Register.vue'
import Login from '../components/pages/login/Login.vue'

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