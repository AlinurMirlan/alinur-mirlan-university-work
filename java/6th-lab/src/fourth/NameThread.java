package fourth;

public class NameThread extends Thread {
    private final Object _lock;
    private final String _name;
    private final int _repeatCounter;

    public NameThread(Object lock, String name, int repeatCounter)
    {
        this._lock = lock;
        this._name = name;
        this._repeatCounter = repeatCounter;
    }

    @Override
    public void run() {
        for (int i = 0; i < this._repeatCounter; i++) {
            synchronized (this._lock) {
                System.out.println(this._name);
            }
        }
    }
}
