package Last;

public class Garage {
    private final Car car;

    public Garage(float carFuel) {
        car = new Car(carFuel);
    }

    public Fillable getCar() {
        return car;
    }

    public static class Car implements Fillable {
        private float fuel;

        public Car(float fuel) {
            if (fuel < 0)
                throw new IllegalArgumentException();

            this.fuel = fuel;
        }

        public float getFuel() {
            return fuel;
        }

        @Override
        public boolean isFull() { return this.fuel == 100; }

        @Override
        public void fill() { this.fuel = 100; }

        @Override
        public void empty() { this.fuel = 0; }

        @Override
        public String toString() {
            return "Car{" +
                    "fuel=" + fuel +
                    '}';
        }
    }
}
