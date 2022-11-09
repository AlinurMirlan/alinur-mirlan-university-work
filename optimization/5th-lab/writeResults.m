function [] = writeResults(x, y, iterations, initialCoordinates)
    fprintf('Minimum found (%.3f, %.3f)\n', x, y);
    message = strcat('It took %d iterations given the initial' ...
        , ' coordinates (%.3f, %.3f)\n');
    fprintf(message, iterations, initialCoordinates(1), ...
        initialCoordinates(2));
    hold off
end