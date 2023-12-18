import os
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'
from keras import layers, Sequential 
from keras.layers import Dense, LSTM, SimpleRNN, Dropout
import matplotlib.pyplot as plt
import pandas as pd
import joblib
from sklearn.ensemble import RandomForestClassifier
from sklearn.neighbors import KNeighborsClassifier
from sklearn.tree import DecisionTreeClassifier
from sklearn.metrics import accuracy_score, f1_score, confusion_matrix, ConfusionMatrixDisplay
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

def train_test_split_custom(X, y, percentage) :
    train_samples = round(X.shape[0] * (1 - percentage))
    X_train, X_test = X[:train_samples, :], X[train_samples:, :]
    y_train, y_test = y[:train_samples], y[train_samples:]

    return  X_train, X_test, y_train, y_test

def cut_exceeding(X,y) :
    reminder = X.shape[0] % TIMESTEPS
    if reminder > 0 : # rimozione necessaria
        X_fixed = X[:-reminder] # rimozione degli ultimi 'reminder' elementi
        y_fixed = y[:-reminder] # rimozione degli ultimi 'reminder' elementi
        return X_fixed, y_fixed
    else :
        return X, y # nessuna rimozione se non è necessario
    
""" def remove_non_single_label(data, labels) :
    tmp_data = data
    tmp_labels = list(labels['drivestyle'])

    print(tmp_data)
    print(tmp_labels)

    fixed_data = []
    fixed_labels = []
    check = True
    counter = 0
    
    for idx in range(0,len(tmp_labels),TIMESTEPS) :
        for idx2 in range(0,TIMESTEPS) :
            if tmp_labels[idx] != tmp_labels[idx2] :
                check = False
        if check :
            for idx3 in range(0,TIMESTEPS) :
                fixed_data.append(tmp_data[idx + idx3])
                fixed_labels.append(tmp_labels[idx + idx3])
        else :
            counter += 1
        check = True

    print("counter: " + str(counter*5))
    return np.asarray(fixed_data), np.asarray(fixed_labels) """

def get_model_RNN(batch_size, time_steps, number_of_features, n_outputs):
    print("\nBATCH SIZE: " + str(batch_size))
    print("TIME STEPS: " + str(time_steps))
    print("NUMBER OF FEATURES: " + str(number_of_features))
    print("NUMBER OF OUTPUTS: " + str(n_outputs))

    model = Sequential()
    model.add(LSTM(7, batch_input_shape=(batch_size, time_steps, number_of_features), dropout = 0.5, recurrent_dropout=0.5))
    model.add(Dense(16, activation='relu'))
    model.add(Dense(n_outputs, activation='softmax'))
    model.compile(loss='categorical_crossentropy', optimizer= keras.optimizers.Adam(lr=0.01), metrics=['accuracy'])

    print(model.summary())

    return model

def get_model_MLP(number_of_features, n_outputs) :
    print("NUMBER OF OUTPUTS: " + str(n_outputs))
    model = Sequential()
    model.add(Dense(7, input_dim=number_of_features, activation='relu'))
    #model.add(Dropout(0.2))
    model.add(Dense(3, activation = 'relu'))
    #model.add(Dropout(0.2))
    #model.add(Dense(n_outputs, activation='softmax'))
    model.add(Dense(1, activation = 'sigmoid'))
    #model.compile(loss='categorical_crossentropy', optimizer= keras.optimizers.Adam(lr=0.01), metrics=['accuracy'])
    model.compile(loss='binary_crossentropy', optimizer='adam', metrics=['accuracy'])

    print(model.summary())

    return model

