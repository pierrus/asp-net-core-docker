docker rmi todo:sqllite
docker rmi todo
#docker rmi dockercontainerdemo.azurecr.io/todo:sqllite

docker build docker/ -t todo

docker tag todo todo:sqllite-2.2
#docker tag todo:sqllite frcontainerregistry.azurecr.io/todo:sqllite
#docker push frcontainerregistry.azurecr.io/todo:sqllite