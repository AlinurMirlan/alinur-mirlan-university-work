import java.io.Serializable;

public class Student implements Serializable {
    public Student(int id, String fullName) {
        setId(id);
        setFullName(fullName);
    }
    private String fullName;
    private int id;

    public String getFullName() {
        return fullName;
    }

    public void setFullName(String fullName) {
        if (fullName == null)
            throw new IllegalArgumentException();

        this.fullName = fullName;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        if (id < 0)
            throw new IllegalArgumentException();

        this.id = id;
    }

    @Override
    public String toString() {
        return "Student{" +
                "fullName='" + fullName + '\'' +
                ", id=" + id +
                '}';
    }
}
