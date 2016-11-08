# Deployment Description
This document is a description of the deployment process of the solution.

The main program in the deploment is **deploy.js** that is located in the 
**deploment** directory.

The steps in the deployment are:

1.  Creates a Resource Group
2.  Creates a Storage Account
3.  Upload files that are needed for the deployment

    * **webappdeploy.zip** - Contains the Realtime web site and the two web jobs: Create Snapshot and Admin Worker.
    * **database.bacpac** - The SQL Database.
    * **webjobsdeploy.zip** - The simulator of Yanzi Gateway.
    * **simulatordevices.json** - The description of the simulated devices.
4.  Run an Azure Resource Management (ARM) Template, **azuredeploy.json** to create the solution.

## ARM Template
This template describes the solution in a json format. It includes all part of the solution, 
except the PowerBI reports.

