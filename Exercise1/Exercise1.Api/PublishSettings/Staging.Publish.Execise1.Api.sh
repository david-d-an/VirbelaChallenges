#!/bin/bash

# This script must be called from Execise1.Api folder because of relative paths in the script

# Stop server
echo Stopping server..........
echo
launchctl unload ~/Library/LaunchAgents/dotnet.run.Execise1.Api.plist

# Create publish version
echo Building release version..........
echo
cd ~/Development/DotNet/VirbelaChallenges/Execise1/Execise1.Api
dotnet publish -c Release

# Copy to Nginx target folder
echo Pushing code to app folder..........
echo
cp -r ~/Development/Dotnet/Execise1/VirbelaChallenges/Execise1/Execise1.Api/bin/Release/netcoreapp3.1/publish/* \
/usr/local/var/www/Execise1.Api

# Start server
echo Starting server..........
echo
launchctl load ~/Library/LaunchAgents/dotnet.run.Execise1.Api.plist
