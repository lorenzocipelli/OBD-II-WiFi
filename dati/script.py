from bs4 import BeautifulSoup
import requests
import sys
import json

parameters = []
sys.stdin.reconfigure(encoding='utf-8')
sys.stdout.reconfigure(encoding='utf-8')
website = requests.get('https://en.wikipedia.org/wiki/OBD-II_PIDs')
soup = BeautifulSoup(website.content, 'html.parser')
tables = soup.find_all('table')
rows = tables[2].tbody.find_all('tr')

for row in rows :
    cols_row = row.find_all('td')
    if len(cols_row)>0:
        parameters.append(cols_row[3].text)

dataset = {'PIDs':[]}

for i in range(1, len(parameters)):
    parameters[i] = parameters[i].replace('\n', '')
    #print("Index: " , i , " - " , parameters[i])
    dataset['PIDs'].append({"description": parameters[i],
                            "value": 0})
    
#dataset.pop(parameters[0])
with open('dati/datafile.json', 'w') as f:
    json.dump(dataset, f)
    
json_string = json.dumps(dataset, indent=4)  
print(json_string)


