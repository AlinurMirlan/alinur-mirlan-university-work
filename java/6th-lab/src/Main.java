import first.*;
import fourth.NameThread;
import second.*;
import third.CounterThread;
import java.util.ArrayList;
import java.util.Scanner;

import static java.lang.Thread.sleep;

public class Main {
    private static long counter = 40000000;

    public static void main(String[] args) throws InterruptedException {
        // 1st task
/*        var messageThread1 = new MessageThread();
        var messageThread2 = new MessageThread();
        var garbageThread2 = new Thread(new GarbageCollection());
        messageThread1.start();
        //garbageThread1.start();
        messageThread2.start();
        try {
            sleep(4000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        garbageThread2.start();*/

/*        // 2nd task
        var secondThread = new SecondThread();
        var firstThread = new FirstThread(secondThread);
        firstThread.start();
        secondThread.start();*/

/*        // 3rd task
        ArrayList<CounterThread> counterThreads = new ArrayList<>();
        counterThreads.add(new CounterThread(getThreadId()));
        counterThreads.add(new CounterThread(getThreadId()));
        counterThreads.add(new CounterThread(getThreadId()));
        counterThreads.add(new CounterThread(getThreadId()));
        counterThreads.add(new CounterThread(getThreadId()));
        for (var counterThread : counterThreads) {
            counterThread.start();
        }*/

        // 4th task
/*        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                NameThread thread = new NameThread("thread", j + 1, 3);
                thread.start();
                thread.join();
            }
            sleep(300);
        }*/
    }

    private static long getThreadId() {
        return counter *= 2;
    }
}
