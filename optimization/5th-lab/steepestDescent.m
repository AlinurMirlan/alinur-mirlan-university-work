function [x, y, iterations] = steepestDescent(startPoint, e1, e2, M)
    func = @(u) fun(u(1), u(2));
    funcDx = @(u) funOfX(u(1), u(2));
    funcDy = @(u) funOfY(u(1), u(2));
    currentPoint = startPoint;
    gradient = [funcDx(currentPoint), funcDy(currentPoint)];
    gradientLength = sqrt(gradient(1).^2 + gradient(2).^2);
    iterations = 0;
    while iterations < M && gradientLength > e1
        iterations = iterations + 1;
        funcOfA = @(a) func(currentPoint - a.*gradient);
        left = 0;
        right = 10;
        a = dichotomousSearch(funcOfA, left, right, 10.^-7);
        while 1
            nextPoint = currentPoint - a.*gradient;
            if func(nextPoint) ...
            	- func(currentPoint) < 0
                break;
            end

            a = a./2;
        end
        
        diffPoint = nextPoint - currentPoint;
        diffLength = sqrt(diffPoint(1).^2 + diffPoint(2).^2);
        diffFunc = func(nextPoint) - func(currentPoint);
        if diffLength < e1 && abs(diffFunc) < e2
            x = nextPoint(1);
            y = nextPoint(2);
            return
        end
        
        plot([currentPoint(1), nextPoint(1)], ...
            [currentPoint(2), nextPoint(2)]);
        currentPoint = nextPoint;
        gradient = [funcDx(currentPoint), funcDy(currentPoint)];
        gradientLength = sqrt(gradient(1).^2 + gradient(2).^2);
    end
    
    x = currentPoint(1);
    y = currentPoint(2);
end

