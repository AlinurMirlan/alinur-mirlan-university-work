import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.regex.Pattern;
import java.util.stream.Stream;

public class Main {
    public static void main(String[] args) throws IOException {
        String filePath = "C:\\Alinur\\university-work\\java\\8th-lab\\src\\file.txt";
        StreamDecorator decorator = null;
        try {
            decorator = new StreamDecorator(new FileReader(filePath));
        } catch (IOException e) {
            System.out.println("File is not found. Please, make sure the path is correct");
            System.exit(-1);
        }

        String line = null;
        while ((line = decorator.readLine()) != null) {
            if (!line.equals(""))
                System.out.println(line);
        }

        BufferedReader reader = new BufferedReader(new FileReader(filePath));
        Stream<String> linesStream = reader.lines();
        Stream<String> filteredStream = linesStream.filter((row) ->
            Pattern.matches(".*\\d{2}.*", row));
        filteredStream.forEach(System.out::println);
    }
}
