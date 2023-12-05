'''
    Mateusz Zajac (298654)
    Advanced Python
    List 5
    Ex 2
'''

from abc import ABC, abstractmethod
from itertools import permutations

# main exception class for formulas
class FormulaException(Exception):
    pass

# Reject non-formulas stuff
class WrongFormulaException(FormulaException):
    def __init__(self, message = "This is not something of formula class..."):            
        super().__init__(message)

# Raise exception if this variable is not defined in environment provided
class MissingVariableValueException(FormulaException):
    def __init__(self, message = f"No value assigned to variable: ", var=""):            
        super().__init__(message, var)

class Formula(ABC):
    @abstractmethod
    def evaluate(self, _ = {}):
        raise NotImplementedError

    @abstractmethod
    def __str__(self):
        raise NotImplementedError

    def __add__(self, other):
        return Or(self, other)

    def __mul__(self, other):
        return And(self, other)

    @staticmethod
    def tautology(f):
        vars = set()
        def count_vars(f):
            if(isinstance(f, Variable)): 
                return vars.add(f.a)
            elif(isinstance(f, Constant)): 
                return
            elif(isinstance(f, SingleArg)):
                return count_vars(f.a)
            elif(isinstance(f, DoubleArg)):
                return count_vars(f.a), count_vars(f.b)
            else: 
                return
        count_vars(f)
        vars = list(vars)
        possibilities = [True, False] * len(vars)
        all_permutations = permutations(possibilities, len(vars))
        for permutation in all_permutations:
            values = {}
            for elem, boolVal in zip(vars, permutation):
                values[elem] = boolVal
            if(not f.evaluate(values)):
                # this print below may be commented-out
                # this is only for debugging purposes
                print(f"Not a tautology: {values}")
                return False
        return True

    # List of used tautologies:
    # I've used some of them (less complicated ones) to simplify 
    # http://www.math.ucsd.edu/~jeggers/math109/tautologies.pdf
    # Numbers in comments correspond to numbers on this list.
    @staticmethod
    def simplify(f):
        if(not isinstance(f, Formula)):
            raise WrongFormulaException
        
        if(isinstance(f, SingleArg)):
            if(isinstance(f, Not)):
                match f:
                    # 2
                    case Not(a=And(a=a, b=Not(a=b)) ) if a==b:
                        return Constant(True)
                    case Not(a=And(a=Not(a), b=b) ) if a==b:
                        return Constant(True)
                    # 5
                    case Not(a=Not(a=a)):
                        return Formula.simplify(a)
                    case _:
                        temp = Formula.simplify(f.a)
                        if temp != f.a:
                            return Formula.simplify(Not(temp))
                        return Not(temp)
            elif(isinstance(f, Variable)):
                return f
            elif(isinstance(f, Constant)):
                return f
            else:
                raise WrongFormulaException

        elif(isinstance(f, DoubleArg)):
            if(isinstance(f, Or)):
                match f:
                    # 1
                    case Or(a=a, b=Not(a=b)) if(a==b):
                        return Constant(True)
                    case Or(a=Not(a=a), b=b) if(a==b):
                        return Constant(True)
                    # 4a
                    case Or(a=a, b=b) if a==b:
                        return Formula.simplify(a)
                    # 9a
                    case Or(a=a, b=Constant(a=False)):
                        return Formula.simplify(a)
                    case Or(a=Constant(a=False), b=a):
                        return Formula.simplify(a)
                    # 9c
                    case Or(a=a, b=Constant(a=True)):
                        return Constant(True)
                    case Or(a=Constant(a=True), b=a):
                        return Constant(True)
                    # 10a
                    case Or(a=Not(a=a), b=Not(a=b)):
                        return Formula.simplify(Not(Formula.simplify(And(a,b))))
                    # 11b
                    case Or(a=And(a=a,b=b),b=And(a=Not(a=c),b=Not(a=d))) if a==c and b==d:
                        return Formula.simplify(Equal(a,b))
                    case Or(a=And(a=Not(a=c),b=Not(a=d)), b=And(a=a,b=b)) if a==c and b==d:
                        return Formula.simplify(Equal(a,b))
                    # 12a
                    case Or(a=Not(a=a), b=b):
                        return Formula.simplify(Impl(Formula.simplify(a), Formula.simplify(b)))
                    case Or(a=a, b=Not(a=b)):
                        return Formula.simplify(Impl(Formula.simplify(b), Formula.simplify(a)))
                    case _:
                        temp1 = Formula.simplify(f.a)
                        temp2 = Formula.simplify(f.b)
                        if temp1 != f.a or temp2 != f.b:
                            return Formula.simplify(Or(temp1, temp2))
                        return Or(temp1, temp2)
            elif(isinstance(f, And)):
                match f:
                    # 4b
                    case And(a=a, b=b) if a==b:
                        return Formula.simplify(a)
                    # 9b
                    case And(a=a, b=Constant(a=False)):
                        return Constant(False)
                    case And(a=Constant(a=False), b=a):
                        return Constant(False)
                    # 9d
                    case And(a=a, b=Constant(a=True)):
                        return Formula.simplify(a)
                    case And(a=Constant(a=True), b=a):
                        return Formula.simplify(a)
                    # 10b
                    case And(a=Not(a=a), b=Not(a=b)):
                        return Formula.simplify(Not(Formula.simplify(Or(a,b))))
                    # 11a
                    case And(a=Impl(a=a,b=b),b=Impl(a=d,b=c)) if a==c and b==d:
                        return Formula.simplify(Equal(a,b))
                    case And(a=Impl(a=d,b=c),b=Impl(a=a,b=b)) if a==c and b==d:
                        return Formula.simplify(Equal(a,b))
                    case _:
                        temp1 = Formula.simplify(f.a)
                        temp2 = Formula.simplify(f.b)
                        if temp1 != f.a or temp2 != f.b:
                            return Formula.simplify(And(temp1,temp2))
                        return And(temp1,temp2)

            elif(isinstance(f, Impl)):
                match f:
                    # 3
                    case Impl(a=a, b=b) if a==b:
                        return Constant(True)
                    # 13
                    case Impl(a=Not(a=a), b=Not(a=b)):
                        return Formula.simplify(Impl(Formula.simplify(b), Formula.simplify(a)))
                    # 17
                    case Impl(a=a,b=Or(a=b, b=c)) if a==b:
                        return Constant(True)
                    # 18
                    case Impl(a=And(a=a, b=b),b=c) if a==c:
                        return Constant(True)
                    # 23
                    case Impl(a=a,b=Constant(a=False)):
                        return Not(a)
                    case _:
                        temp1 = Formula.simplify(f.a)
                        temp2 = Formula.simplify(f.b)
                        if temp1 != f.a or temp2 != f.b:
                            return Formula.simplify(Impl(temp1, temp2))
                        return Impl(temp1, temp2)
            elif(isinstance(f, Equal)):
                match f:
                    # 4 (pvp and p^p are eliminated earlier by other cases)
                    case Equal(a=a, b=b) if a==b:
                        return Constant(True)
                    # 6a
                    case Equal(a=Or(a=a, b=b),b=Or(a=d, b=c)) if ((a==c) and (b==d)): 
                        return Constant(True)
                    # 6b
                    case Equal(a=And(a=a, b=b),b=And(a=d, b=c)) if ((a==c) and (b==d)): 
                        return Constant(True)
                    # 6c
                    case Equal(a=Equal(a=a, b=b),b=Equal(a=d, b=c)) if ((a==c) and (b==d)): 
                        return Constant(True)
                    case Equal(a=Not(a=a),b=Not(b=b)):
                        return Formula.simplify(Equal(Formula.simplify(a), Formula.simplify(b)))
                    case _:
                        temp1 = Formula.simplify(f.a)
                        temp2 = Formula.simplify(f.b)
                        if temp1 != f.a or temp2 != f.b:
                            return Formula.simplify(Equal(temp1, temp2))
                        return Equal(temp1, temp2)
            else:
                raise WrongFormulaException
        else:
            raise WrongFormulaException

    '''
        I've used just some of tautologies to simplify.
        It would be good to also implement just parts of some laws. 
        For example, left part of equality in fifteenth law 
        would do some magic in this function.
        But implementing it all is just too time-consuming task.
        I think the idea behind this exercise sheet is shown.
        It simplifies some silly stuff like:
        (p v true)   ---> true
        or
        (p ^ false) ---> false
    '''

