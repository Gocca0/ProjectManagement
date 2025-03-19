import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Home',
      component: () => import('@/views/Home.vue')
    },
    {
      path: '/employees',
      name: 'Employees',
      component: () => import('@/views/Employees.vue')
    },
    {
      path: '/projects',
      name: 'Projects',
      component: () => import('@/views/Projects.vue')
    },
    {
      path: '/tasks',
      name: 'Tasks',
      component: () => import('@/views/Tasks.vue')
    },
  ],
})

export default router
