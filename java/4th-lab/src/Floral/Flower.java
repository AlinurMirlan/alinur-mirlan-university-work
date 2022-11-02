package Floral;
import Colorful.Color;

public class Flower {
    public String name;
    public String type;
    public Petal petal;

    public Flower(String name, String type, Petal petal) {
        this.name = name;
        this.type = type;
        this.petal = petal;
    }

    protected class Petal implements Color {
        int amount = 0;
        Color color;
        Petal(int amount) {
            this.amount = amount;
        }

        @Override
        public int[] color() {
            return Color.CRIMSON;
        }

        public Color getColor() {
            return this;
        }
    }
}
