import Colorful.Color;
import Floral.Flower;
import Last.Fillables;
import Last.Garage;
import Species.Rectangle;
import Species.Rose;

import java.util.Arrays;
import java.util.LinkedList;

public class Main {
    public static void main(String[] args) {
        // First subtask
        // Interfaces can declare static fields (just like in C#)
        int[] colorRed = Color.RED;
        System.out.println(Arrays.toString(colorRed));
        Rose rose = new Rose("Rose", "Type", 12);

        // Second subtask
        // Unlike in C#, we can gain access to an instance of an inner protected class of some other publicly
        // available class, but, fairly enough, we cannot see any of its members.
        var petal = rose.getPetal();
        System.out.println(petal);

        // Third subtask
        // Getting and instance of the interface the inner protected class implements
        Color colorPetal = rose.getProtectedColorInterface();
        // Since it's a Rose object we're getting an interface from, the interface instance holds
        // base methods of the Rose object, as so does the toString()
        System.out.println(colorPetal);

        Color localColor = rose.getInnerClassInterface();
        System.out.println(localColor);

        // Fourth subtask
        Rectangle rectangle = new Rectangle(5, 10);
        Rectangle.Square square = rectangle.makeSquareByWidth();
        System.out.println(rectangle);
        rectangle.calculateNewArea(10, 5);
        System.out.println(rectangle);
        square.calculateNewArea(1);
        System.out.println(rectangle);

        // Fifth subtask. Static inner class is just like a regular nested class in C#.
        Automobile.Truck truck = new Automobile.Truck(2);
        System.out.println(truck);

        // Sixth subtask.
        LinkedList<Garage> garages = new LinkedList<>();
        garages.add(new Garage(0));
        garages.add(new Garage(100));
        garages.add(new Garage(99));
        garages.add(new Garage(39));
        garages.add(new Garage(10));
        Fillables fillables = new Fillables();
        for (Garage garage : garages) {
            fillables.AddFillable(garage.getCar());
        }

        fillables.IterateFillables();
        fillables.NullFillableAt(0);
    }
}
