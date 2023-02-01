import { createElement } from "./tools/jsxFactory";
import { ProductItem } from "./productItem";
import { CategoryList } from "./categoryList";
export class ProductList {
    getContent() {
        return createElement("div", { className: "container-fluid" },
            createElement("div", { className: "row" },
                createElement("div", { className: "col-3 p-2" },
                    createElement(CategoryList, { categories: this.props.categories, selectedCategory: this.props.selectedCategory, callback: this.props.filterCallack })),
                createElement("div", { className: "col-9 p-2" }, this.props.products.map(p => createElement(ProductItem, { product: p, callback: this.props.addToOrderCallback })))));
    }
}
