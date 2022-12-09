package Floral;
import Colorful.Color;

import java.util.Arrays;

public class Flower {
    public String name;
    public String type;
    protected Petal petal;

    public Flower(String name, String type, int amountOfPetals) {
        this.name = name;
        this.type = type;
        this.petal = new Petal(amountOfPetals);
    }

    protected class Petal implements Color {
        int amount = 0;

        Petal(int amount) {
            this.amount = amount;
        }

        // Since we can't really access protected methods outside the boundaries of the outer class and any other one
        // that derives from it, we might as well create another public method in the containing class that calls these
        // methods so that we'll be able to get a hold of their return values.
        public Color getColor() {
            return this;
        }

        public Color getInnerColor() {
            class InnerColor implements Color {
                private final int[] color = color();
                private final int id;

                public InnerColor(int id) {
                    this.id = id;
                }

                @Override
                public int[] color() {
                    return Color.JADE;
                }

                @Override
                public String toString() {
                    return "InnerColor{" +
                            "color=" + Arrays.toString(color) +
                            ", id=" + id +
                            '}';
                }
            }

            return new InnerColor(1);
        }

        @Override
        public int[] color() {
            return Color.CRIMSON;
        }

        @Override
        public String toString() {
            return "Petal{" +
                    "amount=" + amount +
                    '}';
        }
    }
}
