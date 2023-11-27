# - pip install uvicorn
# - pip install fastapi
# - pip install joblib

import uvicorn
from fastapi import FastAPI
import joblib

app = FastAPI()

"""
joblib_in = open("nostro_modello.joblib","rb")
model=joblib.load(joblib_in)
"""

@app.get('/')
def index():
    return {'message': 'Index dell\'API per predizione del modello'}

if __name__ == '__main__':
    uvicorn.run(app, host='127.0.0.1', port=8000)

""" app = FastAPI()
joblib_in = open("car-recommender.joblib","rb")
model=joblib.load(joblib_in)


@app.get('/')
def index():
    return {'message': 'Cars Recommender ML API'}

@app.post('/car/predict')
def predict_car_type(data:CarUser):
    data = data.dict()
    age=data['age']
    gender=data['gender']

    prediction = model.predict([[age, gender]])
    
    return {
        'prediction': prediction[0]
    }

if __name__ == '__main__':
    uvicorn.run(app, host='127.0.0.1', port=8000) """