public class Main {
    public static void main(String[] args) {
        Thread client = new Thread(new ClientSide());
        Thread server = new Thread(new ServerSide());
        server.start();
        client.start();
    }
}
