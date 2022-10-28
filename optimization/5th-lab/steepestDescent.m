function [x, y] = steepestDescent(startPoint, e1, e2, M)
    currentPoint = startPoint;
    gradient = [funcDx(currentPoint(1), currentPoint(2)), ...
        funcDy(currentPoint(1), currentPoint(2))];
    gradientLength = sqrt(gradient(1).^2 + gradient(2).^2);
    iterations = 0;
    while iterations < M || gradientLength < e1
        iterations = iterations + 1;
        funcOfA = @(a) currentPoint - a.*gradient;
        left = func(currentPoint(1), currentPoint(2)) - 1000;
        right = func(currentPoint(1), currentPoint(2)) + 1000;
        a = dichotomousSearch(funcOfA, left, right, 10.^-7);
        while 1
            nextPoint = funcOfA(a);
            if func(nextPoint(1), nextPoint(2)) ...
            	- func(currentPoint(1), currentPoint(2)) < 0
                break;
            end

            a = a./2;
        end
        
        diffPoint = nextPoint - currentPoint;
        diffLength = sqrt(diffPoint(1).^2 + diffPoint(2).^2);
        diffFunc = func(nextPoint(1), nextPoint(2)) ...
            - func(currentPoint(1), currentPoint(2));
        if diffLength < e1 && abs(diffFunc) < e2
            x = nextPoint(1);
            y = nextPoint(2);
            return
        end

        currentPoint = nextPoint;
        gradient = [funcDx(currentPoint(1), currentPoint(2)), ...
            funcDy(currentPoint(1), currentPoint(2))];
        gradientLength = sqrt(gradient(1).^2 + gradient(2).^2);
    end
    
    x = currentPoint(1);
    y = currentPoint(2);
end

