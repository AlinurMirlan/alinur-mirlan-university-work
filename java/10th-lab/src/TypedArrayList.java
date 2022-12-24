import java.util.ArrayList;

public class TypedArrayList {
    private final ArrayList list;
    private Class listClass = null;

    public TypedArrayList() {
        this.list = new ArrayList();
    }

    public void add(Object object) throws ClassCastException {
        if (this.listClass == null)
            this.listClass = object.getClass();

        if (this.listClass.isInstance(object)) {
            this.list.add(object);
        } else {
            throw new ClassCastException();
        }

    }

    public void add(int index, Object object) throws IndexOutOfBoundsException {
        if (this.listClass == null)
            this.listClass = object.getClass();

        if (this.listClass.isInstance(object)) {
            if (this.list.size() - 1 >= index) {
                this.list.add(object);
            }
            else {
                throw new IndexOutOfBoundsException();
            }
        }
    }

    public Object get(int index) {
        if (this.list.size() - 1 < index)
            throw new IndexOutOfBoundsException();

        return this.list.get(index);
    }
}
