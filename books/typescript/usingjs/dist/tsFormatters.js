"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.writeMessage = exports.costFormatter = exports.sizeFormatter = void 0;
function sizeFormatter(thing, count) {
    writeMessage(`The ${thing} has ${count} items`);
}
exports.sizeFormatter = sizeFormatter;
function costFormatter(thing, cost) {
    if (typeof cost === "number") {
        writeMessage(`The ${thing} costs $${cost.toFixed(2)}`);
    }
    else {
        writeMessage(`The ${thing} costs $${cost}`);
    }
}
exports.costFormatter = costFormatter;
function writeMessage(message) {
    console.log(message);
}
exports.writeMessage = writeMessage;
