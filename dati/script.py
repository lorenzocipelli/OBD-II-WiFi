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
dataset = {}
for i in range(0, len(parameters)):
    parameters[i]= parameters[i].replace('\n', '')
    #print("Index: " , i , " - " , parameters[i])
    key = parameters[i]
    val = 0
    dataset[key]=val

with open('datafile.json', 'w') as f:
    json.dump(dataset, f)
    
json_string = json.dumps(dataset, indent=4)  
print(json_string)


