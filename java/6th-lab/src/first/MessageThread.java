public class MessageThread extends Thread {
    MessageThread() {
        System.out.println(this.getClass().getName()
                + " "
                + Thread.currentThread().threadId()
                + " initialized.");
    }

    @Override
    public void run() {
        for (int i = 0; i < 3; i++) {
            System.out.println("Message " + i);
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    @Override
    protected void finalize() throws Throwable {
        System.out.println(this.getClass().getName()
                + " "
                + Thread.currentThread().threadId()
                + " disposed.");
    }
}
