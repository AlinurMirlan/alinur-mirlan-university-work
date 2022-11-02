function z = func(u1, u2)
    a1 = 9;
    a2 = -9;
    r1 = 5.9142;
    r2 = 3.0858;
    r3 = -0.5;
    z = r1.*(u1.^2) + 2.*((r1.*a1) + (r3.*a2)).*u1 + 2.*r3.*u1.*u2 ...
           + 2.*((r3.*a1) + (r2.*a2)).*u2 + r2.*(u2.^2) + r1.*(a1.^2) ...
           + 2.*r3.*a1.*a2 + r2.*(a2.^2); 
end