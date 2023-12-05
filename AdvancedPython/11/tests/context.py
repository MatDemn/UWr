import os
import sys

rootDir = os.path.abspath(os.path.join(os.getcwd(), os.pardir))
sys.path.append(rootDir)
sys.path.append(rootDir + "\\src\\")
sys.path.append(rootDir + "\\tests\\")
sys.path.append(rootDir + "\\common\\")
sys.path.append(rootDir + "\\tests\\exc2\\")
sys.path.append(rootDir + "\\tests\\exc5\\")
