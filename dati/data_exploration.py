import numpy as np
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt

from sklearn.decomposition import PCA
from sklearn.manifold import TSNE

# Data-preprocessing: Standardizing the data
from sklearn.preprocessing import StandardScaler

cols = ["rpm","maf","iat","speed","engineload"]

df_init = pd.read_csv('dati/dataset.csv')
df_init = df_init[df_init['drivestyle'] != "eco"] # semplificazione

df_labels = df_init["drivestyle"]

NUM_OF_CLASSES = len(df_labels.unique())
print("Numero di classi: " + str(NUM_OF_CLASSES))

df_filtered = df_init[cols]

standardized_data = StandardScaler().fit_transform(df_filtered)
print(standardized_data.shape)

data_1000 = standardized_data[0:1000, :]
labels_1000 = df_labels[0:1000]

model = TSNE(n_components = 2, random_state = 123)

tsne_data = model.fit_transform(data_1000)

tsne_df = pd.DataFrame()
tsne_df["y"] = labels_1000
tsne_df["comp-1"] = tsne_data[:,0]
tsne_df["comp-2"] = tsne_data[:,1]
 
# Plotting the result of tsne
sns.scatterplot(x="comp-1", y="comp-2", hue=tsne_df.y.tolist(),
                palette=sns.color_palette("hls", 2),
                data=tsne_df).set(title="Normal, Sport t-SNE data projection")

plt.show()