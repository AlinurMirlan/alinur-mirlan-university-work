function [x, y, iterations] = conjugateGradient(startPoint, e)
    func = @(u) fun(u(1), u(2));
    funcDx = @(u) funOfX(u(1), u(2));
    funcDy = @(u) funOfY(u(1), u(2));
    currentPoint = startPoint;
    gradient = [funcDx(currentPoint), funcDy(currentPoint)];
    gradientLength = sqrt(gradient(1).^2 + gradient(2).^2);
    iterations = 0;
    k = 0;
    while gradientLength > e
        iterations = iterations + 1;
        if k == 0
            d = -gradient;
        else 
            b = (gradientLength.^2)./(previousGradientLength.^2);
            d = -gradient + b.*d;
        end
        funcOfA = @(a) func(currentPoint + a.*d);
        a = dichotomousSearch(funcOfA, 0, 10, 10.^-7);
        nextPoint = currentPoint + a.*d;
        plot([currentPoint(1), nextPoint(1)], ...
            [currentPoint(2), nextPoint(2)]);
        currentPoint = nextPoint;
        previousGradientLength = gradientLength;
        gradient = [funcDx(currentPoint), funcDy(currentPoint)];
        gradientLength = sqrt(gradient(1).^2 + gradient(2).^2);
        k = k + 1;
    end
    
    x = currentPoint(1);
    y = currentPoint(2);
end

