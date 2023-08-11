/* eslint-env node */
module.exports = {
  extends: [
    "eslint:recommended",
    "plugin:react-hooks/recommended",
    "plugin:@typescript-eslint/recommended",
    // "plugin:@typescript-eslint/recommended-type-checked",
    "plugin:@typescript-eslint/stylistic",
    "plugin:@typescript-eslint/strict",
  ],
  parser: "@typescript-eslint/parser",
  // parserOptions: {
  //   parser: "@typescript-eslint/parser",
  //   project: ["./tsconfig.json"],
  // },
  plugins: ["@typescript-eslint"],
  rules: {
    "@typescript-eslint/no-unsafe-member-access": 0,
  },
  root: true,
  ignorePatterns: [],
};
