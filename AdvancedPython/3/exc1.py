'''
    Mateusz Zajac (298654)
    Advanced Python
    List 3
    Exc 1
'''

import math
import time

def prime_imperative(n):
    if n <= 1:
        return []
    result = []

    def check_primality(n):
        for elem in result:
            if n % elem == 0:
                return False
        return True

    for i in range(2, n+1):
        if check_primality(i):
            result.append(i)
    return result

'''
    For this problem I'm using some sort of
    Sieve of Eratosthenes method.
    Every cancidate for new prime in result list
    is checked against previously added.
    If any of previously added give modulo equals 0,
    then this cendidte is bad, because it's not
    prime according to this number. 
'''

def prime_comprehension(n):
    return [i for i in range(2, n+1) if all([i % x != 0 for x in range(2, int(math.sqrt(i)+1))])]

'''
    For every number from 2 to n I'm checking if this number has any
    divisors (all function). If not, then this number is good candidate for
    another prime number on the list.
'''

def list_of_divisors(n):
    return len(list(filter(lambda x: n%x == 0, range(2, int(math.sqrt(n)+1))))) == 0

def prime_functional(n):
    return list(filter(list_of_divisors, range(2, n+1)))
'''
    First, I make some sort of 'sieve' to filter out wrong numbers (list_of_divisors).
    If this list of divisord is 0 in length, then this number is a great candidate.
    Prime_functional is just using this sieve to make a proper list.
'''

assert prime_imperative(20) == prime_comprehension(20) == prime_functional(20) == [2,3,5,7,11,13,17,19]

'''
    Testing if all three functions give correct answer.
'''

tests_range = int(1e5)

elapsed_time = time.time()
prime_imperative(tests_range)
elapsed_time = time.time() - elapsed_time
print(f"Imperative version: {elapsed_time}")

elapsed_time = time.time()
prime_comprehension(tests_range)
elapsed_time = time.time() - elapsed_time
print(f"List comprehension version: {elapsed_time}")

elapsed_time = time.time()
prime_functional(tests_range)
elapsed_time = time.time() - elapsed_time
print(f"Functional version: {elapsed_time}")

'''
    Time comparison
'''

assert prime_imperative(tests_range) == prime_comprehension(tests_range) == prime_functional(tests_range)

'''
    Another check, on bigger example.
'''

'''
    List comprehension is the fastest of all.
    Second place belongs to imperative method
    and the last one is functional.

    Sometimes functional was slightly better than imperative for some reason.
'''
