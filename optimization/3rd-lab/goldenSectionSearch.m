function [x, iterations] = goldenSectionSearch(func, left, right, precision)
    goldenRatio = 1.1618;
    length = right - left;
    iterations = 0;
    
    while length >= precision
        iterations = iterations + 1;
        ratio = (right - left) ./ goldenRatio;
        leftOffset = right - ratio;
        rightOffset = left + ratio;
        if func(leftOffset) >= func(rightOffset)
            left = leftOffset;
        else
            right = rightOffset;
        end
        
        length = abs(right - left);
    end
    
    x = (right + left) ./ 2;
end
        