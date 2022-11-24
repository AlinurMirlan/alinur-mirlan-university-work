import java.util.*;

public class Main {
    public static void main(String[] args) {
        System.out.println("Collections exhibition");
        ArrayList<Car> cars = getCars();
        HashSet<Car> set = new HashSet<>(cars);
        displayEntrails(set);

        PriorityQueue<Car> priorityQueue = new PriorityQueue<>();
        priorityQueue.addAll(cars);
        displayEntrails(priorityQueue);

        Stack<Car> stack = new Stack<>();
        stack.addAll(cars);
        displayEntrails(stack);

        HashMap<Car, String> hashMap = new HashMap<>();
        for (Car car : cars) {
            hashMap.put(car, generateName());
        }
        displayEntrails(hashMap.entrySet());

        TreeMap<Car, String> sortedMap = new TreeMap<>(hashMap);
        displayEntrails(sortedMap.entrySet());

        TreeSet<Car> sortedSet = new TreeSet<>(set);
        displayEntrails(sortedSet);
    }

    private static <T> void displayEntrails(Iterable<T> collection) throws IllegalArgumentException {
        if (collection == null)
            throw new IllegalArgumentException();

        System.out.println(collection.getClass().getName());
        Iterator<T> iterator = collection.iterator();
        while (iterator.hasNext()) {
            System.out.println(iterator.next().toString());
        }
    }

    private static <T> void push(List<T> pushFrom, List<T> pushTo) {
        Stack<T> stack = new Stack<>();
        stack.addAll(pushFrom);
        while (!stack.isEmpty()) {
            pushTo.add(stack.pop());
        }
    }

    private static ArrayList<Car> getCars() {
        ArrayList<Car> cars = new ArrayList<>();
        cars.add(new Car("Tesla", "Model X", 2022));
        cars.add(new Car("Honda", "Civic Type R", 2017));
        cars.add(new Car("Mazda", "Miata", 2012));
        cars.add(new Car("Ford", "Mustang", 2019));
        cars.add(new Car("Tesla", "Model Y", 2021));
        cars.add(new Car("Tesla", "Model S", 2018));
        cars.add(new Car("Koenigsegg", "Jesko", 2019));
        cars.add(new Car("Ford", "Focus", 2012));
        cars.add(new Car("Honda", "Civic", 2022));
        cars.add(new Car("Ford", "Focus", 2012));
        cars.add(new Car("Tesla", "Model X", 2022));

        return cars;
    }

    private static String generateName() {
        char[] alphabet = "abcdefghijklmnopqrstuvwxyz".toCharArray();
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        int nameLength = 3 + random.nextInt(7);
        for (int i = 0; i < nameLength; i++) {
            builder.append(alphabet[random.nextInt(alphabet.length)]);
        }

        builder.deleteCharAt(0);
        builder.insert(0, Character.toUpperCase(builder.charAt(0)));
        return builder.toString();
    }
}
