# Modify Deployment Script
It is no problem to modify the deploy.js and azuredeploy.json on any operating system that 
supports Node.js. But if you want to change or update the Web App or Web Jobs you need to do
this in a Windows machine, that has Visual Studio installed.

Clone the repository and you will find the different projects for the Web App and Web Jobs.

* Web App for the Realtime Floormap viewer - web/show-Floormap
* Web Job for Create Snapshot - webjob/webjob-snapshot-sql
* Web Job for Admin Worker - webjob/webjob-admin-Worker
* Web Job for Simulator of Yanzi Gateway - webjob/webjob-simulate-yanzi-devices

## Create the deployment packages webappdeploy.zip and webjobsdeploy.zip
The deployment direcory contains a bat file, `build-packages.cmd` that will create
these two deployment packages with the help of the command `MSDeploy`.

> You need to have msdeploy in your PATH when you run the command `build-packages.cmd`.
The easiest way to get this command in your PATH is to open a *Visual Studio Develpment 
Command Prompt*.