# evaluate a model using repeated k-fold cross-validation
def evaluate_model(X, y):
    results = list()

    # define evaluation procedure
    callback = EarlyStopping(monitor='loss', patience=3)
    
    # Split the data into training and test sets
    #X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42, stratify = y)
    X_train, X_test, y_train, y_test = train_test_split_custom(X=X.values, y=y, percentage=0.3)

    #y_train = to_categorical(y_train, dtype="int32") # per RNN
    #y_test = to_categorical(y_test, dtype="int32")  # per RNN
    """
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
    """

    """ print(X_train.shape)
    print(y_train.shape)
    print(X_test.shape)
    print(y_test.shape) """

    # definizione del modello
    # batch_size, number_of_features = X_train.shape[0], X_train.shape[2] # per RNN
    #number_of_features = X_train.shape[1] # per MLP
    #model = get_model_RNN(batch_size, TIMESTEPS, number_of_features, NUM_OF_CLASSES)
    #model = get_model_MLP(number_of_features, NUM_OF_CLASSES)

    model = RandomForestClassifier(n_estimators=200, max_depth=5, random_state=42, n_jobs=-1)
    model.fit(X_train, y_train.values.ravel())
    # make a prediction on the test set
    y_pred = model.predict(X_test)
    # calculate accuracy
    acc_RF = accuracy_score(y_test, y_pred)
    cm_RF = confusion_matrix(y_test, y_pred)
    f1_sco_RF = f1_score(y_test, y_pred)
    # store result
    print("Random Forest")
    print("Accuracy: " + '%.3f' % acc_RF)
    print("F1 Score: " + '%.3f' % f1_sco_RF)

    model1 = KNeighborsClassifier(n_neighbors=3)
    model1.fit(X_train, y_train.values.ravel())
    # make a prediction on the test set
    y_pred1 = model1.predict(X_test)
    # calculate accuracy
    acc_KNN = accuracy_score(y_test, y_pred1)
    cm_KNN = confusion_matrix(y_test, y_pred1)
    f1_sco_KNN = f1_score(y_test, y_pred1)
    # store result
    print("K-Nearest Neighbour: ")
    print("Accuracy: " + '%.3f' % acc_KNN)
    print("F1 Score: " + '%.3f' % f1_sco_KNN)

    model2 = DecisionTreeClassifier()
    model2.fit(X_train, y_train.values.ravel())
    # make a prediction on the test set
    y_pred2 = model2.predict(X_test)

    
    # calculate accuracy
    acc_DT = accuracy_score(y_test, y_pred2)
    cm_DT = confusion_matrix(y_test, y_pred2)
    f1_sco_DT = f1_score(y_test, y_pred2)

    # store result
    print("Decision Tree Classifier: ")
    print("Accuracy: " + '%.3f' % acc_DT)
    print("F1 Score: " + '%.3f' % f1_sco_DT)

    #get_MLP_model
    model3 = get_model_MLP(7, 1)
    #print (X_train)
    #print(y_train.values.ravel())
    #print(y_test.values.ravel())
    model3.fit(X_train, y_train.values.ravel(), epochs = 100, batch_size= 8)
    #test_loss, test_acc = model3.evaluate(X_test, y_test.values.ravel())
    y_pred3 = model3.predict(X_test)
    print(y_pred3)
    y_pred3 = (y_pred3>0.5).astype(int)
    # calculate accuracy
    acc_ML = accuracy_score(y_test, y_pred3)
    cm_ML = confusion_matrix(y_test, y_pred3)
    f1_sco_ML = f1_score(y_test, y_pred3)
    
    # store result
    print("ML model: ")
   
    print("Accuracy: " + '%.3f' % acc_ML)
    print("F1 Score: " + '%.3f' % f1_sco_ML)

    disp_RF = ConfusionMatrixDisplay(confusion_matrix=cm_RF, display_labels=model.classes_)
    
    disp_RF.plot()
    disp_RF.ax_.set_title("Random Forest")

    disp_KNN = ConfusionMatrixDisplay(confusion_matrix=cm_KNN, display_labels=model1.classes_)
    
    disp_KNN.plot()
    disp_KNN.ax_.set_title("K-Nearest Neighbours")

    disp_DT = ConfusionMatrixDisplay(confusion_matrix=cm_DT, display_labels=model2.classes_)
    
    disp_DT.plot()
    disp_DT.ax_.set_title("Decision Tree")

    disp_ML = ConfusionMatrixDisplay(confusion_matrix=cm_ML, display_labels = model.classes_)

    disp_ML.plot()
    disp_ML.ax_.set_title("ML model")

    plt.show()

    # save the model to disk
    """ filename = 'dati/model/finalized_model.pkl'
    with open(filename, 'wb') as file:  
        joblib.dump(model, filename) """

    # fit model
    #history = model.fit(X_train, y_train, validation_split = 0.33, callbacks=[callback], epochs=5, batch_size=10) 
    #print(len(history.history['loss']))
    """ for idx in range(0, len(y_pred)) :
        position = np.argmax(y_pred[idx])
        one_element = [0,0,0]
        one_element[position] = 1
        y_pred[idx] = one_element """

    results.append(acc_RF)
    
    return results

#keras.utils.get_custom_objects().update({"f1_m": f1_m})

TIMESTEPS = 5
NUM_OF_CLASSES = 0

data = pd.read_csv("dataset.csv")
data = data[data['drivestyle'] != "normal"] # semplificazione
#print(f"There are {len(data)} rows in the dataset.")

# ["rpm","maf","iat","accpedal","speed","engineload","abp"] acc -> 0.935, f1 -> 0.874
# ["rpm","maf","iat","speed","engineload"] acc -> 0.935, f1 -> 0.874
# ["rpm","maf","speed","engineload"] acc -> 0.780, f1 -> 0.711
# ["rpm","maf","engineload"] acc -> 0.815, f1 -> 0.741
X = pd.DataFrame(data, columns=["rpm","maf","iat","accpedal","speed","engineload","abp"]) # "throttlepos" escluso
y = pd.DataFrame(data, columns=["drivestyle"])

# [869,163,20,14,67] # eco testing
# [2022,968,9,106,72] # sport testing

NUM_OF_CLASSES = len(y['drivestyle'].unique())

#print(X)
#print(y)

pd.plotting.scatter_matrix(X)
plt.show()

y['drivestyle'] = y['drivestyle'].replace('normal', 2)
y['drivestyle'] = y['drivestyle'].replace('sport', 0)
y['drivestyle'] = y['drivestyle'].replace('eco', 1)

#trasform datasets in np arrays to be compatible with smote kmeans
#X_train_array = X.values
y_train_array = y.values

# normalization
#X = preprocessing.normalize(X_train_array)

# evaluate model
results = evaluate_model(X, y) # UNCOMMENT TO TEST
# summarize performance
#print('Accuracy: %.3f (%.3f)' % (statistics.mean(results), std(results)))
