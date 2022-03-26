/*
 * ATTENTION: The "eval" devtool has been used (maybe by default in mode: "development").
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
/******/ (function() { // webpackBootstrap
/******/ 	var __webpack_modules__ = ({

/***/ "./wwwroot/_dist/Web/ChangePassword/ChangePassword.js":
/*!************************************************************!*\
  !*** ./wwwroot/_dist/Web/ChangePassword/ChangePassword.js ***!
  \************************************************************/
/***/ (function() {

eval("/*import { WindowEvents } from \"@drozdik.m/window-events\";\r\nimport { Form } from \"@drozdik.m/form\";\r\nimport { NativeFormInput } from \"@drozdik.m/form/dist/inputs/NativeFormInput\";\r\nimport { FormValidation_MustBeEmail } from \"@drozdik.m/form/dist/validation/FormValidation_MustBeEmail\";\r\nimport { FormValidation_MustHaveValue } from \"@drozdik.m/form/dist/validation/FormValidation_MustHaveValue\";\r\nimport { FormValidation_MustHaveSameValueAs } from \"@drozdik.m/form/dist/validation/formValidation_MustHaveSameValueAs\";\r\n\r\ndeclare let lang_emailWrongFormat: string;\r\ndeclare let lang_emailNotEmpty: string;\r\ndeclare let lang_passwordNotEmpty: string;\r\ndeclare let lang_passwordNotSame: string;\r\n\r\nWindowEvents.OnDOMReady.Add(function ()\r\n{\r\n    let form = new Form(\"registerForm\");\r\n\r\n    let email = new NativeFormInput(document.getElementById(\"email\"));\r\n    email.AddValidation(new FormValidation_MustHaveValue(email, lang_emailNotEmpty));\r\n    email.AddValidation(new FormValidation_MustBeEmail(email, lang_emailWrongFormat));\r\n\r\n    let password = new NativeFormInput(document.getElementById(\"password\"));\r\n    password.AddValidation(new FormValidation_MustHaveValue(password, lang_passwordNotEmpty));\r\n\r\n    let passwordConfirm = new NativeFormInput(document.getElementById(\"passwordConfirm\"));\r\n    passwordConfirm.AddValidation(new FormValidation_MustHaveSameValueAs(password, passwordConfirm, lang_passwordNotSame));\r\n\r\n    form.AddInput(email);\r\n    form.AddInput(password);\r\n    form.AddInput(passwordConfirm);\r\n\r\n    form.OnValidSubmit.Add(function ()\r\n    {\r\n        form.Submit();\r\n    });\r\n});*/ \r\n\n\n//# sourceURL=webpack://@drozdik.m/user-pages/./wwwroot/_dist/Web/ChangePassword/ChangePassword.js?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = {};
/******/ 	__webpack_modules__["./wwwroot/_dist/Web/ChangePassword/ChangePassword.js"]();
/******/ 	
/******/ })()
;