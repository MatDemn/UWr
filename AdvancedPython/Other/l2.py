import itertools

class Wyrazenie:
    pass

class Stala(Wyrazenie):
    def __init__(self, x):
        self.x = x

    def oblicz(self, _ = None):
        return self.x

class Zmienna(Wyrazenie):
    def __init__(self, x):
        self.x = x

    def oblicz(self, zmienne):
        if self.x in zmienne:
            return zmienne[self.x]

class Dodaj(Wyrazenie):
    def __init__(self, x, y):
        self.x = x
        self.y = y
    
    def oblicz(self, zmienne):
        return self.x.oblicz(zmienne) + self.y.oblicz(zmienne)

class Odejmij(Wyrazenie):
    def __init__(self, x, y):
        self.x = x
        self.y = y

    def oblicz(self, zmienne):
        return self.x.oblicz(zmienne) - self.y.oblicz(zmienne)

class Razy(Wyrazenie):
    def __init__(self, x, y):
        self.x = x
        self.y = y

    def oblicz(self, zmienne):
        return self.x.oblicz(zmienne) * self.y.oblicz(zmienne)

class Dziel(Wyrazenie):
    def __init__(self, x, y):
        self.x = x
        self.y = y

    def oblicz(self, zmienne):
        return self.x.oblicz(zmienne) / self.y.oblicz(zmienne)


class Formula:
    pass

class Wartosc(Formula):
    def __init__(self, val):
        self.val = bool(val)

    def oblicz(self, _ = None):
        return self.val

    def __str__(self):
        return f"{self.val}"

class Zmienna(Formula):
    def __init__(self, val):
        self.val = val

    def oblicz(self, zmienne):
        if self.val in zmienne:
            return zmienne[self.val]
        else:
            raise Exception(f"There is no variable named {self.val} in this scope")

    def __str__(self):
        return f"{self.val}"

class Not(Formula):
    def __init__(self, val):
        self.val = val

    def oblicz(self, zmienne):
        return not(self.value.oblicz(zmienne))

    def __str__(self):
        return f"~{self.val}"

    def __str__(self):
        if not isinstance(self.left, Zmienna) and not isinstance(self.left, Wartosc) :
            return f"~({self.val})"
        else:
            return f"~{self.val}"

class And(Formula):
    def __init__(self, left, right):
        self.left = left
        self.right = right
    
    def oblicz(self, zmienne):
        return self.left.oblicz(zmienne) and self.right.oblicz(zmienne)

    def __str__(self):
        finalStr = ""
        if not isinstance(self.left, Zmienna) and not isinstance(self.left, Wartosc) :
            finalStr += f"({self.left}) ˄ "
        else:
            finalStr += f"{self.left} ˄ "
        
        if not isinstance(self.right, Zmienna) and not isinstance(self.right, Wartosc) :
            finalStr += f"({self.right})"
        else:
            finalStr += f"{self.right}"
        return finalStr

class Or(Formula):
    def __init__(self, left, right):
        self.left = left
        self.right = right

    def oblicz(self, zmienne):
        return self.left.oblicz(zmienne) or self.right.oblicz(zmienne)

    def __str__(self):
        finalStr = ""
        if not isinstance(self.left, Zmienna) and not isinstance(self.left, Wartosc) :
            finalStr += f"({self.left}) ˅ "
        else:
            finalStr += f"{self.left} ˅ "
        
        if not isinstance(self.right, Zmienna) and not isinstance(self.right, Wartosc) :
            finalStr += f"({self.right})"
        else:
            finalStr += f"{self.right}"
        return finalStr
        
class Impl(Formula):
    def __init__(self, left, right):
        self.left = left
        self.right = right

    def oblicz(self, zmienne):
        return Not(self.left).oblicz(zmienne) or self.right.oblicz(zmienne)

    def __str__(self):
        finalStr = ""
        if not isinstance(self.left, Zmienna) and not isinstance(self.left, Wartosc) :
            finalStr += f"({self.left}) => "
        else:
            finalStr += f"{self.left} => "
        
        if not isinstance(self.right, Zmienna) and not isinstance(self.right, Wartosc) :
            finalStr += f"({self.right})"
        else:
            finalStr += f"{self.right}"

        return finalStr

def isTautology(formula):
    variables = set()
    def findVars(formula):
        if isinstance(formula, Zmienna): 
            variables.add(formula.val)
        elif isinstance(formula, Wartosc): 
            pass
        else:
            if hasattr(formula, "val"):
                findVars(formula.val)
            if hasattr(formula, "left"):
                findVars(formula.left)
            if hasattr(formula, "right"):
                findVars(formula.right)
    findVars(formula)
    variables = list(variables)
    combinationsLen = 2^len(variables)
    possibleVals = [[val] ]


    variables = [(elem, val) for elem in variables for val in [True, False]]
    combinations = []
    print(variables)

known_tautologies = [
    Or(Or(And(Zmienna("A"), Zmienna("B")), Not(Zmienna("A"))), Not(Zmienna("B"))),
    Impl(Or(Zmienna("A"),Zmienna("B")),Or(Zmienna("A"),Zmienna("B"))),
    Impl(And(Impl(Zmienna("A"), Zmienna("B")),Impl(Zmienna("B"),Zmienna("C"))),Impl(Zmienna("A"),Zmienna("C")))
]

isTautology(Impl(Not(Zmienna("A")),Impl(Zmienna("B"),And(Zmienna("C"), Or(Zmienna("D"),Not(Zmienna("E")))))))



    
