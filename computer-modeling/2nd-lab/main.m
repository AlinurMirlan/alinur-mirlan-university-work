clc;
t0 = 1;
tf = 2;
y0 = [8 10];
A = [1 log(5./4); 0 -9./40];
B = [8; 10];
T = linsolve(A, B);
func = @(t) T(1) + T(2).*log(abs((9.*t + 1)./(9.*t - 1)));
funcExam('vdpOne', func, t0, tf, y0);

t0 = 0;
y0 = [2 -4 5];
A = [1 0 0; -5 1 0; 75 -30 6];
B = [2; -4; 5];
T = linsolve(A, B);
func = @(t) (T(1) + T(2).*t + T(3).*(t.^2) + (t.^2)./6).*exp(-5.*t);
funcExam('vdpTwo', func, t0, tf, y0);


