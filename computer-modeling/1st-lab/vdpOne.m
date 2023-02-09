function dydt = vdpOne(t, y)
    dydt = zeros(2, 1);
    dydt(1) = y(2);
    dydt(2) = -((162.*t)/(81.*(t^2) - 1)).*(y(2));
end

