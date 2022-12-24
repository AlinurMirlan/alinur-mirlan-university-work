u1ord = -2.2: .1: 2.2;
u2ord = -2.2: .1: 2.2;
u0 = [0, 1];

% Second task
func = @(u) fOfTwo(u(1), u(2));
[u, fval] = fmincon(func, u0, [], [], [], [], [], [], @constraintsOne);
cfunc = .56 ./ u1ord;
exam(@fOfTwo, u, fval, u1ord, u2ord, cfunc);

% First task
func = @(u) fOfOne(u(1), u(2));
[u, fval] = fmincon(func, u0, [], [], [1 1], [-2]);
cfunc = -u1ord - 2;
exam(@fOfOne, u, fval, u1ord, u2ord, cfunc);

% Third task
u1ord = -30: .5: 30;
u2ord = -30: .5: 30;
u0 = [4, 4];
func = @(u) fOfThree(u(1), u(2));
[u, fval] = fmincon(func, u0, [], [], [], [], [], [], @constraintsTwo);
cfunc = 21 - u1ord;
exam(@fOfThree, u, fval, u1ord, u2ord, cfunc);

% Fourth task
func = @(u) fOfThree(u(1), u(2));
[u, fval] = fmincon(func, u0, [], [], [], [], [], [], @constraintsThree);
cfunc = 14.5 - u1ord;
exam(@fOfThree, u, fval, u1ord, u2ord, cfunc);