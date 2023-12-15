# - pip install uvicorn
# - pip install fastapi
# - pip install joblib

import uvicorn
from fastapi import FastAPI
import joblib
from pydantic import BaseModel

class CarData(BaseModel):
  rpm: int
  maf: int
  iat: int
  speed: int
  engineload: int

app = FastAPI()
joblib_in = open("dati/model/finalized_model.pkl","rb")
model = joblib.load(joblib_in)

predictions = []

@app.get('/')
def index():
    return {
        'state': "OK",
        'message': 'Index dell\'API per predizione del modello'
    }

@app.post('/predict')
def predict_car_type(instance:CarData):
    instance = instance.model_dump()

    rpm=instance['rpm']
    maf=instance['maf']
    iat=instance['iat']
    speed=instance['speed']
    engineload=instance['engineload']
    
    pred = model.predict([[rpm, maf, iat, speed, engineload]])

    #prediction.append(pred)

    if pred == 1 :
        return {
            'state': "OK",
            'prediction': "SPORT"
        }
    elif pred == 2 :
        return {
            'state': "OK",
            'prediction': "ECO"
        }   
    else :
        return {
            'state': "ERROR",
            'message': "Cannot Predict"
        }  

if __name__ == '__main__':
    uvicorn.run(app, host='127.0.0.1', port=8000)