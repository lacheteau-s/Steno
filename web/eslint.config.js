// @ts-check

import eslint from '@eslint/js';
import tseslint from 'typescript-eslint';
import vuelint from 'eslint-plugin-vue';
import prettierRecommended from 'eslint-plugin-prettier/recommended';

export default tseslint.config(
	{
		name: 'app/files-to-ignore',
		ignores: ['**/dist/**']
	},
	eslint.configs.recommended,
	tseslint.configs.recommendedTypeChecked,
	vuelint.configs['flat/recommended'],
	{
		languageOptions: {
			parserOptions: {
				parser: tseslint.parser,
				extraFileExtensions: ['.vue'],
				projectService: true,
				tsconfigRootDir: import.meta.dirname
			}
		}
	},
	prettierRecommended
);
