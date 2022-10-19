# static_plot.py

# import necessary packages
import matplotlib.pyplot as plt

# animated_line_plot.py

from random import randint

import matplotlib.pyplot as plt
from matplotlib.animation import FuncAnimation

# data
#data_lst = [60, 59, 49, 51, 49, 52, 53]

# create empty lists for the x and y data
x = []
y = []

# create the figure and axes objects
#fig, ax = plt.subplots()
fig = plt.figure()
plt.clf()
plt.xlabel('Day Number')
plt.ylabel('Temperature (*F)')
plt.title('Temperature in Portland, OR over 7 days')
# function that draws each frame of the animation
def animate(i):
    pt = randint(1,9) # grab a random integer to be the next y-value in the animation
    x.append(i)
    y.append(pt)

    
    plt.plot(x, y)
    plt.xlim([0,20])
    plt.ylim([0,10])

# plot the data and customize
#ax.plot(data_lst)


# run the animation
ani = FuncAnimation(fig, animate, frames=20, interval=500, repeat=False)



# save and show the plot
ani.save('static_plot.gif')
#plt.show()