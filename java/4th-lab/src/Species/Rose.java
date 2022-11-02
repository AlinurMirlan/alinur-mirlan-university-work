package Species;
import Floral.Flower;

public class Rose extends Flower {
    Rose(String name, String type, Petal petal) {
        super(name, type, petal);
    }

    public Petal getPetal() {
        return super.petal;
    }
}
