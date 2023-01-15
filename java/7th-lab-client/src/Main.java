public class Main {
    public static void main(String[] args) {
        if (args.length < 2) {
            System.out.println("Please, specify the host and its port separated my a space");
            System.exit(0);
        }

        Thread server = new Thread(new ClientSide(args[0], Integer.parseInt(args[1])));
        server.start();
    }
}
