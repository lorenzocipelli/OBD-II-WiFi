from keras import layers, Sequential
from keras.layers import Dense
from sklearn.model_selection import train_test_split
import matplotlib.pyplot as plt
import pandas as pd
from sklearn.datasets import make_multilabel_classification
from sklearn.model_selection import train_test_split
from sklearn.multioutput import MultiOutputClassifier
from sklearn.neighbors import KNeighborsClassifier
from sklearn.metrics import accuracy_score, hamming_loss
from sklearn.model_selection import RepeatedKFold

data = pd.read_csv("dataset.csv")
print(f"There are {len(data)} rows in the dataset.")
test_split = 0.1
X = pd.DataFrame(data, columns=["rpm","maf","iat","accpedal","throttlepos","speed","engineload","runtime","abp","roadtype"])
y = pd.DataFrame(data, columns=["drivestyle"])

print(X)
print(y)
# Generate synthetic multi-label dataset
#X, y = make_multilabel_classification(n_samples=100, n_features=10, n_classes=3, random_state=42)

# Split the data into training and test sets
#X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

def get_model(n_inputs, n_outputs):
 model = Sequential()
 model.add(Dense(20, input_dim=n_inputs, kernel_initializer='he_uniform', activation='relu'))
 model.add(Dense(n_outputs, activation='sigmoid'))
 model.compile(loss='categorical_crossentropy', optimizer='adam')
 return model


# evaluate a model using repeated k-fold cross-validation
def evaluate_model(X, y):
    results = list()
    n_inputs, n_outputs = X.shape[1], y.shape[1]
    print(n_inputs)
    print(n_outputs)
    # define evaluation procedure
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)
    # define model
    model = get_model(n_inputs, n_outputs)
    # fit model
    model.fit(X_train, y_train, verbose=0, epochs=100)
    # make a prediction on the test set
    yhat = model.predict(X_test)
    # round probabilities to class labels
    yhat = yhat.round()
    # calculate accuracy
    acc = accuracy_score(y_test, yhat)
    # store result
    print('>%.3f' % acc)
    results.append(acc)
    return results

# evaluate model
results = evaluate_model(X, y)
# summarize performance
print('Accuracy: %.3f (%.3f)' % (mean(results), std(results)))