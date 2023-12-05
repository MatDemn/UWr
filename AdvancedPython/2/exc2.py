'''
    Mateusz Zajac (298654)
    Advanced Python
    List 2
    Exc 2
'''

import random
import math

'''
    The task is simple. We have to calculate
    int(sqrt(n)). Algorithm iterate from 1 to k
    until the sum is greater than n. Then our
    previous k (k-1) is the anwser.
'''

def sqrtSeries(n):
    if(n<0):
        raise ValueError("Argument can't be negative!")
    k = 0
    sum = 0
    while(sum <= n):
        k += 1
        sum += (2*k-1)
    return k-1

'''
    I'm testing 1000 random numbers and comparing to
    standard library sqrt function.
    Of course this library sqrt returns floating point number,
    so i convert it to int.
'''

for _ in range(1000):
    test = random.randint(0, 1e6)
    assert int(math.sqrt(test)) == sqrtSeries(test), test




    