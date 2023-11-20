from keras import layers
from tensorflow import keras
import tensorflow as tf

from sklearn.model_selection import train_test_split
from ast import literal_eval

import matplotlib.pyplot as plt
import pandas as pd
import numpy as np


data = pd.read_csv("dataset.csv")
print(f"There are {len(data)} rows in the dataset.")
test_split = 0.1
X = pd.DataFrame(data, columns=["rpm","maf","iat","accpedal","throttlepos","speed","engineload","runtime","abp","roadtype"])
y = pd.DataFrame(data, columns=["drivestyle"])
print(X)
print(y)