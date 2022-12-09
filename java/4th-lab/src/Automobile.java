public class Automobile implements Tradable {
    private double price;
    @Override
    public double getPrice() {
        return this.price;
    }

    @Override
    public double setPrice(double price) {
        if (price < 0) {
            throw new IllegalArgumentException();
        }

        this.price = price;
        return this.price;
    }

    @Override
    public String toString() {
        return "Automobile{" +
                "price=" + price +
                '}';
    }

    public static class Truck {
        public double groundClearance;
        public Truck(double groundClearance) {
            this.groundClearance = groundClearance;
        }

        @Override
        public String toString() {
            return "Truck{" +
                    "groundClearance=" + groundClearance +
                    '}';
        }
    }
}
