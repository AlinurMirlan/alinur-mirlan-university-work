import { createElement } from "./tools/jsxFactory";
export class ProductItem {
    constructor() {
        this.quantity = 1;
        this.handleQuantityChange = (ev) => {
            this.quantity = Number(ev.target.value);
        };
        this.handleAddToCart = () => {
            this.props.callback(this.props.product, this.quantity);
        };
    }
    getContent() {
        return createElement("div", { className: "card m-1 p-1 bg-light" },
            createElement("h4", null,
                this.props.product.name,
                createElement("span", { className: "badge badge-pill badge-primary float-right" }, this.props.product.price.toFixed(2))),
            createElement("div", { className: "card-text bg-white p-1" },
                this.props.product.description,
                createElement("button", { className: "btn btn-success btn-sm float-right", onclick: this.handleAddToCart }, "Add To Cart"),
                createElement("select", { className: "form-control-inline float-right m-1", onchange: this.handleQuantityChange },
                    createElement("option", { value: "" }, "1"),
                    createElement("option", { value: "" }, "2"),
                    createElement("option", { value: "" }, "3"))));
    }
}
