#!/bin/bash

if [ -z "$1" ] 
    then
    echo "Tag name is required"
    exit 1
fi
tag_num=$1

echo
echo "### Publishing Release code"
dotnet publish -c Release
echo
echo "### Building image"
docker build -f ./DockerSettings/Dockerfile -t execise1.api .

imageid=$(docker images -q execise1.api:latest)
echo
echo "### Tagging image (${imageid}) to: ${tag_num}"
docker tag $(docker images -q execise1.api:latest) dong82/execise1.api:$tag_num
echo
echo "### Tagging image (${imageid}) to: latest"
docker tag dong82/execise1.api:$tag_num dong82/execise1.api:latest

echo
echo "### Pushing image (${imageid}) to Docker Hub: dong82/execise1.api:latest"
docker push dong82/execise1.api:$tag_num
echo
echo "### Pushing image (${imageid}) to Docker Hub: dong82/execise1.api:${tag_num}"
docker push dong82/execise1.api:latest
