function [X1, Y1, X2, Y2] = eulersMethod2Order(T, y, h, difEq)
    t = T(1);
    tf = T(2);
    x2 = y(1);
    x1 = y(2);
    i = 2;
    X1 = [t];
    Y1 = [x2];
    X2 = [t];
    Y2 = [x2];
    while (t < tf)
        x1 = x1 + h * difEq(x2, x1, t);
        x2 = x2 + h * x1;
        t = t + h;
        X1(i) = t;
        Y1(i) = x2;
        X2(i) = t;
        Y2(i) = x1;
        i = i + 1;
    end
end
