# - pip install uvicorn
# - pip install fastapi
# - pip install joblib

import os
import joblib
import uvicorn

from fastapi import FastAPI
from pydantic import BaseModel
from collections import Counter

class CarData(BaseModel):
  rpm: int
  maf: int
  iat: int
  speed: int
  engineload: int

#print('getcwd:      ', os.getcwd())

app = FastAPI()
#joblib_in = open("dati/model/finalized_model.pkl","rb")
joblib_in = open("dati/model/finalized_model.pkl","rb")
model = joblib.load(joblib_in)

predictions = []
BUFFER_SIZE = 5

def most_common(my_list):
    data = Counter(my_list)
    return data.most_common(1)[0][0]

@app.get('/')
def index():
    return {
        'state': "OK",
        'message': 'Index dell\'API per predizione del modello'
    }

@app.post('/predict')
def predict_car_type(instance:CarData):
    instance = instance.model_dump()

    # qui prendo solamente le informazioni necessarie per la predizione
    rpm = instance['rpm']
    maf = instance['maf']
    iat = instance['iat']
    speed = instance['speed']
    engineload = instance['engineload']

    # il modello addestrato effettua la predizione
    pred = model.predict([[rpm, maf, iat, speed, engineload]])
    predictions.append(pred)

    if len(predictions) < BUFFER_SIZE :
        predictions.append(pred)
        return {
            'state': "BUFFERING",
            'message': "System needs more data.\nWait a few seconds!"
        } 
    else : # se abbiamo abbastanza predizioni nel buffer
        final_pred = most_common(predictions) # prendo il valore con più ricorrenze
        del predictions[0] # tolgo il primo elemento della lista per il giro successivo
        if final_pred == 0 :
            return {
                'state': "OK",
                'prediction': "SPORT"
            }
        elif final_pred == 1 :
            return {
                'state': "OK",
                'prediction': "ECO"
            }
        else :
            return {
                'state': "ERROR",
                'message': "API error while trying to predict!"
            }

if __name__ == '__main__':
    uvicorn.run(app, host='127.0.0.1', port=8000)