# Containerizing Instructions


### This instructions explains activities to build a docker image broken down in steps to allow manually control actions during build. A bash script **`build.sh`** can be activated to execute the entire build activities based on **`Dockerfile`**.<br>

### The future goal is that the branch-merge into **`master`** activates the GitHub action, **`master_Execise1ApiDocker6921.yml`**, which triggers deployment of a Docker container for **`Exercise1.Api`** onto **`execise1apidocker6921.azurewebsites.net`** as specified by GitHub.Dockerfile.<br>

<hr>

### Build
* Docker image is build by Dockerfile
```sh
> dotnet publish -c Release
> docker build -f ./DockerSettings/Dockerfile -t exercise1.api .
```

### Test
* Application is running on http://localhost:8070
```sh
> docker run -d -p 8070:80 --name exercise1.api.container exercise1.api
```

### Log in as root user.
* -u 0 is not necessary as Dockerfile sets the default user as root
```sh
> docker exec -it exercise1.api.container bash
> docker exec -u 0 -it exercise1.api.container /bin/bash
```

### Get Image ID
```sh
> docker images -f=reference='*dong82/exercise1.api*:*' -f=reference='exercise1.api*:*'
```

### Tag Iamge
```sh
> docker tag $(docker images -q exercise1.api:latest) dong82/exercise1.api
```

### Get latest tag name
```sh
> tagname=$(./TagName.sh exercise1.api)
```

### Push to Repository
```sh
> docker push dong82/execise1.api:$tagname
> docker push dong82/execise1.api:latest
```
