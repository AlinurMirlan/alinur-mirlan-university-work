f = [-1 0]';
% maximize u1
fun = @(u1, u2) u1;
u1ord = 0: 1: 10;
u2ord = 0: 1: 10;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% minimize u1
f = [1 0]';
fun = @(u1, u2) u1;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% maximize u2
f = [0 -1]';
fun = @(u1, u2) u1;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% minimize u2
f = [0 1]';
fun = @(u1, u2) u1;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% maximize u1 + u2
f = [-1 -1]';
fun = @(u1, u2) u1 + u2;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% minimize u1 + u2
f = [1 1]';
fun = @(u1, u2) u1 + u2;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% maximize -3*u1 + 5*u2
f = [3 -5]';
fun = @(u1, u2) -3.*u1 + 5.*u2;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% minimize 3*u1 - 5*u2
f = [3 -5]';
fun = @(u1, u2) 3.*u1 - 5.*u2;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% maximize -2*u1 - u2
f = [2 1]';
fun = @(u1, u2) -2.*u1 - u2;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% maximize -u1 + 2*u2
f = [1 -2]';
fun = @(u1, u2) -u1 + 2.*u2;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% minimize u1 - 2*u2
f = [1 -2]';
fun = @(u1, u2) u1 - 2.*u2;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);

% minimize 2*u1 + u2
f = [2 1]';
fun = @(u1, u2) 2.*u1 + u2;
drawSolutionOne(f, fun, u1ord, u2ord);
drawSolutionTwo(f, fun, u1ord, u2ord);
drawSolutionThree(f, fun, u1ord, u2ord);
drawSolutionFour(f, fun, u1ord, u2ord);