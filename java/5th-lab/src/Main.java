import java.util.*;

public class Main {
    public static void main(String[] args) {
        System.out.println("Collections exhibition");
        ArrayList<Car> cars = getCars();
        // 1st task
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

        // 2nd task
        HashSet<Car> anotherSet = new HashSet<>(cars);
        anotherSet.add(new Car("Dodge", "Challenger", 2022));
        anotherSet.add(new Car("Dodge", "Viper", 2003));
        HashSet<Car> result = SetOperations.intersection(set, anotherSet);
        // Intersection of 'set' and 'anotherSet' should output all entries except
        // those two we've added above.
        System.out.println("Intersection of 'set' and 'anotherSet':");
        displayEntrails(result);

        result = SetOperations.except(anotherSet, set);
        // Except of 'anotherSet' and 'set' should output entries present
        // only in 'anotherSet' and absent in 'set'.
        System.out.println("Except of 'anotherSet' and 'set':");
        displayEntrails(result);

        result = SetOperations.except(set, anotherSet);
        System.out.println("Except of 'set' and 'anotherSet':");
        displayEntrails(result);

        result = SetOperations.union(set, anotherSet);
        // Union of sets represents unique elements present in both sets.
        System.out.println("Union of 'anotherSet' and 'set':");
        displayEntrails(result);

        // 3rd task
        TreeMap<Car, String> sortedMap = new TreeMap<>(hashMap);
        displayEntrails(sortedMap.entrySet());

        TreeSet<Car> sortedSet = new TreeSet<>(set);
        displayEntrails(sortedSet);

        // 5th task
        List<Integer> numbers = new ArrayList<>();
        numbers.add(1);
        numbers.add(2);
        numbers.add(3);
        numbers.add(4);
        numbers.add(5);
        List<Integer> numbersReverse = new ArrayList<>();
        push(numbers, numbersReverse);
        // This should output 1 2 3 4 5 in reverse order.
        // Hence: 5 4 3 2 1.
        displayEntrails(numbersReverse);
    }

    // 4th task
    private static <T> void displayEntrails(Iterable<T> collection) throws IllegalArgumentException {
        if (collection == null)
            throw new IllegalArgumentException();

        System.out.println(collection.getClass().getName());
        Iterator<T> iterator = collection.iterator();
        while (iterator.hasNext()) {
            System.out.println(iterator.next().toString());
        }
        System.out.println();
    }

    // 5th task
    private static <T> void push(List<T> pushFrom, List<T> pushTo) {
        Stack<T> stack = new Stack<>();
        Iterator<T> iterator = pushFrom.iterator();
        while (iterator.hasNext()) {
            stack.add(iterator.next());
        }

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
