f = [1 0];
fun = @(u1, u2) u1;
u1ord = 0: 1: 10;
u2ord = 0: 1: 10;
drawSolution(f, fun, xord, uord);

f = [-1 0];
fun = @(u1, u2) -u1;
drawSolution(f, fun, xord, uord);

f = [0 1];
fun = @(u1, u2) u1;
drawSolution(f, fun, xord, uord);

f = [0 -1];
fun = @(u1, u2) -u1;
drawSolution(f, fun, xord, uord);