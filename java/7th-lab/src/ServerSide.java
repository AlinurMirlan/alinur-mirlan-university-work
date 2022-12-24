import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;
import java.time.LocalDateTime;

public class ServerSide implements Runnable {
    @Override
    public void run() {
        try {
            ServerSocket server = new ServerSocket(1111);
            Socket serverSocket = server.accept();
            BufferedWriter outputStream = new BufferedWriter(new OutputStreamWriter(serverSocket.getOutputStream()));
            BufferedReader inputReader =new BufferedReader(new InputStreamReader(serverSocket.getInputStream()));
            String inputLine = null;
            while ((inputLine = inputReader.readLine()) != null) {
                System.out.println("Server: " + inputLine);
                outputStream.write(LocalDateTime.now().toString());
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
