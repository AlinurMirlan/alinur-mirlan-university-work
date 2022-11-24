import java.time.LocalDateTime;
import java.util.Objects;

public class Car implements Comparable<Car> {
    private String _model;
    private String _brand;
    private int _year;

    Car(String brand, String model, int year) {
        setBrand(brand);
        setModel(model);
        setYear(year);
    }

    public String getModel() {
        return _model;
    }
    private void setModel(String model) {
        if (model.isBlank())
            throw new IllegalArgumentException();

        this._model = model;
    }

    public String getBrand() {
        return _brand;
    }
    private void setBrand(String brand) {
        if (brand.isBlank())
            throw new IllegalArgumentException();

        this._brand = brand;
    }

    public int getYear() {
        return _year;
    }
    private void setYear(int year) {
        if (year < 1900 && year > LocalDateTime.now().getYear())
            throw new IllegalArgumentException();

        this._year = year;
    }

    @Override
    public String toString() {
        return "Car{" +
                "_brand='" + _brand + '\'' +
                ", _model='" + _model + '\'' +
                ", _year=" + _year +
                '}';
    }

    @Override
    public int compareTo(Car otherCar) {
        int comparison = Integer.compare(this._year, otherCar._year);
        if (comparison == 0)
            comparison = this._brand.compareTo(otherCar._brand);
        if (comparison == 0)
            comparison = this._model.compareTo(otherCar._model);

        return comparison;
    }

    @Override
    public boolean equals(Object otherCar) {
        if (this == otherCar) return true;
        if (otherCar == null || getClass() != otherCar.getClass()) return false;
        Car car = (Car) otherCar;
        return _year == car._year && _model.equals(car._model) && _brand.equals(car._brand);
    }

    @Override
    public int hashCode() {
        return Objects.hash(_model, _brand, _year);
    }
}
