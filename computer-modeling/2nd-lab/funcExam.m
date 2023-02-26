function [] = funcExam(func, manualFunc, t0, tf, y0)
    [t, y] = ode45(func, [t0 tf], y0);
    plot(t, y)
    hold on
    plot(t, manualFunc(t), '--o');
    xlabel("Time t")
    ylabel("Solution y")
    hold off
    pause
end

