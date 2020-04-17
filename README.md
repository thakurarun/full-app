# full-app
angular + dotnet core  + EF


# full-app
angular + dotnet core  + EF


Run app using:

dotnet watch run --project backend.csproj


For local sql db:
Create DB: sqllocaldb create librarystore
Start DB: sqllocaldb start librarystore
Info Db: sqllocaldb info librarystore


Generate angular lazy module:
ng generate module public-pages --route public --module app.module --flat


setting jenkins: local
https://rangle.io/blog/running-jenkins-and-persisting-state-locally-using-docker-2/
docker image pull jenkins/jenkins

=> create a volume
cmd: docker volume create [YOUR VOLUME]
example: docker volume create demojen

=>Run docker container	
cmd: docker container run -d -p [YOUR PORT]:8080 -v [YOUR VOLUME]:/var/jenkins_home --name jenkins-local jenkins/jenkins
-d: detached mode
-v: attach volume
-p: assign port target
—name: name of the container
example: docker container run -d -p 8989:8080 -v demojen:/var/jenkins_home --name jenkins-local jenkins/jenkins

=> verfiy container
cmd: docker ps

=> go to url
cmd: localhost:[YOUR PORT]
example: http://localhost:8989/

=> Get password
cmd: docker container exec [CONTAINER ID or NAME] sh -c "cat /var/jenkins_home/secrets/initialAdminPassword"
example: docker container exec jenkins-local sh -c "cat /var/jenkins_home/secrets/initialAdminPassword"

