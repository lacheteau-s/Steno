<script setup lang="ts">
	import { useCounterStore } from '@/stores/counter';

	const counter = useCounterStore();

	function onClick() {
		counter.increment();
	}
</script>

<template>
	<v-main>
		<v-container class="fill-height">
			<v-row>
				<v-col cols="12" class="d-flex justify-center align-center">About content</v-col>
				<v-col cols="12" class="d-flex justify-center align-center">
					<v-btn @click="onClick">Clicked: {{ counter.count }}</v-btn>
				</v-col>
			</v-row>
		</v-container>
	</v-main>
</template>
