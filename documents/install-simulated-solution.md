# Install Simulated Solution

## Requirements
* Windows / Mac / Linux
* Node.js installed ([install it from here](https://nodejs.org/en/))
* An active subscription in Microsoft Azure

## Steps

### 1. Clone or download source code


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
    npm deploy

You will answer three questions.
1. A prefix. Is used in namespaces and url:s to make names globally unique. (Keep it short, 
max five characters.)
2. The name of the [Resource Group](https://azure.microsoft.com/en-us/documentation/articles/resource-group-overview/) where the solution will hosted in.
3. Region where it should be deployed.

TODO SUMMARY 

