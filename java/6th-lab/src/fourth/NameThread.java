package fourth;

public class NameThread extends Thread {
    private final String title;
    private final int threadsCount;
    private final int order;

    public NameThread(String title, int order, int threadsCount) {
        this.title = title;
        this.threadsCount = threadsCount;
        this.order = order;
    }

    @Override
    public void run() {
        String name = title + " #" + order;
        System.out.print(name + " ");
        if (order == threadsCount) {
            System.out.println();
        }
    }
}
