function [X, Y] = eulersMethod(t, y, step, difEq)
    absc = t(1);
    border = t(2);
    dif = y;
    i = 2;
    X = [absc];
    Y = [dif];
    while (absc < border)
        dif = dif + step * difEq(dif, absc);
        absc = absc + step;
        X(i) = absc;
        Y(i) = dif;
        i = i + 1;
    end
end
