export class DomDisplay {
    getContent() {
        let elem = document.createElement("h3");
        elem.innerText = this.getElementText();
        elem.classList.add("bg-primary", "text-center", "text-white", "p-2");
        return elem;
    }
    getElementText() {
        return `${this.props.products.length} Products, ` +
            `Order total: $${this.props.order.total}`;
    }
}
