'''
    Mateusz Zajac (298654)
    Advanced Python
    List 10
    Ex 1
'''

import datetime
import requests

import pandas as pd
import matplotlib.pyplot as plt

'''
    Small scripts for retrieving and cleaning up coronavirus data
'''

# Download infections data and write them to file
r = requests.get("https://arcgis.com/sharing/rest/content/items/b03b454aed9b4154ba50df4ba9e1143b/data?", allow_redirects=True)
with open("infections_first.csv", mode="wb") as file:
    file.write(r.content)

with open("infections.csv", encoding="utf-8", mode="w+") as resultFile:
    # coronavirus data (03.03.2020 to 23.11.2020)
    with open("infections_first.csv", mode="r") as file:
        # Read all lines except first one (header)
        allLines = file.readlines()[1:]
        # For every other line
        for line in allLines:
            # Split data by ";" delimiter
            lineSeparated = line.split(";")
            # Write to result file
            resultFile.write(lineSeparated[1]+";"+lineSeparated[2].replace(" ", "")+"\n")

# Read infections and meteorogical data from csv files
infectionsData = pd.read_csv("infections.csv", delimiter=";", header=None, names=["RawDate", "Number of new cases"])
temperatureData = pd.read_csv("avg_temps.txt", delimiter="\t", header=None, names=["RawDate","Degrees in Celcius"])

# Add new column which is parsed data in datetime object
infectionsData['Date'] = infectionsData["RawDate"].apply(lambda x: datetime.datetime.strptime(x, "%d.%m.%Y").date())
temperatureData['Date'] = temperatureData["RawDate"].apply(lambda x: datetime.datetime.strptime(x, "%Y-%m-%d").date())

# Start and end dates for cold season
start_date_cold = datetime.datetime.strptime("2020-09-30", "%Y-%m-%d").date()
end_date_cold = datetime.datetime.strptime("2020-11-23", "%Y-%m-%d").date()

# Start and end dates for warm season
start_date_warm = datetime.datetime.strptime("2020-06-01", "%Y-%m-%d").date()
end_date_warm = datetime.datetime.strptime("2020-09-01", "%Y-%m-%d").date()

# Filter out cold season
infections_data_filter_cold = infectionsData.loc[(infectionsData["Date"] > start_date_cold) & (infectionsData["Date"] < end_date_cold)]
tempereture_data_filter_cold = temperatureData.loc[(temperatureData["Date"] > start_date_cold) & (temperatureData["Date"] < end_date_cold)]

# Filter out warm season
infections_data_filter_warm = infectionsData.loc[(infectionsData["Date"] > start_date_warm) & (infectionsData["Date"] < end_date_warm)]
tempereture_data_filter_warm = temperatureData.loc[(temperatureData["Date"] > start_date_warm) & (temperatureData["Date"] < end_date_warm)]

# Make a new figure
fig1 = plt.figure()
# Write proper title
fig1.suptitle("Cold season")
# Add 2 subplots vertically
ax1 = fig1.add_subplot(211)
ax2 = fig1.add_subplot(212)

# Make a new figure
fig2 = plt.figure()
# Write proper title
fig2.suptitle("Warm season")
# Add 2 subplots vertically
ax1_1 = fig2.add_subplot(211)
ax2_1 = fig2.add_subplot(212)

# Prepare plots for
infections_data_filter_cold.plot(x="Date", y="Number of new cases", ax=ax1)
tempereture_data_filter_cold.plot(x="Date", y="Degrees in Celcius", ax=ax2)

infections_data_filter_warm.plot(x="Date", y="Number of new cases", ax=ax1_1)
tempereture_data_filter_warm.plot(x="Date", y="Degrees in Celcius", ax=ax2_1)

plt.show()

'''
    Source of data:
    Coronavirus infections in 2020: https://www.gov.pl/web/koronawirus/wykaz-zarazen-koronawirusem-sars-cov-2
    Cleaned-up meteorogical data by Maciej Witkowski on MS Teams Post
'''

'''
    As we can see, in cold season there is significantly more infections than in warm season.
'''




