xCoordinates = -20:.2:20;
yCoordinates = -20:.2:20;
[x, y] = meshgrid(xCoordinates, yCoordinates);
z = funcOne(x, y);
contour(x, y, z, 12);
pause
contourLines = contour(x, y, z, 3);
clabel(contourLines);
pause
surfc(z);
anonymousFunction = @(u) funcOne(u(1), u(2));
[functionArguments, functionValue, flags, output1] = ...
    fminunc(anonymousFunction, [10, 10]);
[functionArguments, functionValue, flags, output2] = ...
    fminunc(anonymousFunction, [100, 100]);
[functionArguments, functionValue, flags, output3] = ...
    fminunc(anonymousFunction, [1000, 1000]);
xMinimum = functionArguments(1)
yMinimum = functionArguments(2)
iterations1 = output1.iterations
iterations2 = output2.iterations
iterations3 = output3.iterations