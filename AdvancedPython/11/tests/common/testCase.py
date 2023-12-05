class TestCase:
    '''Structure for tests'''

    def __init__(self, test, expect):
        self.test = test
        self.expect = expect

    def __repr__(self):
        return f'TestCase(test={self.test}, expect={self.expect})'
