from keras import layers, Sequential 
from keras.regularizers import l2
from keras.layers import Dense, LSTM, SimpleRNN
from sklearn.model_selection import train_test_split
import matplotlib.pyplot as plt
import pandas as pd
from sklearn.datasets import make_multilabel_classification
from sklearn.model_selection import train_test_split
from sklearn.multioutput import MultiOutputClassifier
from sklearn.neighbors import KNeighborsClassifier
from sklearn.metrics import accuracy_score, hamming_loss, confusion_matrix, ConfusionMatrixDisplay
from sklearn.model_selection import RepeatedKFold
from sklearn import preprocessing
from keras.utils import to_categorical, plot_model  
from numpy import std
import numpy as np
import statistics
#from imblearn.over_sampling import KMeansSMOTE
from keras import backend as K
from keras.callbacks import EarlyStopping
import keras

#ML
from sklearn.ensemble import RandomForestClassifier

def recall_m(y_true, y_pred):
    true_positives = K.sum(K.round(K.clip(y_true * y_pred, 0, 1)))
    possible_positives = K.sum(K.round(K.clip(y_true, 0, 1)))
    recall = true_positives / (possible_positives + K.epsilon())
    return recall

def precision_m(y_true, y_pred):
    true_positives = K.sum(K.round(K.clip(y_true * y_pred, 0, 1)))
    predicted_positives = K.sum(K.round(K.clip(y_pred, 0, 1)))
    precision = true_positives / (predicted_positives + K.epsilon())
    return precision

def f1_m(y_true, y_pred):
    precision = precision_m(y_true, y_pred)
    recall = recall_m(y_true, y_pred)
    return 2*((precision*recall)/(precision+recall+K.epsilon()))

def get_model(batch_size, time_steps, number_of_features, n_outputs):
    print("\nBATCH SIZE: " + str(batch_size))
    print("TIME STEPS: " + str(time_steps))
    print("NUMBER OF FEATURES: " + str(number_of_features))
    print("NUMBER OF OUTPUTS: " + str(n_outputs))

    model = Sequential()
    model.add(LSTM(15, batch_input_shape=(batch_size, time_steps, number_of_features), dropout = 0.2, recurrent_dropout=0.2))
    model.add(Dense(n_outputs, activation='softmax'))
    model.compile(loss='categorical_crossentropy', optimizer= keras.optimizers.Adam(lr=0.01), metrics=['accuracy'])

    print(model.summary())

    return model

# evaluate a model using repeated k-fold cross-validation
def evaluate_model(X, y):
    results = list()

    # define evaluation procedure
    callback = EarlyStopping(monitor='loss', patience=3)

    # Split the data into training and test sets
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42, stratify = y)
    y_train = to_categorical(y_train, dtype="int32")
    print(y_train)
    y_test = to_categorical(y_test, dtype="int32")
    

    # rimozione delle istanze in eccesso per avere un numero sempre uguale di timesteps per ogni batch
    X_train, y_train = cut_exceeding(X_train, y_train)
    X_test, y_test = cut_exceeding(X_test, y_test)
    # posso ora fare il rashape essendo sicuro di avere dimensioni intere (batch_size, time_steps, number_of_features)
    # da mandare in input allo strato LSTM del modello
    X_train = X_train.reshape((int(X_train.shape[0] / TIMESTEPS), TIMESTEPS, X_train.shape[1]))
    X_test = X_test.reshape((int(X_test.shape[0] / TIMESTEPS), TIMESTEPS, X_test.shape[1]))
    # mantengo come label solamente l'ultima del batch, quelle precedenti non mi servono
    y_train = y_train[np.arange(len(y_train)) % TIMESTEPS == (TIMESTEPS - 1)]
    y_test = y_test[np.arange(len(y_test)) % TIMESTEPS == (TIMESTEPS - 1)]

    # definizione del modello
    batch_size, number_of_features = X_train.shape[0], X_train.shape[2]
    model = get_model(batch_size, TIMESTEPS, number_of_features, NUM_OF_CLASSES)

    # fit model
    history = model.fit(X_train, y_train, validation_split = 0.33, callbacks=[callback], epochs=100, batch_size=1) 
    print(len(history.history['loss']))
    # make a prediction on the test set
    y_pred = model.predict(X_test)
    for idx in range(0, len(y_pred)) :
        position = np.argmax(y_pred[idx])
        one_element = [0,0,0]
        one_element[position] = 1
        y_pred[idx] = one_element

    print(y_pred)
 
    print(y_test)
    # round probabilities to class labels
    y_pred = y_pred.round()
    # calculate accuracy
    acc = accuracy_score(y_test, y_pred)

    # store result
    print('%.3f' % acc)
    results.append(acc)
    
    return results

def cut_exceeding(X,y) :
    reminder = X.shape[0] % TIMESTEPS
    if reminder > 0 : # rimozione necessaria
        X_fixed = X[:-reminder] # rimozione degli ultimi 'reminder' elementi
        y_fixed = y[:-reminder] # rimozione degli ultimi 'reminder' elementi
        return X_fixed, y_fixed
    else :
        return X, y # nessuna rimozione se non è necessario

#keras.utils.get_custom_objects().update({"f1_m": f1_m})

TIMESTEPS = 5
NUM_OF_CLASSES = 0

data = pd.read_csv("dati/dataset.csv")
print(f"There are {len(data)} rows in the dataset.")

X = pd.DataFrame(data, columns=["rpm","maf","iat","accpedal","speed","engineload","abp"]) # "throttlepos" escluso
y = pd.DataFrame(data, columns=["drivestyle"])

NUM_OF_CLASSES = len(y['drivestyle'].unique())

print(X)
print(y)

y['drivestyle'] = y['drivestyle'].replace('normal', 0)
y['drivestyle'] = y['drivestyle'].replace('sport', 1)
y['drivestyle'] = y['drivestyle'].replace('eco', 2)

#trasform datasets in np arrays to be compatible with smote kmeans
X_train_array = X.values
y_train_array = y.values

#trasform datasets in np arrays to be compatible with smote kmeans
""" smote_kmeans = KMeansSMOTE(random_state = 42,cluster_balance_threshold=0.05)
X_smotekmeans, y_smotekmeans = smote_kmeans.fit_resample(X_train_array, y_train_array)
y_smotekmeans_db = pd.DataFrame(y_smotekmeans , columns= ["drivestyle"])

y_smotekmeans_db.columns[0]
fig = plt.figure(figsize = (8,8))
ax = fig.gca()
y_smotekmeans_db.hist(ax=ax)
plt.show() """

# normalization
X = preprocessing.normalize(X_train_array)

# evaluate model
results = evaluate_model(X, y)
# summarize performance
print('Accuracy: %.3f (%.3f)' % (statistics.mean(results), std(results)))
