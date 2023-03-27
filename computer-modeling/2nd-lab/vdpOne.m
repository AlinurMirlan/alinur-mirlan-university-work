function dydt = vdpOne(t, y)
    dydt = zeros(1, 1);
    dydt(1) = cos(0.5.*t) - 4.*y(1);
end

