// default objects have to be named, while the others imported
// with their module-s names.
import calcTaxAndSum, { calculateTax } from "./tax";
// If you want to import more than 1 object from a module,
// separate them with a colon between the curly braces.
import { printDetails, applyDiscount } from "./utils"



// let Product = function(name, price) {
//     this.name = name;   
//     this.price = price;
// }
// // We're adding the toString method to the constructor function's prototype,
// // so that further prototypes in the chain will have access to it.
// Product.prototype.toString = function() {
//     return `toString: Name: ${this.name}, Price: ${this.price}`;
// }
// Product.process = (...products) => products.forEach(p => console.log(p.toString()));
// Product.process(new Product("bun", 1), new Product("cheese", 4));

// let TaxedProduct = function(name, price, taxRate) {
//     // Calling the product function with TaxedProduct as its this,
//     // which effectively adds two new properties to TaxedProduct
//     Product.call(this, name, price);
//     this.taxRate = taxRate;
// }
// Object.setPrototypeOf(TaxedProduct.prototype, Product.prototype);
// // Seems like constructor function's prototype property refers to the container object
// // created with the new expression.
// TaxedProduct.prototype.getPriceIncTax = function() {
//     return Number(this.price) * this.taxRate;
// }
// TaxedProduct.prototype.toString = function() {
//     let chainResult = Product.prototype.toString.call(this);
//     return `${chainResult}, Tax: ${this.getPriceIncTax()}`;
// }

// let hat = new TaxedProduct("Hat", 100, 1.2);
// let boots = new Product("Boots", 100);
// console.log(`${hat.toString()}\n${boots.toString()}`);
// function getProductIterator() {
//     const hat = new Product("Hat", 100);
//     const boots = new Product("Boots", 100);
//     const umbrella = new Product("Umbrella", 100);
//     let lastVal;
//     return {
//         // Closure. State of the function-object will be kept in memory since the
//         // return object references the function-object(lastVal variable), and is able
//         // to modify it.
//         next() {
//             switch (lastVal) {
//                 case undefined:
//                     lastVal = hat;
//                     return { value: hat, done: false };
//                 case hat:
//                     lastVal = boots;
//                     return { value: boots, done: false};
//                 case boots:
//                     lastVal = umbrella;
//                     return { value: umbrella, done: false};
//                 case umbrella:
//                     return { value: undefined, done: true};
//             }
//         }
//     }
// }


// classes, inheritance and static properties are just syntactic sugars that
// will ultimately convert to constructor functions and prototype chaining.
class Product {
    constructor(name, price) {
        this.id = Symbol();
        this.name = name;
        this.price = price;
    }

    toString() {
        return `Name: ${this.name}, Price: ${this.price}`;
    }
}

class TaxedProduct extends Product {
    constructor(name, price, taxRate) {
        super(name, price);
        this.taxRate = taxRate;
    }

    getPriceIncTax() {
        return Number(this.price) * this.taxRate;
    }

    toString() {
        let chainResult = Product.prototype.toString.call(this);
        return `${chainResult}, Tax: ${this.getPriceIncTax()}`;
    }

    static process(...products) {
        products.forEach(p => console.log(p.toString()));
    }
}

let product = new Product("Hat", 100);
applyDiscount(product, 10);
let taxedPrice = calculateTax(product.price);
printDetails(product);
let products = [new Product("Hat", 100), new Product("Boots", 120)];
let result = calcTaxAndSum(...products.map(p => p.price));
console.log(result);