<script setup lang="ts">
	import { RouterLink } from 'vue-router';
</script>

<template>
	<v-app-bar title="Application">
		<template #prepend>
			<v-app-bar-nav-icon />
		</template>
		<v-spacer />
		<RouterLink to="/">
			<v-btn>Home</v-btn>
		</RouterLink>
		<RouterLink to="/about">
			<v-btn>About</v-btn>
		</RouterLink>
	</v-app-bar>
</template>
