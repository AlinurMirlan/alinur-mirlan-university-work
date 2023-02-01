import { createElement } from "./tools/jsxFactory";
export class CategoryList {
    getContent() {
        return createElement("div", null, ["All", ...this.props.categories].map(c => this.getCategoryButton(c)));
    }
    getCategoryButton(cat) {
        let selected = this.props.selectedCategory === undefined ? "All" : this.props.selectedCategory;
        let btnClass = selected === cat ? "btn-primary" : "btn-secondary";
        return createElement("button", { className: `btn btn-block ${btnClass}`, onclick: () => this.props.callback(cat) },
            " ",
            cat,
            " ");
    }
}
