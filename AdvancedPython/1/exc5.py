'''
    Mateusz Zajac
    298654
    Advanced Python course
    Exercise 5
'''

def common_prefix(words):
    prefixes = {}
    best_prefix = ""
    for word in words:
        word = word.lower()
        for i in range(1, len(word)+1):
            if word[0:i] in prefixes:
                prefixes[word[0:i]] += 1
                if i > len(best_prefix) and prefixes[word[0:i]] >= 3:
                    best_prefix = word[0:i]
            else:
                prefixes[word[0:i]] = 1
    return(best_prefix)

print(common_prefix(["Cyprian", "cyberotoman", "cynik", "ceniąc", "czule"]))
print(common_prefix(["CaCaCaCa", "CaCa", "aaa", "aaab", "aaac"]))
print(common_prefix(["abc", "zabc", "zaabc"]))
print(common_prefix(["Cyprian", "cyberotoman", "cynik", "ceniąc", "czule", "abcdef", "abcdefg", "abcdefgh"]))

'''
    Function makes cache-like dictionary of all possible prefixes.
    For every possible prefix it checks if it ocurred previously
    and counts it. If length of currently checked prefix is bigger
    than previously saved and more than 2 words has it, it saves
    the new best result in best_prefix.
'''