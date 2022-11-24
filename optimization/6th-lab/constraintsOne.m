function [c, ceq] = constraintsOne(u)
    c = [];
    ceq(1) = u(1).*u(2) - .56;
end