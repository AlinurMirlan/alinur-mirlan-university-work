export class OrderLine {
    constructor(product, quantity) {
        this.product = product;
        this.quantity = quantity;
    }
    get total() {
        return this.product.price * this.quantity;
    }
}
export class Order {
    constructor(initialLines) {
        this.lines = new Map();
        if (initialLines) {
            initialLines.forEach(line => this.lines.set(line.product.id, line));
        }
    }
    addProduct(prod, quantity) {
        if (this.lines.has(prod.id)) {
            if (quantity === 0) {
                this.removeProduct(prod.id);
            }
            else {
                this.lines.get(prod.id).quantity += quantity;
            }
        }
        else {
            this.lines.set(prod.id, new OrderLine(prod, quantity));
        }
    }
    removeProduct(id) {
        this.lines.delete(id);
    }
    get orderLines() {
        return [...this.lines.values()];
    }
    get productCount() {
        return [...this.lines.values()].reduce((total, line) => total += line.quantity, 0);
    }
    get total() {
        return [...this.lines.values()].reduce((total, line) => total += line.total, 0);
    }
}
