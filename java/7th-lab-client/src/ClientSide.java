import java.io.*;
import java.net.Socket;

import static java.lang.Thread.sleep;

public class ClientSide implements Runnable {
    private final String host;
    private final int port;

    public ClientSide(String host, int port) {
        this.host = host;
        this.port = port;
    }

    @Override
    public void run() {
        Socket server = null;
        BufferedReader input  = null;
        PrintWriter output = null;
        try {
            server = new Socket(host, port);
            input = new BufferedReader(new InputStreamReader(server.getInputStream()));
            output = new PrintWriter(server.getOutputStream(),true);
        } catch (IOException e) {
            e.printStackTrace();
        }

        try {
            for (int i = 0; i < 3; i++) {
                // Outputting to the server.
                output.println("Time");
                sleep(5000);
                // Taking input from the server.
                String inputLine = input.readLine();
                System.out.println(inputLine);
            }
        } catch (InterruptedException | IOException e) {
            e.printStackTrace();
        }

        output.close();
        try {
            input.close();
            server.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
