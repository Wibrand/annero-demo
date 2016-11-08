# Install Simulated Solution
> **Be aware** that it will be a cost in Microsoft Azure to run this solution.

This deployment script will create the following artifacts:
- Event Hub
- Stream Analytics Job
- Service Bus with two Topics
- Sql Database
- Storage account
- Web App for Realtime Floormap visualization 
- Admin Web Job
- Sql Snapshot Web Job
- Web Job that host the Yanzi Networks Simulator

It will not install the PowerBI reports as this is not currently possible to do through scripting.

## Requirements
* Windows / Mac / Linux
* Node.js installed ([install it from here](https://nodejs.org/en/))
* An [active subscription](https://azure.microsoft.com/en-us/free/) in [Microsoft Azure](http://www.azure.com)

## Steps

### 1. Clone or download source code
You find the solution in the following [GitHub repository](http://aka.ms/annero).

### 2. Install Acure CLI
    npm install -g azure-cli

### 3. Choose account and subscription to install the solution into
3.1 First you need to log in to Azure. 

    azure login

3.2 Check if you have several subscriptions on this account.

    azure account list

3.3 Look in the column **current** to check if the right subscription is set.

If not, then use `azure account set` to change the subscription.

### 4. Start the installation of the solution
4.1 Go to the **deployment** directory.

4.2 Type
    npm install
    node deploy

You will answer three questions.

1. A prefix. Is used in namespaces and url:s to make names globally unique. (Keep it short, 
max five characters.)
2. The name of the [Resource Group](https://azure.microsoft.com/en-us/documentation/articles/resource-group-overview/) where the solution will hosted in.
3. Region where it should be deployed.

### 5. Run the solution ###
                         ** DEPLOYMENT SUCCEDED **
    ============================ SUMMARY =================================
    == SQL Server
    ==         Server name: k12-sqlser-k6vwbkvm77z64
    ==       Database name: anneroDb
    ==    Admin Login name: k12-admin
    ==      Admin Password: WZLNaN?3fpXco43974
    == Web Application Url: http://k12-floormap.azurewebsites.net
    ==
    == Yanzi Send Connection string: Endpoint=sb://k12-fm112.servicebus.windows.net/;SharedAccessKeyName=yanziSendRule;SharedAccessKey=Ph4/CyJU63zSYGB5tmimytWL19rzUXmzBSgrLX85bwQ=;EntityPath=annero-eh
    ==
    ======================================================================
Open the web app and see the simulation. Open also the [Azure Portal](http://portal.azure.com) and open the 
Resource Group that you just created and explore all the services that is hosted inside it.

