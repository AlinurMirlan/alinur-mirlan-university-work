import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;
import java.time.LocalDateTime;

public class ServerSide implements Runnable {
    private final int port;

    public ServerSide(int port) {
        this.port = port;
    }
    @Override
    public void run() {
        BufferedReader input = null;
        PrintWriter output = null;
        ServerSocket server = null;
        Socket client = null;

        try {
            server = new ServerSocket(port);
            System.out.println("Waiting for client to join.");
            client = server.accept();
            input = new BufferedReader(new InputStreamReader(client.getInputStream()));
            output = new PrintWriter(client.getOutputStream(),true);
        } catch (IOException e) {
            e.printStackTrace();
        }
        String inputLine;
        try {
            while ((inputLine = input.readLine()) != null) {
                System.out.println("Server: " + inputLine);
                // Outputting to the client.
                output.println(LocalDateTime.now().toString());
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

        output.close();
        try {
            input.close();
            client.close();
            server.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
