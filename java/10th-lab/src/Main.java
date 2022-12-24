import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        TypedArrayList list = new TypedArrayList();
        list.add(new Student(0, "Alinur Mirlan"));
        list.add(new Student(1, "Erlan Esengeldiev"));
        try {
            list.add(1);
        } catch (ClassCastException e) {
            System.out.print("TypedArrayList can only hold objects of one type");
        }
    }
}
