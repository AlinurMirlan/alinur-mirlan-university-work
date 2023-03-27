% First model
clc;
t0 = 0;
tf = 10;
y0 = [1];
h = 0.001;
[t, y] = ode45('vdpOne', [t0 tf], y0);
plot(t, y);
hold on
difEq = @(x, t) cos(0.5.*t) - 4.*x;
[X, Y] = eulersMethod([t0, tf], y0, h, difEq);
plot(X, Y, 'o');
func = @(t) analyticalFunc(t);
[X_, Y_] = analyticalSolution([t0, tf], h, func);
plot(X_, Y_, '*');

t = t0;
i = 1;
error = 0;
while (t < tf)
    error = error + abs(Y(i) - Y_(i));
    t = t + h;
    i = i + 1;
end
fprintf("error: %f", error);

pause
hold off

% Second model
% [x, *x]
y0 = [1, 1];
t0 = 0;
tf = 10;
h = 0.1;
[t, y] = ode45('vdpTwo', [t0 tf], y0);
plot(t, y);
hold on
difEq = @(x, xn, t) cos(0.5.*t) - xn - 4.*x;
[X1, Y1, X2, Y2] = eulersMethod2Order([t0, tf], y0, h, difEq);   
plot(X1, Y1, '.');
hold on
plot(X2, Y2, '.');
hold off
