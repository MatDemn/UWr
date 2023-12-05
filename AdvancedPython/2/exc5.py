'''
    Mateusz Zajac (298654)
    Advanced Python
    List 2
    Exc 5
'''

'''
    To compress text, we have to look at every single letter and check
    if it's the same as previous one. If it is, then we +1 the counter.
    If not, we add counted result to list, change current letter to
    this one just tested and clear the counter.
'''

def compress(text: str):
    if not text:
        return []
    result = []
    count = 0
    current_letter = ''
    for letter in text:
        if letter != current_letter:
            if current_letter != '':
                result.append((current_letter, count))
            count = 1
            current_letter = letter
        else:
            count += 1
    if result[-1][0] != current_letter: # there is a problem, should be: "if len(result)>0 and result[-1][0] != current_letter:"
        result.append((current_letter, count))
    return result

compress("aaa")
exit()

'''
    The last if is for last letter in word.
    Without it, function would count all letters but last one.
    It's sort of "clear your buffer" before the end of function.
'''

def decompress(letters):
    result = ""
    for (letter, count) in letters:
        result += count * letter
    return result

'''
    Decompress is much much simpler. We just read every single pair
    and add to result correct number of letters.
'''

with open('download.txt', 'r') as file:
    line = file.readline()
    assert line == decompress(compress(line)), line

'''
    I used text from exc 4 to test everything. 
'''











