## Data preprocessed for forecasting ##
## Bhadravati Data ##
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
import seaborn as sns
import pymssql


def df_server(server,database):
    ## Read data from server through query
    # Server details
    conn = pymssql.connect(server=server, user='appynitty', password='BigV$Telecom', database=database)
    # Query
    df = pd.read_sql_query('select distinct(cast(gcdate as Date)) gcDate ,COUNT(distinct(houseid)) as House_Count ,isnull(SUM(totalGcWeight),0) As Total_Weight from GarbageCollectionDetails group by cast(gcdate as Date) order by cast(gcdate as Date) asc;',conn)

    #Remove starting rows
    df = df.iloc[4:]
    return df

def preprocess(df):
    ## Data Preprocess
    # Set Date as index
    df.set_index("gcDate", inplace=True)

    # Prase the Date
    df.index = pd.to_datetime(df.index)

    # Add missing Dates with NaN
    df = df.asfreq('D')

    # Interpolating the House Count
    # df.set_index('gcDate', inplace=True)['House_Count'].interpolate(method="linear")
    df["House_Count"] = df["House_Count"].interpolate(method="polynomial", order=2)

    # Fill zeros with nan
    # As the the employee has scanned the QR but hasnt dumped the garbage
    df["Total_Weight"] = df["Total_Weight"].replace({'0':np.nan, 0:np.nan})

    # Fill NaN in Totoal Weight which has more that 11 Weight
    df.loc[df["Total_Weight"] > 11, "Total_Weight"] = np.nan

    ## This Data is for training only
    # Drop the NaN for preparing the data for training
    data = df.dropna()

    # Drop the rows which are more than 10
    data_10 = data[data["Total_Weight"] < 11]

    ### Random Forest Regression ###

    # Importing the dataset
    df_X = data_10.iloc[:, 0:1].values
    df_y = data_10.iloc[:, -1].values
    # print(df_X)
    # print(df_y)
    # Fitting Random Forest Regression to the dataset
    from sklearn.ensemble import RandomForestRegressor
    # from sklearn.linear_model import LinearRegression
    df_regressor = RandomForestRegressor(n_estimators = 20, random_state = 30)
    # bh_regressor = LinearRegression()
    df_regressor.fit(df_X, df_y)

    # Predicting a new result
    # df_y_pred = df_regressor.predict([[385]])
    # print(bh_y_pred)

    # Visualising the Random Forest Regression results (higher resolution)
    # X_grid = np.arange(min(df_X), max(df_X), 0.01)
    # X_grid = X_grid.reshape((len(X_grid), 1))
    # plt.figure(figsize=(15,6), dpi=80,linewidth=10)
    # plt.subplot(2,2,1)
    # plt.scatter(df_X, df_y, color = 'red')
    # plt.plot(X_grid, df_regressor.predict(X_grid), color = 'blue')
    # plt.title('Random Forest Regression')
    # plt.xlabel('Total House Count')
    # plt.ylabel('Total Weight')
    ### Random Forest Regression ####^

    # Append the predicted weight values
    d = []
    for i in df["House_Count"].iloc():
        # print(i)
        # print(df_regressor.predict([[i]])[0])
        d.append(df_regressor.predict([[i]])[0])

    df_series = pd.Series(d)
    df.reset_index(inplace=True)

    # Concatinate the predicated weight series with dataset
    df_pred = pd.concat([df, df_series.rename("Pred_Weight")], axis=1)

    # Fill the NaN with predicted value in Total Weight
    df["Total_Weight"].fillna(df_pred["Pred_Weight"], inplace=True)
    df = df.round({"House_Count":0,"Total_Weight":3})

    # Save dataframe to csv file
    # df.to_csv("Prepared_data_for_forcasting3.csv")
    return df
# Plots
# plt.subplot(2,2,2)
# plt.title("House Count Vs Total Weight")
# sns.lineplot(x="House_Count", y="Total_Weight", data=df)
#
#
#
# plt.subplot(2,2,4)
# plt.title("Histogram of Total Weight")
# sns.histplot(x="Total_Weight", data=df, bins=10)
# plt.show()