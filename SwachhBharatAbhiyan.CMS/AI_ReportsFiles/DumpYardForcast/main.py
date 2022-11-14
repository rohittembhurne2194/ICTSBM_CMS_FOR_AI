# Run  python main.py --server 202.65.157.253 --database LIVEBhadravatiGhantaGadi --ulbname Bhadravati --hostname localhost --filename DumpYardForecast

## Bhadravati Data ##
import os
import pandas as pd
import matplotlib.pyplot as plt
import argparse

#scripts
from pre import *
from lstm import *
from forecast import *

#Input
num_pred = 7
epochs = 3000
lookback = 7

# construct the argument parser and parse the arguments
ap = argparse.ArgumentParser()
ap.add_argument("-ip", "--server", required=True, help="Server IP address")
ap.add_argument("-db", "--database", required=True, help="Database name")
ap.add_argument("-ulbname", "--ulbname", required=True,help="name of the ULB")
ap.add_argument("-hostname", "--hostname", required=True,help="name of the ULB")
ap.add_argument("-filename", "--filename", required=True,help="name of the File")
args = vars(ap.parse_args())

#HostName
hostname = args["hostname"]
# Directory 
directory = args["ulbname"]
# Filename 
filename = args["filename"]
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
    print(error)
    
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
# plt.figure(figsize=(10,6))
# plt.plot(date_test[-15:], weight_test[-15:], color='blue', label='Actual')
# lg= "Forecast " + str(forecast_dates[0].date()) + " To " + str(forecast_dates.iloc[-1].date())
# plt.plot(forecast_dates,forecast, color='red', label=lg)
# plt.xlabel("Date")
# plt.ylabel("Total Weight(Tons)")
# plt.title("Garbage Generation Forecast of "+ str(num_pred) + " Days")
# # plt.xticks(rotation=45)
# plt.legend()
# plt.grid()
# plt.savefig('forecast.png')
# plt.show()

import plotly.graph_objects as go
#import plotly.express as px
weight_test = weight_test.reshape((-1))
trace1 = go.Scatter(
    x = date_test[-15:],
    y = weight_test[-15:],
    mode = 'lines',
    name = 'Actual'
)
trace2 = go.Scatter(
    x = forecast_dates,
    y = forecast,
    mode = 'lines',
    name = 'Forecast'
)
layout = go.Layout(
    title = "Garbage Generation Forecast of "+ str(num_pred) + " Days",
    xaxis = {'title' : "Date"},
    yaxis = {'title' : "Total Weight(Tons)"}
)
config = {
  'toImageButtonOptions': {
    'format': 'png', # one of png, svg, jpeg, webp
    'filename': filename
    # 'height': 500,
    # 'width': 700,
    # 'scale': 1 # Multiply title/legend/axis/canvas sizes by this factor
  }
}
fig = go.Figure(data=[trace1,trace2], layout=layout)
#fig.show(config=config)
fig.write_html(path+"/DumpYardforecast.html",config=config)
