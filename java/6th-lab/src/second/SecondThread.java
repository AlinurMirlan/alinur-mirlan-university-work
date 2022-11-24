package second;

public class SecondThread extends Thread {
    @Override
    public void run() {
        try {
            Thread.sleep(50);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        synchronized (this) {
            System.out.println("Running "
                    + this.getClass().getName()
                    + " "
                    + Thread.currentThread().threadId()
                    + ".");
            try {
                Thread.sleep(3000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }

            System.out.println(
                    this.getClass().getName()
                    + " "
                    + Thread.currentThread().threadId()
                    + " finished.");
            this.notifyAll();
        }
    }
}
