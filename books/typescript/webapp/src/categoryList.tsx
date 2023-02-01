import { createElement } from "./tools/jsxFactory";

export class CategoryList {
    props: {
        categories: string[],
        selectedCategory: string,
        callback: (selected: string) => void
    }

    getContent(): HTMLElement {
        return  <div className="d-flex flex-column">
                    { ["All", ...this.props.categories].map(c => this.getCategoryButton(c)) }
                </div>
    }

    getCategoryButton(cat?: string): HTMLElement {
        let selected = this.props.selectedCategory === undefined ? "All": this.props.selectedCategory;
        let btnClass = selected === cat ? "btn-primary": "btn-secondary";
        return  <button className={ `btn btn-block ${btnClass} mb-1` } onclick={ () => this.props.callback(cat) }> { cat } </button>;
    }
}

