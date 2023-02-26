function dydt = vdpTwo(t, y)
    dydt = zeros(3, 1);
    dydt(1) = y(2);
    dydt(2) = y(3);
    dydt(3) = exp(-5.*t) - 15.*y(3) - 75.*y(2) - 125.*y(1);
end

