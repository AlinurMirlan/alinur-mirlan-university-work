public class Main {
    public static void main(String[] args) {
        if (args.length < 1) {
            System.out.println("Please, specify the port of the server");
            System.exit(0);
        }

        Thread server = new Thread(new ServerSide(Integer.parseInt(args[0])));
        server.start();
    }
}
