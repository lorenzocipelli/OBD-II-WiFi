# OBD-II-WiFi
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

We exploited just the 01 Mode, which allow us to read from the ECU data tied to emission and fuel consumption. 