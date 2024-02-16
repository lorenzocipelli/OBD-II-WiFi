# OBD-II-WiFi
![](https://github.com/lorenzocipelli/OBD-II-WiFi/blob/master/images/ADAS_short.gif)
Today more than ever environmental awareness has led the way toward the development of many systems inside our own cars that lead to a much more safe and eco-friendly drive. 
The drive-style classification is one of those feature that car manufacturer fit in their cars to encourage modern drivers to behave carefully on the road. Often times those system are under proprietary license, so it's impossible to "export" them into a global fleet of vehicles.

OBD-II-WiFi is our open-source, simple and easily understandable graphical application not just to classify the drive-style, but also train a Machine Learning model on your drive-style and use it afterwards. 

## Goal
Our prupose was to develop a lite program that allowed for a real-time classification of the drive-style. Our program is able to define if driving in an **ecological** or **sporty** manner. 
The system was developed by initially collect kilometers of driving data and then train a ML model, in our case a Bagging model such as Random Forest. 

## Data
The first concern was to collect driving data and label it. To explain how we made it we first have to explain what hardware we used to exchange data with the ECU of the car (Volkswagen Polo 2015). 

### ELM 327
From the  [ELM 327](https://en.wikipedia.org/wiki/ELM327) Wikipedia's page:
> The ELM327 is a programmed microcontroller produced for translating the on-board diagnostics (OBD) interface found in most modern cars. The ELM327 command protocol is one of the most popular PC-to-OBD interface standards and is also implemented by other vendors
>

So our hardware device used to connect to exchange data with the ECU follows the ELM 327 standard. We decided to use this device because it give us the opportunity to rapildy send and receive messages with the ECU of our car. The OBD-2 interface works following the relative communication protocol, exploiting a mechanism of PIDs, which are essentially codes that allow the sender to receive back some useful information from the car. Example of the device look:

![obd2 device](/images/obd_2_device.png "OBD 2 device appearence.")

### PIDs
OBD-II PIDs (On-board diagnostics Parameter IDs) are codes used to request data from a vehicle (SAE standard J1979 defines many OBD-II PIDs). The PIDs eco-stystem works in Modes, here in the following all the possible working Modes:

| Mode (hex)  | Description |
| ------------- |:-------------:|
| 01     | Show current data     |
| 02      | Show freeze frame data    |
| 03      | Show stored Diagnostic Trouble Code    |
| 04     | Clear Diagnostic Trouble Codes and stored values     |
| 05     | Test results, oxygen sensor monitoring (non CAN only)    |
| 06     | Test results, other component/system monitoring     |
| 07     | Show pending Diagnostic Trouble Codes (detected during current or last driving cycle)     |
| 08     | Control operation of on-board component/system     |
| 09     | Request vehicle information    |
| 0A     | Permanent Diagnostic Trouble Codes (DTCs) (Cleared DTCs)  |

We exploited just the 01 Mode, which allow us to read from the ECU data tied to emission and fuel consumption. The codes we used are: 
| PIDs (hex)  | Description |
| ------------- |:-------------:|
| 04     | Calculated engine load    |
| 0C     | Engine speed    |
| 0D     | Vehicle speed     |
| 0F     | Intake air temperature    |
| 10     | Mass air flow sensor (MAF)    |
| 11     | 	Throttle position    |
| 1F     | Run time since engine start    |
| 33     | Absolute Barometric Pressure   |
| 49     | Accelerator pedal position D    |
| 1C     | OBD standards this vehicle conforms to  |

We put 1C (OBD standards this vehicle conforms to) as last because this PID serve as synchronising mechanism to update the CSV file (data set) or use the trained model to predict the last data batch drive-style. We talk about "data batch" because both the row appending inside the CSV and model prediction are done once all the data from the above table is returned from the OBD-II device.
For a complete list of all the Standard PIDs we would like to suggest checking out the related [wikipedia](https://en.wikipedia.org/wiki/OBD-II_PIDs) page, which we found to be quite exhaustive.

Beneath you can see what the dataset looks like after some driving in both eco and sport mode. It is worth mentioning that the labeling is semi-automatically made by clicking the right button for the driving stryle currently applied, allowing to label data rows as eco, sport or normal (in the following we will explain why normal wasn't used). 

![dataset](/images/dataset.PNG "Built dataset")

Normal drive-style wasn't considered for classification because the variance in data between [eco, normal] and between [normal, sport] wasn't that much, leading to a significant drop in accuracy. So we decided to just use eco and sport, which are quite different at data level, leading to a much more reliable classification. Of couse this is a relaxation in our objective, but this software isn't meant to replace any professional driving-style monitoring system.

## Maching Learning Models
Of couse more than one model was trained on the collected dataset, a couple had similar results, but in the end the model which performed the best in the classification task was Random Forest (as mentioned before). The tested models are:
* **Random Forest**
  * _Accuracy_ : 0.884
  * _F1 Score_ : 0.924
* **kNN**
  * _Accuracy_ : 0.856
  * _F1 Score_ : 0.902
* **Decision Tree Classifier**
  * _Accuracy_ : 0.727
  * _F1 Score_ : 0.797

Even Deep Learning models such as ANN or RNN (because of time dependant predictions) where evaluated, however, both led to the underfitting problem.

## Python API
Once our RF has been trained and locally saved, we can use it in "release mode" in our back-end to predict real-time data coming from the ECU. Of course the same data type used to build up the dataset. Basically the API wait for a POST method with the new data and the local model makes his prediction. Finally the API sends back to the front-end (our graphical application in C#) the prediction (remember that the prediction is either eco or sport). The prediction returned isn't necessarily what the RF predicted, in fact this would cause the program to rapidly flick between eco and sport mode in case of uncertainty prediction; so the solution we proposed is to check which class is predicted more frequently out of the last BUFFER_SIZE predictions (which is a costant, we set it to 5). This gives to the driver-style display a more stable behavior.

# Credits
Devs:
* [Annalisa Panicieri](https://github.com/Annalisaa1) - Università di Parma
* [Lorenzo Cipelli](https://lorenzocipelli.github.io/) - Università di Parma
