docker rmi todo:sqllite
docker rmi todo
docker rmi todo.web
docker rmi todo.health

#docker rmi dockercontainerdemo.azurecr.io/todo:sqllite


docker build docker/todo.web -t todo.web
docker build docker/todo.health -t todo.health


#docker tag todo todo:sqllite-2.2
#docker tag todo:sqllite frcontainerregistry.azurecr.io/todo:sqllite
#docker push frcontainerregistry.azurecr.io/todo:sqllite