class SingleArg(Formula):

    def __init__(self, a):
        self.a = a
        super().__init__()

    def __eq__(self, other):
        return type(self) == type(other) and self.a == other.a

class DoubleArg(Formula):
    def __init__(self, a, b):
        self.a = a
        self.b = b
        super().__init__()

    def __eq__(self, other):
        return type(self) == type(other) and self.a == other.a and self.b == other.b

class Or(DoubleArg):
    def __init__(self, a, b):
        super().__init__(a,b)

    def evaluate(self, variables = {}):
        return self.a.evaluate(variables) or self.b.evaluate(variables)

    def __str__(self):
        return f"({self.a} \u2228 {self.b})"

    def __eq__(self, other):
        return super().__eq__(other) or (type(self) == type(other) and self.a == other.b and self.b == other.a)

class And(DoubleArg):
    def __init__(self, a: Formula, b: Formula):
        super().__init__(a,b)

    def evaluate(self, variables = {}):
        return self.a.evaluate(variables) and self.b.evaluate(variables)

    def __str__(self):
        return f"({self.a} \u2227 {self.b})"

    def __eq__(self, other):
        return super().__eq__(other) or (type(self) == type(other) and self.a == other.b and self.b == other.a)

class Impl(DoubleArg):
    def __init__(self, a: Formula, b: Formula):
        super().__init__(a,b)

    # because a -> b is the same as ~a v b
    def evaluate(self, variables = {}):
        return Or(Not(self.a), self.b).evaluate(variables)

    def __str__(self):
        return f"({self.a} \u2192 {self.b})"

