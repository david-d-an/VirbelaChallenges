# Containerizing Instructions


### This instructions explains activities to build a docker image broken down in steps to allow manually control actions during build. A bash script **`build.sh`** can be activated to execute the entire build activities based on **`Dockerfile`**.<br>

### The future goal is that the branch-merge into **`master`** activates the GitHub action, **`master_Execise1ApiDocker6921.yml`**, which triggers deployment of a Docker container for **`Execise1.Api`** onto **`execise1apidocker6921.azurewebsites.net`** as specified by GitHub.Dockerfile.<br>

<hr>

### Build
* Docker image is build by Dockerfile
```sh
> dotnet publish -c Release
> docker build -f ./DockerSettings/Dockerfile -t execise1.api .
```

### Test
* Application is running on http://localhost:8070
```sh
> docker run -d -p 8070:80 --name execise1.api.container execise1.api
```

### Log in as root user.
* -u 0 is not necessary as Dockerfile sets the default user as root
```sh
> docker exec -it execise1.api.container bash
> docker exec -u 0 -it execise1.api.container /bin/bash
```

### Get Image ID
```sh
> docker images -f=reference='*dong82/execise1.api*:*' -f=reference='execise1.api*:*'
```

### Tag Iamge
```sh
> docker tag $(docker images -q execise1.api:latest) dong82/execise1.api
```

### Get latest tag name
```sh
> tagname=$(./TagName.sh execise1.api)
```

### Push to Repository
```sh
> docker push dong82/execise1.api:$tagname
> docker push dong82/execise1.api:latest
```
