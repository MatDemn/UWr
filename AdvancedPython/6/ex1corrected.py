'''
    Mateusz Zajac (298654)
    Advanced Python
    List 6
    Ex 1
'''

'''
    I have to read the page from start_page address, 
    call the function 'action' on this webpage
    and call the action for another pages in this website.

    Subpages calls are limited by distance parameter. Also,
    I can't process the same page twice.

    I have to implement it as iterator, which returns tuples:
    (url,result_of_the_function_action)

    Most basic function as action is searching for sentences with "Python" in it.
'''

# Fetching html websites
import requests as req
# Processing html text
from bs4 import BeautifulSoup
# Cutting out links
from bs4.element import SoupStrainer
# Split text into sentences
from sentence_splitter import split_text_into_sentences
# Get absolute links from relative
from urllib.parse import urljoin
# Some regex utilities
import re

# Depth more than 2 may dig up too deep. Assuming start_page has N unique links
# and those links also has N links etc, etc we have aprox. O(N^(distance)) time complexity.
# Quadratic search is enough I think.
DEPTH_MAX = 2

# Function for extracting "python" and "Python" sentences
def findAllSentencesWithPythonWord(text):
    # Extracting text from html structure
    soup = BeautifulSoup(text, features="html.parser").text
    # Splitting into sentences
    myList = split_text_into_sentences(soup, language="en")
    # Return only those, which has exactly "Python" or "python" word inside
    return [word for word in myList if re.search(r"\b[Pp]ython\b", word)]


# Function for extracting all links from html
def findAllLinks(text):
    # Extract all <a> tags
    soup = BeautifulSoup(text, features="html.parser", parse_only=SoupStrainer('a'))
    # Return only thosewith href attribute
    return [link['href'] for link in soup if link.has_attr("href")]

# Set that gives answer for question: Is this site visited already?
visited_websites = set()

# In this exercise I focus only on links that are valid and are absolute.
# For relative links like: /subpage algorithm should 'glue' the website prefix for further processing.
def crawl(start_page, distance, action):
    # Return if depth is too much
    if distance>DEPTH_MAX:
        raise("I don't think you know what you are doing. This type of depth may be dangerous.")
    # If you visited this website, do not process it twice
    if start_page in visited_websites:
        return
    try:
        # 'cause I may fail
        response = req.get(start_page)
    except:
        # If I fail to download page, just return this:
        return (start_page, "ERROR_GET")

    # Fetch all links from webpage
    all_links = findAllLinks(response.text)
    # Convert relative links to absolute links
    all_links = [urljoin(start_page, sublink) for sublink in all_links]

    # Sign this website as visited
    visited_websites.add(start_page)
    # Yield result
    yield (start_page, action(response.text))

    # If distance is greater than 0, then yield rest of them
    if distance>0:
        for elem in all_links:
            yield from crawl(elem, distance-1, action)

# Example: searching sentences containing "Python" word (case insensitive).
for elem in crawl("https://sites.google.com/cs.uni.wroc.pl/boehm/python_parsing", 2, findAllSentencesWithPythonWord):
    print(elem)