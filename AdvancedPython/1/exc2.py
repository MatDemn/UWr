'''
    Mateusz Zajac
    298654
    Advanced Python course
    Exercise 2
'''

import re

def is_palindrom(word):
    word = re.sub(r'\W*', '', word)
    word = word.lower()
    word_length = len(word)-1
    for i in range(0, word_length):
        if i >= word_length:
            return True
        if word[i] != word[word_length-i]:
            return False
    return True

with open("exc2Tests.txt", "r") as file:
    word = file.readline()
    assert is_palindrom(word) == True, f"Error in test: {word}"

'''
    Package "re" is used to filter out non-letter characters.
    Tests provided in exc2Tests.txt.
    App reads file line by line and checks if word is palindrome.
    In case one of the tests fails, whole app fails with AssertionError.
    All tests should end with True statement from is_palindrom.
'''



