import pandas as pd

c_normal = 0
c_sport = 0
c_eco = 0

df = pd.read_csv('dati/dataset.csv')

labels = df["drivestyle"]

for label in labels :
    if label == "eco" :
        c_eco += 1
    elif label == "normal" : 
        c_normal += 1
    else :
        c_sport += 1

print("Number of SPORT istances: " + str(c_sport) + " -> " + str(round((c_sport/len(labels))*100, 2)) + " %")
print("Number of NORMAL istances: " + str(c_normal) + " -> " + str(round((c_normal/len(labels))*100, 2)) + " %")
print("Number of ECO istances: " + str(c_eco) + " -> " + str(round((c_eco/len(labels))*100, 2)) + " %")
