import java.io.*;
import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        ArrayList<Student> students = new ArrayList<>();
        students.add(new Student(0, "Alinur Mirlan"));
        students.add(new Student(1, "Erlan Esenglediev"));
        students.add(new Student(2, "Erlan Chkalov"));

        // Serialization
        FileOutputStream fileOutputStream = null;
        try {
            fileOutputStream = new FileOutputStream("students.ser");
        } catch (FileNotFoundException e) {
            System.out.println("File output error!");
            System.exit(-1);
        }
        try {
            ObjectOutputStream objectOutputStream = new ObjectOutputStream(fileOutputStream);
            objectOutputStream.writeObject(students);
            objectOutputStream.close();
            fileOutputStream.close();
        }
        catch (NotSerializableException e) {
            System.out.println("Object is not serializable!");
            System.exit(-1);
        } catch (IOException e) {
            System.out.println("Object output error!");
            System.exit(-1);
        }

        // Deserialization
        FileInputStream fileInputStream = null;
        String filePath = "C:\\Alinur\\university-work\\java\\9th-lab\\students.ser";
        try {
            fileInputStream = new FileInputStream(filePath);
        } catch (FileNotFoundException e) {
            System.out.println("File input error!");
            System.exit(-1);
        }
        try {
            ObjectInputStream objectInputStream = new ObjectInputStream(fileInputStream);
            ArrayList<Student> deserializedStudents = (ArrayList<Student>)objectInputStream.readObject();
            for (Student student : deserializedStudents) {
                System.out.println(student);
            }
        } catch (IOException | ClassNotFoundException e) {
            System.out.println("Deserialization error!");
            System.exit(-1);
        }

    }
}
