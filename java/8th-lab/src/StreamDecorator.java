import java.io.BufferedReader;
import java.io.IOException;
import java.io.Reader;
import java.util.regex.Pattern;

public class StreamDecorator extends BufferedReader {
    public StreamDecorator(Reader in) {
        super(in);
    }

    @Override
    public String readLine() throws IOException {
        String line = super.readLine();
        if (line != null) {
            if (Pattern.matches(".*\\d{2}.*", line)) {
                return line;
            } else {
                return "";
            }
        }

        return null;
    }
}
