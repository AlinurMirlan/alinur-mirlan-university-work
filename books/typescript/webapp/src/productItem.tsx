import { createElement } from "./tools/jsxFactory";
import { Product } from "./data/entities";
import { addClass } from "./decorators";

export class ProductItem {
    private quantity: number = 1;

    props: {
        product: Product,
        callback: (product: Product, quantity: number) => void
    }

    @addClass("select", "bg-info", "m-1")
    getContent(): HTMLElement {
        return <div className="card m-1 p-1 bg-light">
                    <h4>
                        { this.props.product.name }
                        <span className="badge rounded-pill text-bg-primary float-end">
                            { this.props.product.price.toFixed(2) }
                        </span>
                    </h4>
                    <div className="card-text bg-white p-1">
                        { this.props.product.description }
                        <button className="btn btn-success btn-sm float-end" onclick={ this.handleAddToCart }>Add To Cart</button>
                        <select className="float-end m-1" onchange={ this.handleQuantityChange }>
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                        </select>
                    </div>
                </div>;
    }

    handleQuantityChange = (ev: Event): void => {
        this.quantity = Number((ev.target as HTMLSelectElement).value);
    }

    handleAddToCart = (): void => {
        this.props.callback(this.props.product, this.quantity);
    }
}