class Equal(DoubleArg):
    def __init__(self, a: Formula, b: Formula):
        super().__init__(a,b)

    # truth only if both arguments eval to the same
    def evaluate(self, variables = {}):
        return self.a.evaluate(variables) == self.b.evaluate(variables)

    def __str__(self):
        return f"({self.a} \u27F7  {self.b})"

    def __eq__(self, other):
        return super().__eq__(other) or (type(self) == type(other) and self.a == other.b and self.b == other.a)

class Not(SingleArg):
    def __init__(self, a):
        super().__init__(a)

    def evaluate(self, variables = {}):
        return not self.a.evaluate(variables)

    def __str__(self):
        return f"\u00AC{self.a}"

class Variable(SingleArg):
    def __init__(self, a):
        super().__init__(a)

    def evaluate(self, variables = {}):
        if self.a in variables:
            return variables[self.a]
        raise MissingVariableValueException(var=self.a)

    def __str__(self):
        return f"{self.a}"

class Constant(SingleArg):
    def __init__(self, a):
        super().__init__(bool(a))

    def evaluate(self, _ = {}):
        return self.a

    def __str__(self):
        return f"{self.a}"

# TESTS:
# I can just test tautology function on known tautologies from:
# http://www.math.ucsd.edu/~jeggers/math109/tautologies.pdf
# Second assert is for simplify function checking.
# Both of this asserts should pass without an error.
# This is because both formulas are equal, but second one is slightly simplified.

