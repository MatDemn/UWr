'''
    Mateusz Zajac (298654)
    Advanced Python
    List 4
    Ex 3
'''

import math
import os

# Colors test
print('\033[91m\u2588\033[0m\033[92m\u2588\033[0m\033[93m\u2588\033[0m\033[94m\u2588\033[0m\033[95m\u2588\033[0m')

# Frame chars
# FRAME_{(H)orizontal|(V)ertical}{(L)ine}
FRAME_HL = "\u2500"
FRAME_VL = "\u2502"

# FRAME_{(T)op|(M)iddle|(D)own}{(L)eft|(M)iddle|(R)ight}
FRAME_TL = "\u250C"
FRAME_TM = "\u252C"
FRAME_TR = "\u2510"

FRAME_ML = "\u251C"
FRAME_MM = "\u253C"
FRAME_MR = "\u2524"

FRAME_DL = "\u2514"
FRAME_DM = "\u2534"
FRAME_DR = "\u2518"

'''
    Group_x, group_y - indicates how much groups are there horizontally and vertically
    elems_x, elems_y - indicates how much elements are there in group horizontally and vertically

    Default behaviour is 9x9 sudoku. But this print function can print even some weird ones.
    Example shown below function.
'''
def print_sudoku(diagram, group_x = 3, group_y = 3, elems_x = 3, elems_y = 3, history = []):
    size_y = group_y*elems_y
    max_elem_length = int(math.log10(elems_x*elems_y))+1
    def section1():
        print(
            FRAME_TL,
            ((1+elems_x*(max_elem_length + 1))*FRAME_HL + FRAME_TM)*(group_x-1),
            (1+elems_x*(max_elem_length + 1))*FRAME_HL,FRAME_TR, 
            sep=""
        )
    
    def section2():
        for line in range(size_y):
            for gx in range(group_x):
                print(FRAME_VL, end ="")
                print(" ", end="")
                for ex in range(elems_x):
                    if diagram[line][gx*elems_x + ex]:
                        if (line, gx*elems_x + ex) in history:
                            print(f'\033[92m{diagram[line][gx*elems_x + ex]:{max_elem_length}}\033[0m', end = "")
                        else:
                            print(f'{diagram[line][gx*elems_x + ex]:{max_elem_length}}', end = "")
                        print(" ", end="")
                    else:
                        print(f"{(max_elem_length-1)*' '}" + "\u06D4", end = "")
                        print(" ", end="")
            print(FRAME_VL, end = "")
            print("") 
            if line%elems_y == elems_y-1 and line != size_y-1:
                section3() 
    
    def section3():
        print(
            FRAME_ML, 
            ((1+elems_x*(max_elem_length + 1))*FRAME_HL + FRAME_MM)*(group_x-1), 
            (1+elems_x*(max_elem_length + 1))*FRAME_HL, 
            FRAME_MR, 
            sep=""
        )
    
    def section4():
        print(
            FRAME_DL,
            ((1+elems_x*(max_elem_length + 1))*FRAME_HL + FRAME_DM)*(group_x-1),
            (1+elems_x*(max_elem_length + 1))*FRAME_HL,
            FRAME_DR,
            sep=""
        )
    
    # printing out correct sudoku table
    section1()
    section2()
    section4() 
