# Welcome
Welcome to the project "Annero" which is a collaboration between 
[Microsoft Sweden](http://www.microsoft.se), [Yanzi Networks](https://yanzi.se/), Coor and Intel.

> This is an example and should be treated as such.

This project has three objectives.

1.  To implement an IoT solution for Facility Management at Microsoft's offices in Sweden.
2.  Take lessons from it and extract a general template for creating this solution in other situations and places.
3.  Create a simulated environment to test the solution without having to install any sensors from Yanzi.

## 1. The solution for Facility Management at Microsoft Office in Sweden
The company Coor is responsible for Facility Management at Microsoft office in Sweden. 
The want to have better insigth in the use of the office. Microsoft has an open space solution, which imply that no one has their own office space. 

Yanzi installed 140 sensors on floor one in the building. Is was a combination of sensors for Temperature, Motion, CO2 and Humidity. 
Motion sensors was used to register if an area, such as Conference Room, Desk or Toilet was occuiped or not.

This information is collected in Yanzis cloud servers and send by a gateway 
into an Event Hub in Azure. (An IoT Hub could also have been used.)

A Stream Analytics job reads from the Event Hub and process the information, mainly by transforming it to different sources. 
It sends it to Service Bus Topics for real time information and to a SQL Database for business analysis. 

A Web Application hosted in Azure as a Web App is listening to the events from the Service Bus Topic queue and 
sends the events to a page with SignalR. That page contains a SVG map of the office and updates the display of 
the map from the information in the events.

Optional: An UWP app installed on a Raspberry PI is available to easy display the Web Application on a large screen.

A PowerBI dashboard is also created to host visualizations of the data. It uses the SQL Database as it source.

[A deeper technical description of the solution](documents/architecture-description-annero.md)

## 2. Template to deploy the simulated solution
You need only to run a script to deploy the solution. It includes everything, except the PowerBI reports. It also includes a
simulated yanzi gateway that sends messages from about 140 sensors into the system.

[How to install the simulated solution](documents/install-simulated-solution.md)

[How to setup PowerBI](documents/setup-powerbi.md)

## Contributors
* Marie Lassborn, Yanzi
* Oriol Piñol Piñol, Yanzi
* Dag König, Microsoft
* Anders Thun, Microsoft
* Peter Bryntesson, Microsoft

