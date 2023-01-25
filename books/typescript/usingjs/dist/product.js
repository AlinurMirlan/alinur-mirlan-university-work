"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SportsProduct = exports.Sport = exports.Product = void 0;
class Product {
    constructor(id, name, price) {
        this.id = id;
        this.name = name;
        this.price = price;
    }
}
exports.Product = Product;
var Sport;
(function (Sport) {
    Sport[Sport["Running"] = 0] = "Running";
    Sport[Sport["Soccer"] = 1] = "Soccer";
    Sport[Sport["Watersports"] = 2] = "Watersports";
    Sport[Sport["Other"] = 3] = "Other";
})(Sport = exports.Sport || (exports.Sport = {}));
class SportsProduct extends Product {
    constructor(id, name, price, ...sportsArray) {
        super(id, name, price);
        this.id = id;
        this.name = name;
        this.price = price;
        this._sports = sportsArray;
    }
    get sports() {
        return this._sports;
    }
    usedForSport(s) {
        return this._sports.includes(s);
    }
}
exports.SportsProduct = SportsProduct;
