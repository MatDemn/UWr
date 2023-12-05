'''
    Mateusz Zajac
    298654
    Kurs języka Python
'''

class Z1:
    defList = [0.2, 0.5, 4.59, 6]

    @staticmethod
    def vat_faktura(lista : list = defList):
        return 0.23*sum(lista)

    @staticmethod
    def vat_paragon(lista : list = defList):
        return sum([0.23*x for x in lista])

print(Z1.vat_faktura())
print(Z1.vat_paragon())
print(Z1.vat_faktura() == Z1.vat_paragon())

'''
    The solution should be the same, methematically speaking.
    But because computers use floating-point arithmetic,
    there may be rounding error during calculations.
    This is the reason why results differ from each other. 
'''

class Z2:
    coins = [1,2,5, 10, 20]
    coins = sorted(coins, key=None, reverse=True)

    @staticmethod
    def make_change_greedy(amount):
        result = {1 : 0, 2 : 0, 5 : 0, 10: 0, 20: 0}
        for coin in Z2.coins:
            while amount-coin > -1:
                amount -= coin
                result[coin] += 1
            if amount == 0:
                return result
        return result

print(Z2.make_change_greedy(123))

'''
    Simple greedy algorithm works here perfectly.
    We use only "canonical" coins, so greedy
    approach is optimal (we know this from
    Algorithms and Data Structures).
'''

class Z3:
    @staticmethod
    def romb(n):
        for i in range(n):
            print((1+(n-i))*" ", (2*i + 1)*"#")
        print(" ", (2*n+1)*"#")
        for i in range(n-1, -1, -1):
            print((1+(n-i))*" ", (2*i + 1)*"#")

Z3.romb(4)
'''
    For some reason, the additional space is needed
    even for a row full of hashes. That's why
    additional space is added
    in order to acquire the right formatting.
'''

class Z4:
    @staticmethod
    def zaszyfruj(tekst, klucz):
        if klucz < 0 or klucz > 255:
            raise ValueError("Wrong key passed")
        return "".join([chr(ord(x)^klucz) for x in tekst])

    @staticmethod
    def odszyfruj(szyfr, klucz):
        return Z4.zaszyfruj(szyfr, klucz)

print(Z4.zaszyfruj("Python", 7))
print(Z4.odszyfruj(Z4.zaszyfruj("Python", 7),7))
print("Python" == Z4.odszyfruj(Z4.zaszyfruj("Python", 7),7))

class Z5:
    def eratosthenesSievePrime():
        num = 2
        D = {}
        while True:
            if num not in D:
                yield num
                D[num*num] = [num]
            else:
                for p in D[num]:
                    D.setdefault(p+num, []).append(p)
                del D[num]
            num += 1

    @staticmethod
    def find_factors(number):
        result = {}
        for divisor in Z5.eratosthenesSievePrime():
            while(number % divisor == 0):
                if divisor in result:
                    result[divisor] += 1
                else:
                    result[divisor] = 1
                number /= divisor
            if number == 1:
                return [tuple(elem) for elem in result.items()] 

print(Z5.find_factors(756))

'''
    This code uses Eratosthenes sieve method
    for generating new primes every iteration of loop.
'''