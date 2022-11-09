groundwork();
initialCoordinates = [20, 20];
[x, y, iterations] = conjugateGradient(initialCoordinates, 10.^-3);
fprintf('Conjugate gradient method\n');
writeResults(x, y, iterations, initialCoordinates);
pause
groundwork();
[x, y, iterations] = steepestDescent(initialCoordinates, 10.^-3, 10.^-3 ...
    , 1000);
fprintf('Steepest descent method\n');
writeResults(x, y, iterations, initialCoordinates);
 
