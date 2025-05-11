import { fileURLToPath, URL } from 'node:url';

import { defineConfig, loadEnv } from 'vite';
import vue from '@vitejs/plugin-vue';
import vueDevTools from 'vite-plugin-vue-devtools';
import vuetify, { transformAssetUrls } from 'vite-plugin-vuetify';
import fonts from 'unplugin-fonts/vite';

// https://vite.dev/config/
export default defineConfig(({ mode }) => {
	const env = loadEnv(mode, process.cwd(), '');
	const port = Number(env.PORT);

	return {
		plugins: [
			vue({
				template: { transformAssetUrls }
			}),
			vueDevTools(),
			vuetify({
				autoImport: true,
				styles: {
					configFile: 'src/styles/settings.scss'
				}
			}),
			fonts({
				google: {
					families: [
						{
							name: 'Roboto',
							styles: 'wght@100;300;400;500;700;900'
						}
					]
				}
			})
		],
		resolve: {
			alias: {
				'@': fileURLToPath(new URL('./src', import.meta.url))
			}
		},
		server: { port }
	};
});
