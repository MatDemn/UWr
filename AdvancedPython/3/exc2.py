'''
    Mateusz Zajac (298654)
    Advanced Python
    List 3
    Exc 2
'''

import time

'''
    There is a problem with provided example.
    Function in example should be called for n=10.000, not 1000.
'''

def perfect_imperative(n):
    if n<1:
        return []

    result = []

    def find_divisors(x):
        res = []
        for i in range(1, int(x/2) + 1):
            if x%i == 0:
                res.append(i)
        return res

    for i in range(1, n+1):
        if sum(find_divisors(i)) == i:
            result.append(i)
    return result

'''
    Similar to the first exercise. I've made another function 
    in this function which finds divisors of particular argument.
    Then, the outer function checks if sum of this divisors equals
    number provided.
'''


def perfect_comprehension(n):
    return [elem for elem in range(1, n+1) if elem == sum([x for x in range(1, elem) if elem % x == 0])]

'''
    Function makes some sort of "temporary list" to sum its elements.
    If this sum equals number provided, then this number is good candidate
    for prefect number.
'''

def perfect_test(x):
    return x == sum(list(filter(lambda k: x%k == 0, range(1,x))))

def perfect_functional(n):
    return list(filter(perfect_test, range(1, n+1)))

'''
    Again, I've made helper function 'perfect_test', which is a sieve
    for filter in outer function.
'''

assert perfect_imperative(10000) == perfect_comprehension(10000) == perfect_functional(10000)

tests_range = int(1e4)

elapsed_time = time.time()
perfect_imperative(tests_range)
elapsed_time = time.time() - elapsed_time
print(f"Imperative version: {elapsed_time}")

elapsed_time = time.time()
perfect_comprehension(tests_range)
elapsed_time = time.time() - elapsed_time
print(f"List comprehension version: {elapsed_time}")

elapsed_time = time.time()
perfect_functional(tests_range)
elapsed_time = time.time() - elapsed_time
print(f"Functional version: {elapsed_time}")

'''
    As it turns out, now imperative version is
    the fastest of all. Suprisingly, list comprehension
    occupies the second place in this race and functional
    is the worst.
'''

