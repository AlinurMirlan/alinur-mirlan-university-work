function y = funcThree(u1, u2)
    a1 = 7;
    a2 = -7;
    r1 = 3.9047;
    r2 = -1.9047;
    r3 = -0.75;
    y = r1.*(u1.^2) + 2.*((r1.*a1) + (r3.*a2)).*u1 + 2.*r3.*u1.*u2 ...
           + 2.*((r3.*a1) + (r2.*a2)).*u2 + r2.*(u2.^2) + r1.*(a1.^2) ...
           + 2.*r3.*a1.*a2 + r2.*(a2.^2);
end