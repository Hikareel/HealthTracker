import { useUserStore } from './../store/account/auth';
import {createRouter, createWebHistory} from 'vue-router'
import Home from '../components/views/home/Home.vue'
import About from '../components/views/about/About.vue'
import Diary from '../components/views/diary/Diary.vue'
import TreningsPlanner from '../components/views/treningsPlanner/TreningsPlanner.vue'
import Health from '../components/views/health/Health.vue'
import Goals from '../components/views/goals/Goals.vue'
import Community from '../components/views/community/Community.vue'
import Register from '../components/views/account/register/Register.vue'
import Login from '../components/views/account/login/Login.vue'
import NewPass from '../components/views/account/new_pass/NewPass.vue'
import PassReset from '../components/views/account/pass_reset/PassReset.vue'
import LoginSuccess from '../components/views/account/login/components/LoginSuccess.vue'

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
    },
    {
      path: '/login-success',
      name: 'LoginSuccess',
      component: LoginSuccess
    },
    {
      path: '/login/pass-reset',
      name: 'Reset Password',
      component: PassReset
    },
    {
      path: '/login/new-pass',
      name: 'New Password',
      component: NewPass
    },
    {
      path: '/logout',
      name: 'Logout',
      beforeEnter: (_to, _from, next) => {
        localStorage.removeItem("user");
        const userStore = useUserStore();
        userStore.updateUserData();
        next('/login')
      },
      redirect: ''
    }
  ]
})
export default router