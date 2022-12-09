package Species;

public class Rectangle {
    private double height;
    private double width;
    private double area;

    public Rectangle(double height, double width) {
        this.height = height;
        this.width = width;
        this.area = height * width;
    }

    public void calculateNewArea(double height, double width) {
        this.height = height;
        this.width = width;
        this.area = height * width;
    }

    public Square makeSquareByHeight() {
        return new Square(this.height);
    }

    public Square makeSquareByWidth() {
        return new Square(this.width);
    }

    public Square makeSquare(double dimension) {
        Square square = new Square();
        square.calculateNewArea(dimension);
        return square;
    }

    public class Square {
        public Square() { }

        public Square(double dimension) {
            height = dimension;
            width = dimension;
            area = dimension * dimension;
        }

        public void calculateNewArea(double dimension) {
            Rectangle.this.calculateNewArea(dimension, dimension);
        }

        @Override
        public String toString() {
            return "Square{" +
                    "height=" + height +
                    ", width=" + width +
                    ", area=" + area +
                    '}';
        }
    }

    @Override
    public String toString() {
        return "Rectangle{" +
                "height=" + height +
                ", width=" + width +
                ", area=" + area +
                '}';
    }
}
