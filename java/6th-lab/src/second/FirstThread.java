package second;

public class FirstThread extends Thread {
    private final Thread _lockThread;

    public FirstThread(Thread thread) {
        this._lockThread = thread;
    }

    @Override
    public void run() {
        synchronized (this._lockThread) {
            System.out.println("Running "
                    + this.getClass().getName()
                    + " "
                    + Thread.currentThread().threadId()
                    + ".");

            System.out.println("Releasing a lock.");
            try {
                this._lockThread.wait();
                System.out.println("Resuming "
                        + this.getClass().getName()
                        + " "
                        + Thread.currentThread().threadId()
                        + ".");
                Thread.sleep(2000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }

            System.out.println(
                    this.getClass().getName()
                    + " "
                    + Thread.currentThread().threadId()
                    + " finished.");
        }
    }
}
