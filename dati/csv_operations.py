import pandas as pd

c_normal = 0
c_sport = 0
c_eco = 0

df = pd.read_csv('dataset.csv')
df["throttlepos"] = round(df["throttlepos"]/2.55)
df["accpedal"] = round((df["accpedal"] +37)/2.55)
labels = df["drivestyle"]
print(df)
for label in labels :
    if label == "eco" :
        c_eco += 1
    elif label == "normal" : 
        c_normal += 1
    else :
        c_sport += 1
df.to_csv('dataset.csv', index = False)
print("Number of SPORT istances: " + str(c_sport) + " -> " + str(round((c_sport/len(labels))*100, 2)) + " %")
print("Number of NORMAL istances: " + str(c_normal) + " -> " + str(round((c_normal/len(labels))*100, 2)) + " %")
print("Number of ECO istances: " + str(c_eco) + " -> " + str(round((c_eco/len(labels))*100, 2)) + " %")


#throttle /2.55
#acce ped +37 )/2.55
