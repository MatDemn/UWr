'''
    Mateusz Zajac (298654)
    Advanced Python
    List 7
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
# Thread related functions
import threading as thr
# Depth more than 2 may dig up too deep. Assuming start_page has N unique links
# and those links also has N links etc, etc we have aprox. O(N^(distance)) time complexity.
# Quadratic search is enough I think.
DEPTH_MAX = 2

lock = thr.Lock()

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

# Results to yield iterate
resultIterator = []

# In this exercise I focus only on links that are valid and are absolute.
# For relative links like: /subpage algorithm should 'glue' the website prefix for further processing.
def crawl_it(start_page, distance, action):
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
    # As I checked, set add is NOT thread-safe
    # Therefore, I have to acquire lock and release it after
    # finishing adding to set.
    lock.acquire()
    visited_websites.add(start_page)
    lock.release()

    # Add result to iterator list
    resultIterator.append((start_page, action(response.text)))

    # If distance is greater than 0, then yield rest of them
    if distance>0:
        # For every link on website
        for elem in all_links:
            # If there is too much threads, wait
            if thr.active_count == 5:
                thr.current_thread().join()
            # Otherwise, make a new thread
            t = thr.Thread(target=crawl_it, args=[elem, distance-1, action])
            # And start it
            t.start()

# Main crawl function
def crawl(start_page, distance, action):
    # Make new thread with crawl_it
    t = thr.Thread(target=crawl_it, args=[start_page, distance, action])
    # Start it
    t.start()
    # Wait for all results
    while len(thr.enumerate()) > 1:
        pass
    # Yield them
    for elem in resultIterator:
        yield elem

# Example: searching sentences containing "Python" word (case insensitive).
for elem in crawl("https://sites.google.com/cs.uni.wroc.pl/boehm/python_parsing", 2, findAllSentencesWithPythonWord):
    print(elem)

'''
    Set.add is not thread-safe. Therefore, 
    I added lock mechanism to prevent two threads
    operating on it on the same time.

    List.append is thread-safe.
'''