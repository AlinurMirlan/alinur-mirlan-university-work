import java.util.Comparator;

public class CarComparator implements Comparator<Car> {
    @Override
    public int compare(Car left, Car right) {
        int comparison = left.getBrand().compareTo(right.getBrand());
        if (comparison == 0)
            comparison = left.getModel().compareTo(right.getModel());
        if (comparison == 0)
            comparison = Integer.compare(left.getYear(), right.getYear());

        return comparison;
    }
}
