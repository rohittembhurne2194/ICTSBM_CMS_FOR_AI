# import matplotlib modules
import os
import sys
import pandas as pd
import matplotlib
matplotlib.use("PDF")
import matplotlib.pyplot as plt
import matplotlib.ticker as mticker
# imports for SQL data part
import pymssql

# import the necessary packages
import argparse
# construct the argument parse and parse the arguments
ap = argparse.ArgumentParser()
ap.add_argument("-ip", "--datasource", required=True,help="server IP")
ap.add_argument("-db", "--database", required=True,help="name of the Database")
ap.add_argument("-ulbname", "--ulbname", required=True,help="name of the ULB")
ap.add_argument("-hostname", "--hostname", required=True,help="name of the ULB")
ap.add_argument("-fromdate", "--fromdate", required=True,help="Starting Date")
ap.add_argument("-todate", "--todate", required=True,help="Ending Date")

args = vars(ap.parse_args())

#HostName
hostname = args["hostname"]
# Directory 
directory = args["ulbname"]

if hostname == "localhost":   

    # Parent Directory path 
    parent_dir = "D:\Rohit\ICTSBM_CMS_AI_TEST_NEW\SwachhBharatAbhiyan.CMS\Images\AI"

else:

    # Parent Directory path 
    parent_dir = "D:\AdvancePublish\ICTSBMCMS_AI\Images\AI"


# Path 
path = os.path.join(parent_dir, directory)

try: 
    os.mkdir(path) 
except OSError as error: 
    print(error)


# ip = args["datasource"]
# DB = args["database"]


# Establish the Python SQL Server Connection
# cnxn_str = ("Driver={SQL Server};"
            # "Server=ip;"
            # "Database=DB;"
            # "UID=appynitty;"
            # "PWD=BigV$Telecom;"
            # "Trusted_Connection=no;")

    
# cnxn = pyodbc.connect(cnxn_str)

conn = pymssql.connect(server=args["datasource"], user='appynitty',
                       password='BigV$Telecom', database=args["database"])
                       
df = pd.read_sql_query("select (select userName from UserMaster um where um.userId=gc.userId) EmployeeName, count(gc.houseId) as house_count from GarbageCollectionDetails gc where cast(gc.gcDate as date) >= "+"'"+args["fromdate"]+"'"+" and cast(gc.gcDate as date) <= "+"'"+args["todate"]+"'"+" group by gc.userid;", conn)

# define df data frame
df = df.groupby("EmployeeName").sum()
#print(df)

# plt.hist(df["house_count"], color="blue")
# plt.show()
# create bar chart object
pt = df.plot.barh(color="red", alpha=.8, width=.4)

# configure title, legend, and grid
pt.set_title(label="House Collection By Employee", y=1.04, 
  family="fantasy", fontsize=14, weight=500, color="navy")
pt.legend().set_visible(False)
pt.grid(color="slategray", alpha=.5, linestyle="dotted", linewidth=.5)

# format Y axis
pt.invert_yaxis()
pt.set_ylabel("Employee Name", fontsize=12, color="navy")
pt.set_yticklabels(labels=df.index, fontsize=9, color="navy")
 
# format X axis
pt.set_xlabel("Total House Collection", labelpad=10, fontsize=12, color="navy")
pt.set_xticklabels(labels=df["house_count"], fontsize=9, color="navy")
pt.get_xaxis().set_major_formatter(matplotlib.ticker.FuncFormatter(lambda x, p: format(int(x), ",")))
  
# save bar chart to PDF file
#plt.savefig("D:\Rohit\ICTSBM_CMS_AI_TEST_NEW\SwachhBharatAbhiyan.CMS\Images\AI\Khapa_New\Emp_WiseCollection.png",bbox_inches="tight", pad_inches=.5)
plt.savefig(path+"\Emp_WiseCollection.png",bbox_inches="tight", pad_inches=.5)

#python EmpWise_Collection.py -ip 202.65.157.253 -db LIVEAdvanceAarmoriGhantaGadi -ulbname AarmoriNagarParishad -hostname localhost -fromdate 2022-01-01 -todate 2022-09-30