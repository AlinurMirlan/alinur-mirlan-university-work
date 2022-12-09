package Last;

import java.util.ArrayList;

public class Fillables {
    private ArrayList<Fillable> fillables = new ArrayList<>();

    public Fillables() { }

    public Fillables(Iterable<Fillable> fillables) {
        for (Fillable fillable : fillables) {
            this.fillables.add(fillable);
        }
    }

    public void AddFillable(Fillable fillable) {
        this.fillables.add(fillable);
    }

    public void NullFillableAt(int index) {
        if (this.fillables.size() > index) {
            this.fillables.set(index, null);
        }
    }

    public void IterateFillables() {
        for (Fillable fillable : this.fillables) {
            System.out.println(fillable + "\n" +
                    "isFull : " + fillable.isFull() + "\n");
            fillable.fill();
            fillable.empty();
        }
    }
}
