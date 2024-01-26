# ffmpeg -i badapple.mp4 -vf fps=1 out/%d.png
#  ffmpeg -i Output/attempt1.mp4 -filter:v "setpts=PTS/10,fps=24" -an Output/attempt1sped.mp4

import math
import os
from PIL import Image
import numpy as np

# change this
# this isn't going to be the same accross installations
package_name = "Microsoft.SDKSamples.Appointments.CS_8wekyb3d8bbwe"
local_app_data_path = os.path.expanduser(f"~\\AppData\\Local\\Packages\\{package_name}\\LocalState")
file_path = os.path.join(local_app_data_path, "out.csv")


def analyze_block(image, starty, startx, leny, lenx):
    total = 0
    totalPixels = 0
    for i in range(starty, starty + leny):
        for j in range(startx, startx + lenx):
            totalPixels += 1
            if (image[i][j][0] > 127.5):
                total += 1
    return 1 if (total / totalPixels > .5) else 0


def divide_image(ydivs, xdivs, image, out=[]):
    blockwidth = math.ceil(len(image)/ydivs)
    blockheight = math.ceil(len(image[0])/xdivs)

    for i in range(0, ydivs):
        out.append([])
        for j in range(0, xdivs):
            out[len(out) - 1].append(analyze_block(image, i*blockwidth, j *
                                                   blockheight, blockwidth, blockheight))
    return out


ydivs = 20
xdivs = 30
i = 0
out = []
while (True):
    i += 1
    path = './out/' + str(i) + '.png'
    try:
        image = Image.open(path)
        image = np.asarray(image)
    except:
        print("stopped at image " + str(i - 1))
        break;
    divide_image(ydivs, xdivs, image, out)
    if(i % 20 == 0):
        print(i)


os.makedirs(os.path.dirname(file_path), exist_ok=True)
npout = np.asarray(out, dtype=str)
np.savetxt(file_path, npout, delimiter=",", fmt='%s')