'''
    Print is divided to 4 different sections, where D is a sudoku group 3x3 containing numbers. 
    (spaces inserted between characters horizontally for better visibility in this example):

    Section 1: ┌ ─ ┬ ─ ┬ ─ ┐
    Section 2: │ D │ D │ D │
    Section 3: ├ ─ ┼ ─ ┼ ─ ┤
    Section 2: │ D │ D │ D │
    Section 3: ├ ─ ┼ ─ ┼ ─ ┤
    Section 2: │ D │ D │ D │
    Section 4: └ ─ ┴ ─ ┴ ─ ┘

    The tricky one is the second section, because I have to print not only characters for frame, 
    but also numbers 
'''
'''
    By default, this function solves 9x9 sudoku cube.
    But it is possible to drop something different here.
    The elems_x is number of numbers horizontally in one group 
    (9x9 sudoku contains 3 groups horizontally and 3 vertically, 
    with 3 numbers horizontally and vertically inside each group).
    Similar for elems_y, but vertically in group.
    The return_history is only for nicer print.
    print_sudoku checks history and if cell is in it, it prints it green
    to indicate change done by algorithm.
'''
def solving_sudoku(s, elems_x = 3, elems_y = 3, return_history=False):
    # X and Y size of diagram 
    size_x = len(s[0])
    size_y = len(s)
    # Possible numbers to put into diagram
    possible_numbers = set(range(1,elems_x*elems_y+1))

    # history holds list of nodes to visit (and visited)
    # I just iterate through this list and check only cells
    # that I have to check.
    history = []
    # posibilities is a dictionary of lists. It holds:
    # [0] - possible numbers to insert to cell
    # [1] - at which number this cell is. sort of iterator for [0]
    possibilities = {}

    # checks if number can be inserted in (y,x) place
    def check_neighbours(y,x, val):
        # Check horizontally
        for x_i in range(0, x):
            if s[y][x_i] == val: return False
        # Check vertically
        for y_i in range(0, y):
            if s[y_i][x] == val: return False
        
        # Check cube
        start_x = x - x%elems_x
        start_y = y - y%elems_y
        for y_i in range(start_y, y):
            for x_i in range(start_x, x):
                if y_i == y and x_i == x: break
                if s[y_i][x_i] == val: return False

        return True

    # this function lists possible numbers to put in (y,x) place in a diagram
    def list_possibilities(y, x):
        result = set()
        # I have to check:
        # 1) left and right
        # 2) up and down
        # 3) whole cube

        # 1)
        for i in range(size_x):
            if i==x: continue
            if s[y][i] != None: result.add(s[y][i])

        # 2)
        for j in range(size_y):
            if j==y: continue
            if s[j][x] != None: result.add(s[j][x])

        # 3)
        start_x = x - x%elems_x
        start_y = y - y%elems_y
        for j in range(start_y, start_y+elems_y):
            if j == y: continue
            for i in range(start_x, start_x+elems_x):
                if i == x: continue
                if s[j][i] != None: result.add(s[j][i])
        return result


    # creating history to iterate through it and possibilities for every empty cell
    for y_i in range(size_y):
        for x_i in range(size_x):
            if s[y_i][x_i] == None:
                history.append((y_i, x_i))
                possibilities[(y_i,x_i)] = [sorted(list(possible_numbers - list_possibilities(y_i,x_i))),0]

    # main loop for iterating through table
    # try and backtrack algorithm
    i = 0
    while i<len(history):
        # unpack x and y
        y_history,x_history = history[i]

        # short version for easier access
        options = possibilities[history[i]][0]
        iterator = possibilities[history[i]][1]
        
        # While I can iterate through possibilities 
        # and neighbours prevent me from putting number there
        while(len(options) != iterator and (not check_neighbours(y_history,x_history,options[iterator]))):
            # I iterate until I jump into right value or deplete possibilities
            iterator += 1

        # I found good number to put here
        if len(options) != iterator:
            # change digit to another one
            s[y_history][x_history] = options[iterator]
            # and go forward in history
            i = i+1
        # if you're here, possibilities are depleted
        # you have to roll back and check different digits for different cells
        else:
            # if you've depleted whole history, then this sudoku doesn't have a solution
            if i-1 < 0:
                return None
            
            #if there is a history, then revert changes made and look for another solution
            # in past history
            possibilities[history[i]][1] = 0
            possibilities[history[i-1]][1] += 1
            s[y_history][x_history] = None
            i = i-1
    # if user wants to return history to nicer print
    if return_history:
        return s, history
    return s

# TESTS FOR SOLVER
for filename in os.listdir(os.getcwd()+"/tests"):
    with open(os.path.join(os.getcwd()+"/tests", filename), 'r') as t: # open in readonly mode
        test = []
        Lines = t.readlines()
        for line in Lines:
            line = line.rstrip('\n')
            test.append(line.split(" "))
        for y_it in range(len(test)):
            for x_it in range(len(test[y_it])):
                if test[y_it][x_it] == '.': test[y_it][x_it] = None
                else: test[y_it][x_it] = int(test[y_it][x_it])

        with open(os.path.join(os.getcwd()+"\solutions", filename), 'r') as s:
            sol = []
            Lines = s.readlines()
            for l in Lines:
                l = l.rstrip('\n')
                sol.append(l.split(" "))
            for y_it in range(len(sol)):
                for x_it in range(len(sol[y_it])):
                    sol[y_it][x_it] = int(sol[y_it][x_it])

            assert solving_sudoku(test) == sol, filename

# TESTS FOR PRINTER
for filename in os.listdir(os.getcwd()+"/print_tests"):
    test = []
    with open(os.path.join(os.getcwd()+"/print_tests", filename), 'r') as f:
        Lines = f.readlines()
        for line in Lines:
            line = line.rstrip('\n')
            test.append(line.split(" "))
        for y_it in range(len(test)):
            for x_it in range(len(test[y_it])):
                if test[y_it][x_it] == '.': test[y_it][x_it] = None
                else: test[y_it][x_it] = int(test[y_it][x_it])
    dims = [ int(x) for x in filename.rstrip(".txt").split("_")]
    print_sudoku(test, dims[0], dims[1], dims[2], dims[3])

# TESTS FOR NICER PRINTER
with open("./tests/9x9_3.txt", "r") as f:
    test = []
    Lines = f.readlines()
    for line in Lines:
        line = line.rstrip('\n')
        test.append(line.split(" "))
    for y_it in range(len(test)):
        for x_it in range(len(test[y_it])):
            if test[y_it][x_it] == '.': test[y_it][x_it] = None
            else: test[y_it][x_it] = int(test[y_it][x_it])
    x = solving_sudoku(test, return_history=True)
    print_sudoku(x[0], history=x[1])


# Tried to test it on 25x25 grid, but failed miserably.
# Combinatorics show that 25x25 task done by this method is very
# inefficient and needs too much calculations.
# It's about A^N where A is number of possible numbers to fill cell with
# and N is number of empty cells to fill.
# For bigger cases, another method should be used.
test = []
with open("25x25.txt", "r") as f:
    Lines = f.readlines()
    for line in Lines:
        line = line.rstrip('\n')
        test.append(line.split(" "))
    for y_it in range(len(test)):
        for x_it in range(len(test[y_it])):
            if test[y_it][x_it] == '.': test[y_it][x_it] = None
            else: test[y_it][x_it] = int(test[y_it][x_it])

    print_sudoku(test, 5, 5, 5, 5)
