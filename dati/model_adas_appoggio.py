from keras import layers, Sequential 
from keras.regularizers import l2
from keras.layers import Dense, LSTM
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
import statistics
from imblearn.over_sampling import KMeansSMOTE
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

#keras.utils.get_custom_objects().update({"f1_m": f1_m})

data = pd.read_csv("dataset.csv")
print(f"There are {len(data)} rows in the dataset.")
test_split = 0.1
X = pd.DataFrame(data, columns=["rpm","iat","accpedal","throttlepos","speed","engineload","abp"]) #"maf",,"runtime"
#X['roadtype'] = X['roadtype'].astype('int')
y = pd.DataFrame(data, columns=["drivestyle"])
print(y.dtypes)
print(y['drivestyle'].unique())
#y['drivestyle'] = y['drivestyle'].astype(str)
y['drivestyle'] = y['drivestyle'].replace('normal', 1)
y['drivestyle'] = y['drivestyle'].replace('sport', 2)
y['drivestyle'] = y['drivestyle'].replace('eco', 3)
print(y.dtypes)
print(X)
print(y)

#trasform datasets in np arrays to be compatible with smote kmeans
X_train_array = X.values
y_train_array = y.values

#trasform datasets in np arrays to be compatible with smote kmeans
smote_kmeans = KMeansSMOTE(random_state = 42,cluster_balance_threshold=0.05)
X_smotekmeans, y_smotekmeans = smote_kmeans.fit_resample(X_train_array, y_train_array)
y_smotekmeans_db = pd.DataFrame(y_smotekmeans , columns= ["drivestyle"])
'''
y_smotekmeans_db.columns[0]
fig = plt.figure(figsize = (8,8))
ax = fig.gca()
y_smotekmeans_db.hist(ax=ax)
plt.show()
'''

#normalization
X = preprocessing.normalize(X_smotekmeans)
y = preprocessing.normalize(y_smotekmeans_db)


# Split the data into training and test sets
#X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

def get_model(n_inputs, n_outputs):
 
 model = Sequential()
 
 model.add(Dense(9, input_dim=n_inputs, kernel_regularizer=l2(0.01), bias_regularizer=l2(0.01), activation='relu'))
 model.add(layers.Dropout(0.2))
 
 #model.add(LSTM(9, input_dim = n_inputs, dropout = 0.2, recurrent_dropout=0.2))
 model.add(Dense(n_outputs, activation='softmax'))
 model.compile(loss='categorical_crossentropy', optimizer= keras.optimizers.Adam(lr=0.01), metrics=['accuracy'])
 #plot_model(model, to_file = "banane.png", show_shapes = True, show_layer_names = True)
 print(model.summary())
 return model

# evaluate a model using repeated k-fold cross-validation
def evaluate_model(X, y):
    results = list()
    n_inputs, n_outputs = X.shape[1], 3
    print(n_inputs)
    print(n_outputs)
    # define evaluation procedure
    callback = EarlyStopping(monitor='loss', patience=3)
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42, stratify = y)
    y_train = to_categorical(y_train, 3)
    y_test = to_categorical(y_test, 3)
    # define model
    model = get_model(n_inputs, n_outputs)
    # fit model
    history = model.fit(X_train, y_train, validation_split = 0.33, batch_size = 3, callbacks=[callback], epochs= 1)
    print(len(history.history['loss']))  # Only 4 epochs are run.
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
print('Accuracy: %.3f (%.3f)' % (statistics.mean(results), std(results)))



