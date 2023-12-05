from common.testCase import TestCase
import exc2
import context
import unittest
import os
import sys
import random

sys.path.append(os.getcwd())


def capitalityChanger(word):
    if len(word) < 1:
        return word
    if word.isupper():
        return word.lower()
    elif word.islower():
        return word.upper()
    else:
        return word.upper()


def palindromeBreaker(word):
    randomStr = random.randint(100, 999)
    return str(randomStr) + word


class testExc2(unittest.TestCase):

    test_cases = []

    def setUp(self):
        '''Setting up test cases'''
        with open("exc2/testCases/cases.txt", mode="r", encoding="utf-8") as tests_file:
            for line in tests_file.readlines():
                self.test_cases.append(TestCase(line, True))

    def tearDown(self):
        '''Tearing down test cases'''
        self.test_cases = []

    def testCorrectPalindromes(self):
        '''Testing correct palindromes'''
        for test_case in self.test_cases:
            self.assertEqual(exc2.is_palindrom(test_case.test),
                             test_case.expect, test_case)

    def testDifferentCapitalityCorrectPalindromes(self):
        '''Testing different capitality palindromes'''
        for test_case in self.test_cases:
            test_case.test = capitalityChanger(test_case.test)
            self.assertEqual(exc2.is_palindrom(test_case.test),
                             test_case.expect, test_case)

    def testIncorrectPalindromes(self):
        '''Testing incorrect palindromes'''
        for test_case in self.test_cases:
            test_case.test = palindromeBreaker(test_case.test)
            self.assertEqual(exc2.is_palindrom(test_case.test),
                             not test_case.expect, test_case)


if __name__ == "__main__":
    unittest.main()
