function [] = exam(fOf, u, fval, u1ord, u2ord, cfunc)
    [u1, u2] = meshgrid(u1ord, u2ord); 
    f = fOf(u1, u2);    
    surfc(u1, u2, f)
    pause
    cont = contour(u1ord, u2ord, f, [fval fval]);
    clabel(cont);
    xlabel('u1');
    ylabel('u2');
    hold on
    plot(u(1), u(2), '*')
    text(u(1), u(2),'Solution')
    plot(u1ord, cfunc)
    hold off
    pause
end

