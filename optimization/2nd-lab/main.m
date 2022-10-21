% TASK 1
% white-spaces separate row elements and semi-colons separate rows 
A = [1 2 3; 4 5 6; 7 8 9; 10 11 12]

% line breaks may also serve as row dividers
A = [1 2 3
    4 5 6
    7 8 9
    10 11 12]

% you may as well create a .mat file containing the matrix definition and
% load it. Notice though: by using load() we won't obtain our immediate
% value, but a wrapper around it called 'struct' which will hold anything
% defined within a .mat file that are accessed via a '.' operator
A = load('matrix4x3.mat', 'A').A
clear A

% Assigning a new row
A = [1 2 3; 4 5 6]
A(3,:) = [7 8 9]

A = [A; 10 11 12]
% ':' means every element. B reads column by column
B = A(:)
clear B

% Assignment this way works column by column
A(:) = 12:-1:1
clear A

% couple of ways of creating an empty matrix
A = []
A = 1:-1
clear A

% constructing complex matrices
A = [1 2 3; 4 5 6; 7 8 9; 10 11 12] + [1 2 3; 4 5 6; 7 8 9; 10 11 12] * i
A = [1+1*i 2+2*i 3+3*i;
     4+4*i 5+5*i 6+6*i; 
     7+7*i 8+8*i 9+9*i; 
     10+10*i 11+11*i 12+12*i]

% TASK 2
A = [1 2 3;
     4 5 6;
     7 8 9;
     10 1 12];
A(5,1) = exp(-1);
A(1,5) = sqrt(2)

% TASK 3
A(3,4)

% TASK 4
B = A(2:3,1:3)

% TASK 5
B1 = A(2,:)

% TASK 6
AI = eye(5)

% TASK 7
BT = B'

% TASK 8
A1 = B + B
A2 = B - A1
A3 = B * BT
% A1 / B is equivalent to A1 * inv(B)
A4 = A1 / B
% B / A1 is equivalent to inv(B) * A1
A5 = B \ A1
A6 = inv(A3)

% TASK 9 A B B1 AI BT
C1 = A + AI
C2 = AI - A
C3 = B .* [2 2 2; 2 2 2]
C4 = BT ./ 2
C5 = 4 .\ B1

% TASK 10
det(A5)
A4^2

% TASK 11
D = [1 2 3; 0 -3 -6; 0 2 4];
D^18

% TASK 12
A7 = load('A7').A

% TASK 13
polynomial = [1 -1 -16 16];
roots(polynomial)
polynomial = [5.6 90 1.07 8 -0.002 -18]
roots(polynomial)

% TASK 14
coefficients = [1 -3 0;
                1 -8 2;
                -1 -6 1];
unknowns = [5; 4; 3];
linsolve(coefficients, unknowns)

coefficients = [1 3 1 4;
                2 6 4 8;
                4 9 2 12;
                3 3 3 4];
unknowns = [0; -1; 1; 0];
linsolve(coefficients, unknowns)
