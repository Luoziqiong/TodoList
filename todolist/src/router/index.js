import Vue from 'vue'
import Router from 'vue-router'
import Register from '@/pages/register.vue'
import TodoList from '@/pages/todolist.vue'
import Login from '@/pages/login.vue'
Vue.use(Router)

export default new Router({
  routes: [{
      path: '/',
      redirect: '/login',
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/register',
      name: 'register',
      component: Register
    },

    {
      path: '/todolist',
      name: 'todolist',
      component: TodoList
    }
  ]
})
