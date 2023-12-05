import sys
import os
import unittest
import context

import exc2
import exc5
import exc2Tests
import exc5Tests

m1 = unittest.defaultTestLoader.loadTestsFromTestCase(exc2Tests.testExc2)
m2 = unittest.defaultTestLoader.loadTestsFromTestCase(exc5Tests.testExc5)
alltests = unittest.TestSuite([m1, m2])
unittest.TextTestRunner(verbosity=3).run(alltests)
