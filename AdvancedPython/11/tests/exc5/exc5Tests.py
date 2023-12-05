from common.testCase import TestCase
import exc5
import context
import unittest
import os
import sys
import random

sys.path.append(os.getcwd())


class testExc5(unittest.TestCase):

    test_cases_whole_words_prefixes = []
    test_cases_empty_prefixes = []
    test_cases_some_prefixes = []

    def setUp(self):
        '''Setting up testing environment...'''
        with open("exc5/testCases/wholeWordPrefixes.txt", mode="r", encoding="utf-8") as tests_file:
            for line in tests_file.readlines():
                raw_list, result = line.split(";")
                result = result.rstrip('\n')
                words_list = raw_list.split(",")
                self.test_cases_whole_words_prefixes.append(
                    TestCase(words_list, result))

        with open("exc5/testCases/noPrefixes.txt", mode="r", encoding="utf-8") as tests_file:
            for line in tests_file.readlines():
                raw_list, result = line.split(";")
                result = result.rstrip('\n')
                words_list = raw_list.split(",")
                self.test_cases_empty_prefixes.append(
                    TestCase(words_list, result))

        with open("exc5/testCases/somePrefixes.txt", mode="r", encoding="utf-8") as tests_file:
            for line in tests_file.readlines():
                raw_list, result = line.split(";")
                result = result.rstrip('\n')
                words_list = raw_list.split(",")
                self.test_cases_some_prefixes.append(
                    TestCase(words_list, result))

    def tearDown(self):
        '''Tearing down test cases'''
        self.test_cases_whole_words_prefixes = []
        self.test_cases_empty_prefixes = []
        self.test_cases_some_prefixes = []

    def testWholeWordsPrefixes(self):
        '''Testing cases where one of the words is result prefix'''
        for test in self.test_cases_whole_words_prefixes:
            self.assertEqual(exc5.common_prefix(test.test), test.expect, test)

    def testEmptyPrefixes(self):
        '''Testing cases where result prefix is empty'''
        for test in self.test_cases_empty_prefixes:
            self.assertEqual(exc5.common_prefix(test.test), test.expect, test)

    def testSomePrefixes(self):
        '''Testing cases where result is some prefix of provided words'''
        for test in self.test_cases_some_prefixes:
            self.assertEqual(exc5.common_prefix(test.test), test.expect, test)


if __name__ == "__main__":
    unittest.main()
