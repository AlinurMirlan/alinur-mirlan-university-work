import java.io.*;
import java.net.InetAddress;
import java.net.Socket;
import java.util.Scanner;

import static java.lang.Thread.sleep;

public class ClientSide implements Runnable {
    @Override
    public void run() {
        try {
            Socket clientSocket = new Socket(InetAddress.getLocalHost(), 1111);
            BufferedWriter outputStream = new BufferedWriter(new OutputStreamWriter(clientSocket.getOutputStream()));
            Scanner scanner = new Scanner(System.in);
            for (int i = 0; i < 3; i++) {
                outputStream.write("Time");
                sleep(5000);
            }
        } catch (IOException | InterruptedException e) {
            e.printStackTrace();
        }
    }
}
