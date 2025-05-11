<script setup lang="ts">
	import AppHeader from '@/components/AppHeader.vue';
	import { RouterView } from 'vue-router';
</script>

<template>
	<v-app>
		<AppHeader />
		<RouterView />
	</v-app>
</template>
