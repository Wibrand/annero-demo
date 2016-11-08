
@SET DeploymentZipFileStoreInSystemSimulator=%~dp0\deploymentfiles\webjobsdeploy.zip
@SET DeploymentZipFileStoreInSystemWebApp=%~dp0\deploymentfiles\webappdeploy.zip

del %DeploymentZipFileStoreInSystemSimulator%
del %DeploymentZipFileStoreInSystemWebApp%

msbuild ../simulators/SimulatorWebJobHost/SimulatorWebJobHost/SimulatorWebJobHost.csproj /t:Package /p:PackageLocation=%DeploymentZipFileStoreInSystemSimulator%;Configuration=Debug
msbuild ../web/show-floormap/web-show-floormap/web-show-floormap.csproj /t:Package /p:PackageLocation=%DeploymentZipFileStoreInSystemWebApp%;Configuration=azure-deploy

:END