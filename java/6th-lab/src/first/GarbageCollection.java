package first;

public class GarbageCollection implements Runnable {
    @Override
    public void run() {
        System.out.println("Running garbage collector.");
        System.gc();
        System.runFinalization();
        System.out.println("Garbage collection finished.");
    }
}
