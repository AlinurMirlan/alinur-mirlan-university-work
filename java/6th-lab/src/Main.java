import first.*;
import fourth.NameThread;
import second.*;
import third.CounterThread;

import java.util.ArrayList;
import java.util.Scanner;

public class Main {
    private static long counter = 40000000;

    public static void main(String[] args) {
        // 4th task
        Scanner scanner = new Scanner(System.in);
        int threadCount = scanner.nextInt();
        int repeatCount = scanner.nextInt();
        Object lock = new Object();
        for (int i = 0; i < threadCount; i++) {
            new NameThread(lock, "Thread " + i, repeatCount).start();
        }
/*        // 1st task
        var messageThread1 = new MessageThread();
        var garbageThread1 = new Thread(new GarbageCollection());
        var messageThread2 = new MessageThread();
        var garbageThread2 = new Thread(new GarbageCollection());
        messageThread1.start();
        garbageThread1.start();
        messageThread2.start();
        try {
            Thread.sleep(4000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        garbageThread2.start();

        // 2nd task
        var secondThread = new SecondThread();
        var firstThread = new FirstThread(secondThread);
        firstThread.start();
        secondThread.start();

        // 3rd task
        ArrayList<CounterThread> counterThreads = new ArrayList<>();
        counterThreads.add(new CounterThread(getThreadId()));
        counterThreads.add(new CounterThread(getThreadId()));
        counterThreads.add(new CounterThread(getThreadId()));
        counterThreads.add(new CounterThread(getThreadId()));
        counterThreads.add(new CounterThread(getThreadId()));
        for (var counterThread : counterThreads) {
            counterThread.start();
        }*/
    }

    private static long getThreadId() {
        return counter *= 2;
    }
}
