import pandas as pd
import numpy as np

def predict(df, num_prediction, model, look_back):
    weight_data = df['Total_Weight'].values
    weight_data = weight_data.reshape((-1, 1))
    weight_data = weight_data.reshape((-1))
    prediction_list = weight_data[-look_back:]

    for _ in range(num_prediction):
        x = prediction_list[-look_back:]
        x = x.reshape((1, look_back, 1))
        out = model.predict(x)[0][0]
        prediction_list = np.append(prediction_list, out)
    prediction_list = prediction_list[look_back - 1:]

    return prediction_list


def predict_dates(df, num_prediction):
    last_date = df['gcDate'].values[-1]
    prediction_dates = pd.date_range(last_date, periods=num_prediction + 1).tolist()
    return pd.Series(prediction_dates)