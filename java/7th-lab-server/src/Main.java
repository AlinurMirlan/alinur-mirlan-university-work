public class Main {
    public static void main(String[] args) {
        Thread server = new Thread(new ServerSide());
        server.start();
    }
}
