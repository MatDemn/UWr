'''
    Mateusz Zajac
    298654
    Advanced Python course
    Exercise 1
'''

zakupy = [0.2, 0.5, 4.59, 6]

def vat_faktura(lista : list):
    return 0.23*sum(lista)


def vat_paragon(lista : list):
    return sum([0.23*x for x in lista])

print(vat_faktura(zakupy))
print(vat_paragon(zakupy))
print(vat_faktura(zakupy) == vat_paragon(zakupy))

'''
    The solution should be the same, methematically speaking.
    But because computers use floating-point arithmetic,
    there may be rounding error during calculations.
    This is the reason why results differ from each other. 
'''

from decimal import *

zakupyDecimal = [Decimal('.2'), Decimal('.5'), Decimal('4.59'), Decimal('6')]

def vat_faktura2(lista : list):
    return Decimal('.23')*sum(lista)


def vat_paragon2(lista : list):
    return sum([Decimal('.23')*x for x in lista])

print(vat_faktura2(zakupyDecimal))
print(vat_paragon2(zakupyDecimal))
print(vat_faktura2(zakupyDecimal) == vat_paragon2(zakupyDecimal))

'''
    This method is slower, but helps with more precise calculations.
    Numbers have to be string in order to work properly.
    For example Decimal(".23") =/= Decimal(.23)

    But this method doesn't always work better.
    It has its own rounding that can ruin calculations.

    Example:

    (Decimal('1') / Decimal('3')) * Decimal('3') =/= Decimal('1')

    (1.0 / 3.0) * 3.0 == 1.0
'''
    
