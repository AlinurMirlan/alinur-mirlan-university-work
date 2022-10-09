function [] = funcExam(func)
    xCoordinates = -20:.2:20;
    yCoordinates = -20:.2:20;
    [x, y] = meshgrid(xCoordinates, yCoordinates);
    z = func(x, y);
    contour(x, y, z, 12);
    pause
    contourLines = contour(x, y, z, 3);
    clabel(contourLines);
    pause
    surfc(z);
    pause
    % Making that our function accepts one parameter instead of two. This
    % is due to the fminunc's incapability of taking functions with
    % multiple parameters
    anonymousFunction = @(u) func(u(1), u(2));
    initCoordinates1 = [10, 10];
    initCoordinates2 = [100, 100];
    initCoordinates3 = [1000, 1000];
    [~, ~, ~, output1] = ...
        fminunc(anonymousFunction, ...
        [initCoordinates1(1), initCoordinates1(2)]);
    [~, ~, ~, output2] = ...
        fminunc(anonymousFunction, ...
        [initCoordinates2(1), initCoordinates2(2)]);
    [functionArguments, ~, ~, output3] = ...
        fminunc(anonymousFunction, ...
        [initCoordinates3(1), initCoordinates3(2)]);
    xMinimum = functionArguments(1);
    yMinimum = functionArguments(2);
    iterations1 = output1.iterations;
    iterations2 = output2.iterations;
    iterations3 = output3.iterations;
    iterationsToCoordinates = ...
        [iterations1, initCoordinates1(1), initCoordinates1(2); ...
        iterations2, initCoordinates2(1), initCoordinates2(2); ...
        iterations3, initCoordinates3(1), initCoordinates3(2)];

    fprintf('Extremum found (%.3f, %.3f)\n', xMinimum, yMinimum);
    for i = 1:3
        fprintf('It took %d iterations given the initial coordinates (%.3f, %.3f)\n'...
            , iterationsToCoordinates(i, 1), iterationsToCoordinates(i, 2), ...
            iterationsToCoordinates(i, 3));
    end
end

