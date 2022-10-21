function y = func(t, a)
    y = (-1/(3*(a^6)*(t^3))) - 1/(3*(a^6)*(a^3 + t^3)) ...
        + (2/(3*a^9))*log(abs((a^3 + t^3)/t^3));
end