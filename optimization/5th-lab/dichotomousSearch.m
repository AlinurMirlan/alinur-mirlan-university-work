function [x, iterations] = dichotomousSearch(func, left, right, precision)
    halfOfLength = (right - left) ./ 2;
    iterations = 0;
    
    while halfOfLength >= precision
        iterations = iterations + 1;
        midPoint = (left + right) ./ 2;
        leftOffset = midPoint - (precision ./ 2);
        rightOffset = midPoint + (precision ./ 2);
        if func(leftOffset) < func(rightOffset)
            right = rightOffset;
        else
            left = leftOffset;
        end
        
        halfOfLength = (right - left) ./ 2;
    end
    
    x = midPoint;
end
        