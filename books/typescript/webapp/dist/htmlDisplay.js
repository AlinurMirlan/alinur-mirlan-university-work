import { createElement } from "./tools/jsxFactory";
import { ProductList } from "./productList";
export class HtmlDisplay {
    constructor() {
        this.addToOrder = (product, quantity) => {
            this.props.dataSource.order.addProduct(product, quantity);
            this.updateContent();
        };
        this.selectCategory = (selected) => {
            this.selectedCategory = selected === "All" ? undefined : selected;
            this.updateContent();
        };
        this.containerElem = document.createElement("div");
    }
    async getContent() {
        await this.updateContent();
        return this.containerElem;
    }
    async updateContent() {
        let products = await this.props.dataSource.getProducts("id", this.selectedCategory);
        let categories = await this.props.dataSource.getCategories();
        this.containerElem.innerHTML = "";
        let content = createElement("div", null,
            createElement(ProductList, { products: products, categories: categories, selectedCategory: this.selectedCategory, addToOrderCallback: this.addToOrder, filterCallack: this.selectCategory }));
        this.containerElem.appendChild(content);
    }
}
