"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const calc_1 = require("./calc");
let printMessage = (msg) => console.log(`Message: ${msg}`);
let message = ("Hello, Typescript");
printMessage(message);
debugger;
let total = (0, calc_1.sum)(100, 200, 300);
console.log(`Total: ${total}`);
//# sourceMappingURL=index.js.map