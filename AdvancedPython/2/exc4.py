'''
    Mateusz Zajac (298654)
    Advanced Python
    List 2
    Exc 4
'''

'''
    Simplest solution might be to first delete every "too long" word
    and then delete words randomly until word_number is exceeded.
'''

import random
import requests


def simplify_sentence(text, word_length, word_number):
    # simple checks to see if function have to be calculated
    if word_number <= 0 or word_length <= 0:
        return ""
    words = text.split()

    # cutting out long words
    words = [words[i] for i in range(0, len(words)) 
            if sum(l.isalnum() for l in words[i]) <= word_length]
    # calculate how much you have to delete
    count_delete= len(words) - word_number
    # normalize result
    count_delete = 0 if count_delete < 0 else count_delete
    # delete until there are words left to delete
    while count_delete > 0:
        # random index
        choice = random.randint(0, len(words)-1)
        # if didn't delete this word previously
        if words[choice]:
            words[choice] = ''
            count_delete -= 1
    # construct list from "not null words"
    words = [elem for elem in words if elem != '']
    # join them together
    result_text = " ".join(filter(None, words))
    # first letter is capitalized and period on the end if last char is not letter or number
    if len(result_text):
        if not result_text[-1].isalnum():
            return result_text.capitalize()
        return (result_text+".").capitalize()
    return ""

tekst = "Podział peryklinalny inicjałów wrzecionowatych \
kambium charakteryzuje się ścianą podziałową inicjowaną \
w płaszczyźnie maksymalnej."
print(simplify_sentence(tekst, 10, 5))

print(simplify_sentence("Hello, world!", 5, 5))

exit()
url = "https://wolnelektury.pl/media/book/txt/pan-tadeusz.txt"
r = requests.get(url, allow_redirects=True)

with open("download.txt", "wb") as f:
    f.write(r.content)

with open("download.txt", "r", encoding='utf-8') as f:
    text = f.read()
    with open("result.txt", "w") as res:
        res.write(simplify_sentence(text, 10, 100))

print("done!")


