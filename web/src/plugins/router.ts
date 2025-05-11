import HomeView from '@/views/HomeView.vue';
import { createRouter, createWebHistory } from 'vue-router';

/* eslint-disable @typescript-eslint/no-unsafe-assignment */
const routes = [
	{
		path: '/',
		name: 'home',
		component: HomeView
	},
	{
		path: '/about',
		name: 'about',
		component: () => import('@/views/AboutView.vue')
	}
];
/* eslint-enable @typescript-eslint/no-unsafe-assignment */

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes
});

export default router;
