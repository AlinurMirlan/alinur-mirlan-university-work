function [] = drawSolution(f, fun, u1ord, u2ord)
    ca = [-1 -1; -7 8; 3 2];
    cb = [-5; 0; 18];
    clb = [0; 0];
    cub = [4; 6];
    disp('Решение проблемы будет иметь вид:')
    u = linprog(f, ca, cb, [], [], clb, cub)
    disp('Оптимальное значение критерия:')
    j0 = f*u
    u01 = [1 0] * u;
    u02 = [0 1] * u;
    [u1, u2] = meshgrid(u1ord, u2ord);
    fOf = fun(u1, u2);
    cm = contour(u1ord, u2ord, fOf, [.0000 j0], 'w:'); 
    clabel(cm);
    xlabel('u1');
    ylabel('u2');
    title('Solution of problem');
    hold on;
    
    % 1st constraint
    u2ord = 5 - u1ord;
    plot(u1ord, u2ord, 'y');
    % 2nd constraint
    u2ord = (7 ./ 8) * u1ord;
    plot(u1ord, u2ord, 'b');
    % 3rd constraint 
    u2ord = 9 - ((3 ./ 2) * u1ord);
    plot(u1ord, u2ord, 'g'); 
    % 4th constraint
    plot([0 4], [0 0], 'r');
    % 5th constraint
    plot([0 0], [0 6], 'r');
    
    plot(u01, u02, '*r');
    hold off
    pause
end

