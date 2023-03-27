function dydt = vdpTwo(t, y)
    dydt = zeros(2, 1);
    dydt(1) = y(2);
    dydt(2) = cos(0.5.*t) - y(2) - 4.*y(1);
end

