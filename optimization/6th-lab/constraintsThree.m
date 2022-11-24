function [c, ceq] = constraintsThree(u)
    c = [];
    ceq(1) = u(1) + u(2) - 14.5;
end