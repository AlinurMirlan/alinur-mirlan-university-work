import { AbstractDataSource } from "./abstractDataSource";
import { Product, Order, OrderLine } from "./entities";
import Axios from "axios";

const urls = {
    products: `/api/products`,
    orders: `/api/orders`,
};

export class RemoteDataSource extends AbstractDataSource {
    protected loadProducts(): Promise<Product[]> {
        return Axios.get(urls.products).then(response => response.data);
    }
    
    storeOrder(): Promise<number> {
        let orderData = {
            lines: [...this.order.orderLines.values()].map(ol => ({
                productId: ol.product.id,
                productName: ol.product.name,
                quantity: ol.quantity
            }))
        };

        return Axios.post(urls.orders, orderData).then(response => response.data.id);
    }
    
}