function f = fOfTwo(u1, u2)
    a = 2;
    b = 1;
    f = .5.*((a.^(-2)).*(u1.^2) + (b.^(-2)).*(u2.^2));
end

