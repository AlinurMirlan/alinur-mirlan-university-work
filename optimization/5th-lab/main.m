xCoordinates = -25:.5:25;
yCoordinates = -25:.5:25;
[x, y] = meshgrid(xCoordinates, yCoordinates);
z = fun(x, y);
contour(x, y, z, 50);
hold on
[x, y, iterations] = conjugateGradient([20, 20], 10.^-3);
fprintf('Minimum found (%.3f, %.3f)\n', x, y);
fprintf('It took %d iterations given the initial coordinates (%.3f, %.3f)\n'...
            , iterationsToCoordinates(i, 1), iterationsToCoordinates(i, 2), ...
            iterationsToCoordinates(i, 3));
hold off