# 1
f = Or(Variable('p'),Not(Variable('p')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 2
f = Not(And(Variable('p'), Not(Variable('p'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 3
f = Impl(Variable('p'),Variable('p'))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 4
f = Equal(Variable('p'), Or(Variable('p'), Variable('p')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(Variable('p'), And(Variable('p'), Variable('p')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 5
f = Equal(Not(Not(Variable('p'))), Variable('p'))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 6
f = Equal(Or(Variable('p'),Variable('q')),Or(Variable('q'),Variable('p')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(And(Variable('p'),Variable('q')),And(Variable('q'),Variable('p')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(Equal(Variable('p'),Variable('q')),Equal(Variable('q'),Variable('p')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 7
f = Equal(Or(Variable('p'),Or(Variable('q'),Variable('r'))),Or(Or(Variable('p'),Variable('q')),Variable('r')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(And(Variable('p'),And(Variable('q'),Variable('r'))),And(And(Variable('p'),Variable('q')),Variable('r')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 8
f = Equal(And(Variable('p'),Or(Variable('q'),Variable('r'))),Or(And(Variable('p'),Variable('q')),And(Variable('p'),Variable('r'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(Or(Variable('p'),And(Variable('q'),Variable('r'))),And(Or(Variable('p'),Variable('q')),Or(Variable('p'),Variable('r'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 9
f = Equal(Or(Variable('p'), Constant(False)), Variable('p'))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(And(Variable('p'), Constant(False)), Constant(False))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(Or(Variable('p'), Constant(True)), Constant(True))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(And(Variable('p'), Constant(True)), Variable('p'))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 10
f = Equal(Not(And(Variable('p'), Variable('q'))),Or(Not(Variable('p')),Not(Variable('q'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(Not(Or(Variable('p'), Variable('q'))),And(Not(Variable('p')),Not(Variable('q'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 11
f = Equal(Equal(Variable('p'), Variable('q')),And(Impl(Variable('p'),Variable('q')),Impl(Variable('q'),Variable('p'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(Equal(Variable('p'), Variable('q')),Or(And(Variable('p'),Variable('q')),And(Not(Variable('q')),Not(Variable('p')))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(Equal(Variable('p'), Variable('q')),Equal(Not(Variable('p')),Not(Variable('q'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 12
f = Equal(Impl(Variable('p'), Variable('q')),Or(Not(Variable('p')),Variable('q')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(Not(Impl(Variable('p'), Variable('q'))),And(Variable('p'),Not(Variable('q'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 13
f = Equal(Impl(Variable('p'), Variable('q')),Impl(Not(Variable('q')),Not(Variable('p'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 14
f = Equal(Impl(Variable('p'), Variable('q')),Impl(And(Variable('p'),Not(Variable('q'))),Constant(False)))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 15
f = Equal(And(Impl(Variable('p'),Variable('r')),Impl(Variable('q'),Variable('r'))),Impl(Or(Variable('p'),Variable('q')),Variable('r')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(And(Impl(Variable('p'),Variable('q')),Impl(Variable('p'),Variable('r'))),Impl(Variable('p'), And(Variable('q'),Variable('r'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

f = Equal(Or(Impl(Variable('p'),Variable('q')),Impl(Variable('p'),Variable('r'))),Impl(Variable('p'), Or(Variable('q'),Variable('r'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 16
f = Equal(Impl(And(Variable('p'),Variable('q')),Variable('r')),Impl(Variable('p'),Impl(Variable('q'),Variable('r'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 17
f = Impl(Variable('p'),Or(Variable('p'),Variable('q')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 18
f = Impl(Variable('p'),And(Variable('p'),Variable('p')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 19
f = Impl(And(Variable('p'),Impl(Variable('p'),Variable('q'))),Variable('q'))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 20
f = Impl(And(Impl(Variable('p'),Variable('q')),Not(Variable('q'))),Not(Variable('p')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 21
f = Impl(And(Impl(Variable('p'),Variable('q')),Impl(Variable('q'),Variable('r'))),Impl(Variable('p'),Variable('r')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 22
f = Impl(And(Or(Variable('p'),Variable('q')),Not(Variable('p'))),Variable('q'))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 23
f = Impl(Impl(Variable('p'),Constant(False)),Not(Variable('p')))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 24
f = Impl(And(Impl(Variable('p'),Variable('q')),Impl(Variable('r'),Variable('s'))),Impl(Or(Variable('p'),Variable('r')),Or(Variable('q'),Variable('s'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True
# 25
f = Impl(Impl(Variable('p'),Variable('q')),Impl(Or(Variable('p'),Variable('r')),Or(Variable('q'),Variable('r'))))
print(f)
print(Formula.simplify(f))
print("--------")
assert Formula.tautology(f) == True
assert Formula.tautology(Formula.simplify(f)) == True

# If all of this tests pass without assertion error, we're home. :)

# Tests for __add__ and __mul__:

assert Constant(True) + Constant(False) == Or(Constant(True), Constant(False))
assert Constant(True) * Constant(False) == And(Constant(True), Constant(False))

# example from sheet:
print(Formula.simplify(Or(Not(Variable("x")), And(Variable('y'),Constant(True)))))
print(Formula.tautology(Or(Not(Variable("x")), And(Variable('y'),Constant(True)))))