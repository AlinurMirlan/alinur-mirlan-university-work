import { SportsProduct, Sport } from "./product";
import { Cart } from "./cart";
import { sizeFormatter, costFormatter, writeMessage } from "./formatters";
import debug from "debug";
import chalk from "chalk";

let kayak = new SportsProduct(1, "Kayak", 275, Sport.Watersports);
let hat = new SportsProduct(2, "Hat", 22, Sport.Running, Sport.Watersports);
let ball = new SportsProduct(3, "Soccer Ball", 19.50, Sport.Soccer);

let cart = new Cart("Bob");
cart.addProduct(kayak, 1);
cart.addProduct(hat, 1);
cart.addProduct(hat, 2);

console.log(`Cart has ${cart.itemCount} products`);
console.log(`Cart value is $${cart.totalPrice.toFixed(2)}`);
sizeFormatter("Cart", cart.itemCount);
costFormatter("Cart", `${cart.totalPrice}`);
// writeMessage("Test message");

// let db = debug("Example App");
// db.enabled = true;
// db("Message: %0", "Test Message");

console.log(chalk.greenBright("Formatted message"));