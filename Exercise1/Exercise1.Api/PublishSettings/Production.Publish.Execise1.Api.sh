#!/bin/bash

# Deploy target Azure App Service Web App: 
# This script must be called from Execise1.Api folder because of relative paths in the script

appName="Execise1Api6921"
resourceGroup="Execise1Linux"

# Create publish version
echo Building release version..........
echo
cd ~/Development/DotNet/VirbelaChallenges/Execise1/Execise1.Api
dotnet publish -c Release

# Create deploy package in Zip format
echo Creating deploy.zip..........
echo
cd ./bin/release/netcoreapp3.1/publish
zip -r deploy.zip *

# Copy to Azure App Service
echo Pushing code to Azure web app..........
echo
# az webapp deploy --src-path ./deploy.zip --name Execise1Api6921 -g Execise1Linux --type zip
az webapp deploy --src-path ./deploy.zip --name $appName -g $resourceGroup --type zip

# Removing deploy.zip
rm ./deploy.zip
