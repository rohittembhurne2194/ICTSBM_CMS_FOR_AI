# Importing packages
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
# imports for SQL data part
import pyodbc

# Establish the Python SQL Server Connection
cnxn_str = ("Driver={SQL Server};"
            "Server=124.153.94.110;"
            "Database=LIVEAdvanceJathGhantaGadi;"
            "UID=appynitty;"
            "PWD=BigV$Telecom;"
            "Trusted_Connection=no;")

    
cnxn = pyodbc.connect(cnxn_str)

# build up our query string
#query = ("select * from GarbageCollectionDetails",cnxn)

# execute the query and read to a dataframe in Python
#df = pd.read_sql(query, cnxn)

df = pd.read_sql_query('select distinct(userid), count(houseId) as house_count from GarbageCollectionDetails where AreaId is not null group by userid', cnxn)

#print(df)
#print(type(df))

del cnxn  # close the connection


# Generating the heat map
sns.heatmap(df.corr(), annot=True)
plt.savefig("D:\Rohit\ICTSBM_CMS_AI_TEST_NEW\SwachhBharatAbhiyan.CMS\Images\AI\Test3.png")
#plt.show()
