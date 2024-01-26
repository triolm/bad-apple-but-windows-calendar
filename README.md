# Bad Apple but it's windows calendar events
A lot of the code here is from the [Windows Universal Platform calendar appointment sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/Appointments). I have no idea what I'm doing but UWP has great docs, so it was surprisingly not that hard.

## Installation
To run `./Frames/generateblocks.py`, the python code that converts the video frames to an array of pixels, you need to convert the video to image files (I used ffmpeg) and put them in the `./Frames/out` directory. You also need to change the package_name variable to the UWP app's package name in appdata.

To run the UWP app, open `./Samples/Appointments/cs/Appointments.sln` in Visual Studio. In the UI it opens, run the "Add Appointment" scenario.
