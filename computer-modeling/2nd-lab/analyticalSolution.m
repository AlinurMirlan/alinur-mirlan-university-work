function [X, Y] = analyticalSolution(t0, h, func)
    X = [];
    Y = [];
    t = t0(1);
    tf = t0(2);
    i = 1;
    while (t < tf)
        X(i) = t;
        Y(i) = func(t);
        t = t + h;
        i = i + 1;
    end
end
