function [] = groundwork()
    xCoordinates = -25:.5:25;
    yCoordinates = -25:.5:25;
    [x, y] = meshgrid(xCoordinates, yCoordinates);
    z = fun(x, y);
    contour(x, y, z, 50);
    hold on
end