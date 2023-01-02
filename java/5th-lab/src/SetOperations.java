import java.util.HashSet;

public class SetOperations {
    public static <T> HashSet<T> intersection(HashSet<T> left, HashSet<T> right) {
        HashSet<T> resultingSet = new HashSet<>();
        for (T element : left) {
            if (right.contains(element)) {
                resultingSet.add(element);
            }
        }

        return resultingSet;
    }

    public static <T> HashSet<T> union(HashSet<T> left, HashSet<T> right) {
        HashSet<T> resultingSet = new HashSet<>();
        resultingSet.addAll(left);
        resultingSet.addAll(right);

        return resultingSet;
    }

    public static <T> HashSet<T> except(HashSet<T> left, HashSet<T> right) {
        HashSet<T> resultingSet = new HashSet<>(left);
        for (T element : left) {
            if (right.contains(element))
                resultingSet.remove(element);
        }

        return resultingSet;
    }

    public static <T> boolean isSubset(HashSet<T> left, HashSet<T> right) {
        return right.containsAll(left);
    }

    public static <T> boolean isSuperset(HashSet<T> left, HashSet<T> right) {
        return left.containsAll(right);
    }
}
