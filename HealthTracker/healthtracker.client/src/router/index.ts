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
      name: 'Home',
      component: Home
    },
    {
      path: '/about',
      name: 'About',
      component: About
    },
    {
      path: '/diary',
      name: 'Diary',
      component: Diary
    },
    {
      path: '/planner',
      name: 'Planner',
      component: TreningsPlanner
    },
    {
      path: '/health',
      name: 'Health',
      component: Health
    },
    {
      path: '/goals',
      name: 'Goals',
      component: Goals
    },
    {
      path: '/community',
      name: 'Community',
      component: Community
    },
    {
      path: '/register',
      name: 'Register',
      component: Register
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    }
  ]
})
export default router