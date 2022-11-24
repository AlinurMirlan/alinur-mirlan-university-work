package third;

public class CounterThread extends Thread {
    private final long _id;

    public CounterThread(long id) {
        this._id = id;
    }

    @Override
    public void run() {
        System.out.println("Running "
                + this.getClass().getName()
                + " "
                + this._id
                + ".");
        long counter = this._id;
        while (counter > 0)
            counter--;

        System.out.println(
                this.getClass().getName()
                + " "
                + this._id
                + " finished.");
    }
}
