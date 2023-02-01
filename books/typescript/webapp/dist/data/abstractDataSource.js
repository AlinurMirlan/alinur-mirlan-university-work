import { Order } from "./entities";
export class AbstractDataSource {
    constructor() {
        this._products = [];
        this._categories = new Set();
        this.order = new Order();
        this.loading = this.getData();
    }
    async getProducts(sortProp = "id", category) {
        await this.loading;
        return this.selectProducts(this._products, sortProp, category);
    }
    async getData() {
        this._products = [];
        this._categories.clear();
        const rawData = await this.loadProducts();
        rawData.forEach(p => {
            this._products.push(p);
            this._categories.add(p.category);
        });
    }
    selectProducts(prods, sortProp, category) {
        return prods.filter(p => category === undefined || p.category === category)
            .sort((p1, p2) => p1[sortProp] < p2[sortProp] ? -1 : p1[sortProp] > p2[sortProp] ? 1 : 0);
    }
    async getCategories() {
        await this.loading;
        return [...this._categories.values()];
    }
}
