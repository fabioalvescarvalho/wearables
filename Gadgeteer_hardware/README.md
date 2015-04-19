# The Personal Display: hardware prototype
By A Caroline E Dahl, Stockholm April 2015, for the 2015 NASA Space Apps Hackathon

#### Executive Summary
The functional hardware prototype (MVP) of the Personal Display is built using the Gadgeteer prototyping toolkit and encompasses a display as well as tactile and touchless user input hooked up to a microcontroller. The hardware prototype is fully operational and was demo'ed during the Swedish NASA Space Apps Challenge finals. The hardware prototype was made to be compatible with both Speech-to-text and eyetracking soft- and hardware, and efforts to merge these technologies are ongoing.

#### Introduction
Digitised manuals are hard work to navigate, not least in zero gravity onboard the International Space Station. The Swedish NASA Space Apps winners countered this issue by designing The Personal Display. This GitHub page is the file depository for the functional hardware prototype, which encompasses a display as well as tactile and touchless user input hooked up to a microcontroller.

#### Methodology
The Gadgeteer prototyping toolkit was used to build the hardware MVP.

__Hardware__
The circuit contains a display (N18 v1.1), two buttons (v1.2 and v1.3), a light sensor (LightSense v1.1), the FEZ Spider 1.0 mainboard and a USB Client (DP v1.2). All hardware are produced by GHI Electronics.

__Software__
Visual Studio Express 2012 was used to generate a Gadgeteer application using a VB template. Excerpts from the NASA manual "Crew Escape Systems 21002, Section 2.0: CREW-WORN EQUIPMENT" were hardcoded into string arrays for easy access and display.

#### Results
The hardware circuit was modelled in VS Express'12. The user may use either tactile (button) or high-intensity light input (laser) to guide him/herself through the manual. By default, the first manual page is displayed and auto-scrolling is off. The two buttons have auto-scroll and reset functionality, respectively. Auto-scroll makes new pages appear automatically every 5s, while Reset reverts display content to the first page of the manual and stops auto-scrolling. The light level is sensed every 500 ms, and if beyond the laser threshold of 900 lux, the display updates. Display updating stops when laser input is no longer detected, in which case the display reverts to its prior button-state, whether Reset or Auto-scrolling.

The microcontroller was encoded using the above instructions. All components were sewn into a latex glove and this prototype was fully operational during the live demo at NASA Space Apps Challenge. Images of the the complete circuit diagram and the working prototype are available here:

https://drive.google.com/folderview?id=0Bw59TasEg3SJfnFLcm9YcHJ3WjNfUERxTUd3TUxlYkxnZ0xCRGpEQVdPT3ZmcS1Gdm14T2s&usp=sharing

N.B. In-line comments are available throughout the code. The full VS'12 project encompasses more files than are listed in this directory; default and auto-generated files are omitted.

#### Discussion and Conclusions
A fully functional prototype of the Personal Display was developed during the 24h of the NASA Space Apps Challenge in Stockholm, Sweden. Components were chosen to give the user choice in terms of inputs to operate manual content, and all components were successfully calibrated and functionalized. Efforts to merge Tobii eye-tracking hardware and speech-to-text code with the Gadgeteer hardware prototype are ongoing. We are optimistic about this merge, as all hardware work on the same platform, and both Text-to-Speech and Gadgeteer code operate in the .NET universe.

#### References
Manual text used for the hardware prototype:
Crew Escape Systems 21002, Section 2.0: CREW-WORN EQUIPMENT
http://www.nasa.gov/centers/johnson/pdf/383443main_crew_escape_workbook.pdf

Overview of the .NET Gadgeteer toolkit: http://en.wikipedia.org/wiki/.NET_Gadgeteer

Gadgeteer homepage: http://www.netmf.com/gadgeteer/

#### Find out more
For further details of the project, see our project website

https://2015.spaceappschallenge.org/project/the-personal-display/
