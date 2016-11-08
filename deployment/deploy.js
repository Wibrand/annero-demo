/*
    https://www.npmjs.com/package/shelljs
    https://www.npmjs.com/package/readline-sync
    http://chancejs.com/
    https://www.npmjs.com/package/password-generator

*/

var DEBUG = false;

const shell = require('shelljs');
const readlineSync = require('readline-sync');
const generatePassword = require('password-generator');
const fs = require('fs');

var locations = ['North Europe', 'West Europe', 'East Asia', 'East US',
    'West US'];
var deploymentName = 'dep-' + Date.now();

// inputs
var globalPrefix = readlineSync.question('(It must start with a letter, be max five characters long and contain only lower-case letters)\nGlobal prefix for the solution: ', {
    limit: /^[a-z][a-z0-9]{2,4}$/,
    limitmessage: 'wrong input'
});

var resoureGroupName = readlineSync.question('Resource group name: ');
var resourceGroupLocationIndex = readlineSync.keyInSelect(locations, 'Resource Group location:');
if(resourceGroupLocationIndex == -1) return;

var resoureGroupLocation = locations[resourceGroupLocationIndex];

var storageName = globalPrefix + '2str' + '4' + resoureGroupName.replace('-', '');
var storageAccountPrimaryKey;
var storageAccountBlobEndpoint;
var storageContainerDeploymentName = "deployment";
var storageContainerDataName = "data";
var storageContainerEventHubArchiveName = "eventhub-archive";
var storageContainerAsaRefData = "asa-refdata";
var deploymentFilesLocalDirectory = "deploymentfiles";
var sqlBacPacFileName = "database.bacpac";
var webJobDeploymentName = "webjobsdeploy.zip";
var webAppDeploymentName = "webappdeploy.zip";
var simulatedDataName = "simulatordevices.json";
var asaTransformQueryName = "asa-transform-query.txt";
var deploymentFiles = [sqlBacPacFileName, webJobDeploymentName, simulatedDataName, webAppDeploymentName];

var armTemplateDirectory = "json";
var armTemplateName = "azuredeploy.json";

var sqlAdminName;
var sqlAdminPassword = generatePassword(3, false, /[A-Z]/) + + generatePassword(1, false, /[.,-]/) + generatePassword(7, false, /[\w\d\?\-]/) + generatePassword(5, false, /[\d]/);
var sqlServerName;
var sqlDatabaseName;
var yanziSasRule;
var debugAsaListenKey;

var stepCounter = 1;

// deployment flow
if (!checkIfResourceGroupAlreadyExist()) return;
if (!createResourceGroup()) return;
if (!createStorageAccount()) return;
if (!uploadDeploymentFiles()) return;
var resultOfArmDeployment = startArmDeployment()

printSummary(resultOfArmDeployment);

function checkIfResourceGroupAlreadyExist() {
    printStep('Check if Resource Group already exist');

    // check if group already exist, we will not deploy to an existing group
    var checkIfResourceGroupExistCommand = `azure group show ${resoureGroupName}`;
    var returnValue = runCommand(checkIfResourceGroupExistCommand, false);

    if (returnValue) {
        console.log('Error: Resource group already exist');
        return false;
    }
    else {
        return true;
    }
    
}
function createResourceGroup() {
    printStep('Create Resource Group');

    var createResurceGroupCommand = `azure group create --json ${resoureGroupName} "${resoureGroupLocation}"`;
    var returnValue = runCommand(createResurceGroupCommand);

    return true;
}

function createStorageAccount() {
    try {
        printStep('Create Storage Account');

        var createStorageAccountCommand = `azure storage account create -g ${resoureGroupName} --sku-name LRS -l "${resoureGroupLocation}" --kind storage ${storageName}`;
        var returnValue = runCommand(createStorageAccountCommand);

        printStep('Get keys from Storage Account');
        var getStorageAccountKeysCommand = `azure storage account keys list ${storageName} -g ${resoureGroupName} --json`;
        var returnValueJson = JSON.parse(runCommand(getStorageAccountKeysCommand));
        storageAccountPrimaryKey = returnValueJson[0].value;

        printStep('Create Storage Container for deployment files');
        var createStorageContainerPublicCommand = `azure storage container create --container ${storageContainerDeploymentName} -a ${storageName} -k ${storageAccountPrimaryKey} -p blob --json`;
        var returnValue = JSON.parse(runCommand(createStorageContainerPublicCommand));

        printStep('Create Storage Container for data');
        var createStorageContainerPrivateCommand = `azure storage container create --container ${storageContainerDataName} -a ${storageName} -k ${storageAccountPrimaryKey} --json`;
        var returnValue = JSON.parse(runCommand(createStorageContainerPrivateCommand));

        printStep('Create Storage Container for eventhub archive');
        var createStorageContainerPrivateCommand = `azure storage container create --container ${storageContainerEventHubArchiveName} -a ${storageName} -k ${storageAccountPrimaryKey} --json`;
        var returnValue = JSON.parse(runCommand(createStorageContainerPrivateCommand));

        printStep('Create Storage Container for stream analytics reference data');
        var createStorageContainerPrivateCommand = `azure storage container create --container ${storageContainerAsaRefData} -a ${storageName} -k ${storageAccountPrimaryKey} --json`;
        var returnValue = JSON.parse(runCommand(createStorageContainerPrivateCommand));

        return true;
    }
    catch (ex) {
        return false;
    }
}
 
