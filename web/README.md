# Steno - web app

## Requirements

| Package | Version |
| ------- | ------- |
| [Node]  | >= 20   |
| [NPM]   | >= 10   |

## Getting started

1. Install dependencies:

```sh
npm install
```

2. Configure local environment (optional):
   - Create a `.env` file at the root of the project directory.
   - Add the following configuration:

```
PORT=<port-number>
```

If no valid `PORT` is specified, a default one will be assigned.

3. Compile and run locally:

```sh
npm run dev
```

4. Debug (VS Code)
    * Select `Web (Firefox)` from the `Run and Debug` panel.
    * Press `F5` or click the :arrow_forward: button.

## Build for production

1. Build the application:

```sh
npm run build
```

2. Preview build:

```sh
npm run preview
```

## Additional commands

### Code formatting ([Prettier])

```sh
npm run format
```

For formatting rules, see [.editorconfig] and [prettier.config.js].

### Linting ([ESLint])

Linting is configured to support [TypeScript] and [Vue files].
Together with the recommended [IDE setup], you should get linting errors directly in your editor.

Alternatively, you can run the linter from the command line:

```sh
npm run lint
```

Note that the linter is configured to also [report Prettier issues].

For linting configuration, see [eslint.config.js].

## Recommended IDE Setup

[VSCode] + [extensions]

[Node]: https://nodejs.org/en
[NPM]: https://www.npmjs.com/
[Prettier]: https://prettier.io/
[.editorconfig]: .editorconfig
[prettier.config.js]: prettier.config.js
[ESLint]: https://eslint.org/
[TypeScript]: https://typescript-eslint.io/
[Vue files]: https://eslint.vuejs.org/
[IDE setup]: #recommended-ide-setup
[report Prettier issues]: https://github.com/prettier/eslint-plugin-prettier
[eslint.config.js]: eslint.config.js
[VSCode]: https://code.visualstudio.com/
[extensions]: .vscode/extensions.json
