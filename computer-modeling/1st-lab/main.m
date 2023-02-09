clc;
t0 = 1;
tf = 2;
y0 = [8 10];
[t, y] = ode45('vdpOne', [t0 tf], y0);

plot(t, y)
xlabel("Time t")
ylabel("Solution y")
A = [1 log(5./4); 0 -9./40];
B = [8; 10];
T = linsolve(A, B);
pause

t0 = 0;
y0 = [2 -4 5];
[t, y] = ode45('vdpTwo', [t0 tf], y0);
plot(t, y)