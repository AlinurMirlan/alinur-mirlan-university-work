package Species;
import Colorful.Color;
import Floral.Flower;

public class Rose extends Flower {
    public Rose(String name, String type, int amountOfPetals) {
        super(name, type, amountOfPetals);
    }

    public Petal getPetal() {
        return super.petal;
    }
    public Color getProtectedColorInterface() { return super.petal.getColor(); }
    public Color getInnerClassInterface() { return super.petal.getInnerColor(); }
}
