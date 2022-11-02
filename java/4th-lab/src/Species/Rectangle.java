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

    public double calculateNewArea(double height, double width) {
        this.height = height;
        this.width = width;
        this.area = height * width;
        return this.area;
    }

    public Square makeSquareByHeight() {
        return new Square(this.height);
    }
    public Square makeSquareByWidth() {
        Square square = makeSquareByHeight();
        square.calculateNewArea(this.width);
        return square;
    }

    public class Square {
        public Square(double dimension) {
            height = dimension;
            width = dimension;
            area = dimension * dimension;
        }

        public void calculateNewArea(double dimension) {
            height = dimension;
            width = dimension;
            area = dimension * dimension;
        }
    }
}
