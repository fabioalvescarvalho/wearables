# The Personal Display: hardware
Swedish Team wearables' project, for the 2015 NASA Space Apps Hackathon

___Executive Summary___
The functional hardware prototype (MVP) of the Personal Display is built using the Gadgeteer prototyping toolkit and encompasses a display as well as tactile and touchless user input hooked up to a microcontroller.

___Introduction___
Digitised manuals are hard work to navigate, not least in zero gravity onboard the International Space Station. The Swedish NASA Space Apps winners countered this issue by designing The Personal Display. This GitHub page is the file depository for the functional hardware prototype, which encompasses a display as well as tactile and touchless user input hooked up to a microcontroller.

___Methodology___
The Gadgeteer prototyping toolkit was used to build the hardware MVP. The mainboard was encoded in VB using Visual Studio (VS) Express 2012.

_Hardware_
The circuit contains a display (N18 v1.1), two buttons (v1.2 and v1.3), a light sensor (LightSense v1.1), the FEZ Spider 1.0 mainboard and a USB Client (v1.2). All hardware are produced by GHI Electronics.

_Software_
Visual Studio Express 2012 was used to generate a Gadgeteer application using a VB template. Excerpts from the NASA manual "Crew Escape Systems 21002, Section 2.0: CREW-WORN EQUIPMENT" were hardcoded into string arrays for easy access and display.

___Results___
The hardware circuit was modelled in VS Express'12. The user may use either tactile (button) or high-intensity light input to guide him/herself through the manual. By default, the first manual page is displayed and auto-scrolling is off. The two buttons have auto-scroll and reset functionality, respectively. Auto-scroll makes new pages appear automatically every 5s, while Reset reverts display content to the first page of the manual and stops scrolling. The light level is sensed every 500 ms, and if beyond the laser threshold of 900 lux, the display updates. Display updating stops when laser input is no longer detected.

Images of the the complete circuit diagram and the working prototype, mounted on a latex glove
https://drive.google.com/folderview?id=0Bw59TasEg3SJfnFLcm9YcHJ3WjNfUERxTUd3TUxlYkxnZ0xCRGpEQVdPT3ZmcS1Gdm14T2s&usp=sharing

N.B. In-line comments are available throughout the code. The full VS'12 project encompasses more files than are listed in this directory; default and auto-generated files are omitted.

___Discussion___

___Conclusions___

___References___
Manual text used for the hardware prototype:
Crew Escape Systems 21002, Section 2.0: CREW-WORN EQUIPMENT
http://www.nasa.gov/centers/johnson/pdf/383443main_crew_escape_workbook.pdf

Overview of the .NET Gadgeteer toolkit: http://en.wikipedia.org/wiki/.NET_Gadgeteer
Gadgeteer homepage: http://www.netmf.com/gadgeteer/

___Find out more___
For further details of the project, see our project website
https://2015.spaceappschallenge.org/project/the-personal-display/