function uploadDeploymentFiles() {
    try {
            printStep('Uploading files to deployment container');

            deploymentFiles.forEach(function(fileName) {
                var uploadFileToBlobStorageCommand =  `azure storage blob upload -f ${deploymentFilesLocalDirectory + '/' + fileName} --container ${storageContainerDeploymentName} -a ${storageName} -k ${storageAccountPrimaryKey}`;
                returnValue = runCommand(uploadFileToBlobStorageCommand);
           }, this);

           return true;
    }
    catch (ex) {
        return false;
    }
}

function startArmDeployment() {
    printStep('Start Azure Resource Manager Deployment (takes between 5 to 10 minutes)');

    var parameters = {
        "globalPrefix": {"value": globalPrefix},
        "eventHubNamespaceName": {"value": globalPrefix + "-" + resoureGroupName},
        "eventHubName": {"value": "annero-eh"},
        "webAppName": {"value": globalPrefix + "-floormap"},
        "storageAccountName": {"value": storageName},
        "storageKey": {"value": storageAccountPrimaryKey},
        "storageUriForDeployment": {"value": "https://" 
                        + storageName + 
                        ".blob.core.windows.net/" + 
                        storageContainerDeploymentName},
        "sqlBacPacFileName": {"value": sqlBacPacFileName},
        "sqlAdministratorLoginPassword": {"value": sqlAdminPassword},
        "webJobDeploymentFileName": {"value": webJobDeploymentName},
        "webAppDeploymentFileName": {"value": webAppDeploymentName},
        "simDataFileName": {"value": simulatedDataName}
        };

    var startArmDeploymentCommand = `azure group deployment create -g ${resoureGroupName} ` +
        `-f ${armTemplateDirectory + '/' + armTemplateName } ` +
        `-p "${addslashes(JSON.stringify(parameters))}" ` +
        `--json -n ${deploymentName}`;

    try
    {
    var returnValueJson = JSON.parse(runCommand(startArmDeploymentCommand));

    if(returnValueJson.properties.provisioningState == "Succeeded") {
        sqlAdminName = returnValueJson.properties.outputs.sqlAdmin.value;
        sqlAdminPassword = returnValueJson.properties.outputs.sqlAdminPassword.value;
        sqlServerName = returnValueJson.properties.outputs.sqlServerName.value;
        sqlDatabaseName = returnValueJson.properties.outputs.sqlDatabaseName.value;
        yanziSasRule = returnValueJson.properties.outputs.yanziSasRule.value;
        return true;
    }
    else {
        return false;
    }
    }
    catch (e) {
        return false;
    }
}

function getDeploymentInformation() {
    var getDeploymentOperationInformationCommand = `azure group deployment operation list ${resoureGroupName} ${deploymentName} --json`;
    return runCommand(getDeploymentOperationInformationCommand);
}

function printSummary(result) {

    if (result) {
        console.log(`
                     ** DEPLOYMENT SUCCEDED **
============================ SUMMARY =================================
== SQL Server
==         Server name: ${sqlServerName}
==       Database name: ${sqlDatabaseName}
==    Admin Login name: ${sqlAdminName}
==      Admin Password: ${sqlAdminPassword}
== Web Application Url: http://${globalPrefix + "-floormap"}.azurewebsites.net
==
== Yanzi Send Connection string: ${yanziSasRule}
==
======================================================================

`);
    }
    else {
        console.log('============================ Deployment failed! ============================');
        var returnValueJson = JSON.parse(getDeploymentInformation());
        returnValueJson.forEach(function(element) {
            if(element.properties.provisioningState == 'Failed') {
                console.log(element);
            }
        }, this);

        console.log('============================ Deployment failed! ============================');
    }
}
    

function runCommand(commandLine, showResult) {
    showResult = showResult == undefined ? true : showResult;
    
    try {
        console.log('\t' + commandLine);
        if(DEBUG) {
            var r = readlineSync.keyIn('Continue: [y/n] ', {limit: '<yn>'});
            if(r != 'y') {
                throw "end";
            }
        }

        var r = shell.exec(commandLine, { silent: true });
        if (r.stderr) {
            // console.log(`ERROR: ${r.stderr}` )
            return showResult ? r.stderr : false;
        }
        else {
            return showResult ? r.stdout : true;
        }
    }
    catch (ex) {
        return null;
    }
}

function printStep(stepDescription) {
    console.log(`Step: ${stepCounter++} ==== ${stepDescription}`);
}

 function addslashes(str) {
    return (str + '').replace(/[\\"]/g, '\\$&').replace(/\u0000/g, '\\0');
 }