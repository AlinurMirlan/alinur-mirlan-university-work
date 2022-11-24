function [c, ceq] = constraintsTwo(u)
    c = [];
    ceq(1) = u(1) + u(2) - 21;
end