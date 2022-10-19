# Run  python .\main.py --server 202.65.157.253 --database LIVEBhadravatiGhantaGadi

## Bhadravati Data ##
import pandas as pd
import matplotlib.pyplot as plt
import pymssql
import argparse
import keras as k
import tensorflow as tf
import os
#scripts
from pre import *
from lstm import *
from forecast import *

#Input
num_pred = 7
epochs = 3
lookback = 7

# construct the argument parser and parse the arguments
ap = argparse.ArgumentParser()
ap.add_argument("-ip", "--server", required=True, help="Server IP address")
ap.add_argument("-db", "--database", required=True, help="Database name")
ap.add_argument("-ulbname", "--ulbname", required=True,help="name of the ULB")
ap.add_argument("-hostname", "--hostname", required=True,help="name of the ULB")
args = vars(ap.parse_args())

#HostName
hostname = args["hostname"]
# Directory 
directory = args["ulbname"]

if hostname == "localhost":   

    # Parent Directory path 
    parent_dir = "D:/Rohit/ICTSBM_CMS_AI_TEST_NEW/SwachhBharatAbhiyan.CMS/Images/AI"

else:

    # Parent Directory path 
    parent_dir = "D:/AdvancePublish/ICTSBMCMS_AI/Images/AI"

# Path 
path = os.path.join(parent_dir, directory)

try: 
    os.mkdir(path) 
except OSError as error: 
    path = os.path.join(parent_dir, directory)

# Read data from server
dataframe = df_server(args["server"],args["database"])

# Preprocess
dataframe = preprocess(dataframe)

# Train the lstm model
Model, weight_test, date_test = model(df=dataframe, num_epochs=epochs,look_back=lookback, vb=0)

# Forecast
forecast = predict(df=dataframe,num_prediction = num_pred, model=Model, look_back=lookback)
forecast_dates = predict_dates(df=dataframe,num_prediction = num_pred)

# Plot
plt.figure(figsize=(10,4))
plt.plot(date_test[-15:], weight_test[-15:], color='blue', label='Actual')
lg= "Forecast " + str(forecast_dates[0].date()) + " To " + str(forecast_dates.iloc[-1].date())
plt.plot(forecast_dates,forecast, color='red', label=lg)
plt.xlabel("Date")
plt.ylabel("Total Weight(Tons)")
plt.title("Garbage Generation Forecast of "+ str(num_pred) + " Days")
# plt.xticks(rotation=45)
plt.legend()
plt.grid()
# Save file to the dir
plt.savefig(path+'/DumpYardforecast.png')
#plt.show()
# Run  python main.py --server 202.65.157.253 --database LIVEAdvanceNagpurGhantaGadi --ulbname Nagpur --hostname localhost