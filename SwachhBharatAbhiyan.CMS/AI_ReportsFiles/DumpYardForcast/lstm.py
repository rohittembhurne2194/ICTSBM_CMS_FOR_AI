import pandas as pd
import numpy as np
import keras as k
from keras.preprocessing.sequence import TimeseriesGenerator
import tensorflow as tf


def model(df, num_epochs = 30,look_back = 7, vb = 1):
    ## Preprocessing
    df.drop(columns=['House_Count'], inplace=True)

    weight_data = df['Total_Weight'].values
    weight_data = weight_data.reshape((-1, 1))

    split_percent = 0.80
    split = int(split_percent * len(weight_data))

    weight_train = weight_data[:split]
    weight_test = weight_data[split:]

    date_train = df['gcDate'][:split]
    date_test = df['gcDate'][split:]


    train_generator = TimeseriesGenerator(weight_train, weight_train, length=look_back, batch_size=20)
    test_generator = TimeseriesGenerator(weight_test, weight_test, length=look_back, batch_size=1)

    model = k.models.Sequential()
    model.add(
        k.layers.LSTM(30,
             activation='relu',
             input_shape=(look_back, 1))
    )
    model.add(k.layers.Dense(1))
    model.compile(optimizer='adam', loss='mse')

    model.fit_generator(train_generator, epochs=num_epochs, verbose=vb)

    return model, weight_test, date_test

