# 3.1-buster is a Debian
# 3.1-buster-slim image has dotnet 3.1 runtime
# 3.1-buster image has dotnet 3.1 SDK
# mcr.microsoft.com/dotnet/aspnet:3.1 is 3.1-buster-slim

# Build a container image with dotnet runtime to be deployed on Azure
FROM mcr.microsoft.com/dotnet/aspnet:3.1-buster-slim AS base

ENV DOTNET_ENVIRONMENT=Docker
ENV ASPNETCORE_ENVIRONMENT=Docker

# Install SSH server
RUN apt-get update -y \
    && apt-get install -y --no-install-recommends openssh-server
RUN mkdir -p /run/sshd && echo "root:Docker!" | chpasswd
COPY ./Execise1.Api/DockerSettings/sshd_config /etc/ssh/
EXPOSE 2222

# Build a temproary container image with dotnet SDK to build Execise1.Api app
FROM mcr.microsoft.com/dotnet/sdk:3.1-buster AS build-env
WORKDIR /App
COPY ./Execise1.Common ./Execise1.Common
COPY ./Execise1.Data ./Execise1.Data
COPY ./Execise1.DataAccess ./Execise1.DataAccess
COPY ./Execise1.Api ./Execise1.Api
RUN dotnet restore ./Execise1.Common/Execise1.Common.csproj 
RUN dotnet restore ./Execise1.Data/Execise1.Data.csproj 
RUN dotnet restore ./Execise1.DataAccess/Execise1.DataAccess.csproj 
RUN dotnet restore ./Execise1.Api/Execise1.Api.csproj

RUN dotnet publish ./Execise1.Api/Execise1.Api.csproj -c Release -o ./out

# Copy published Execise1.Api on the final container image. 
FROM base AS publish-env
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT [ \
    "/bin/bash", \
    "-c", \
    "/usr/sbin/sshd && dotnet Execise1.Api.dll" \
]
