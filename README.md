# ASP.NET CORE + DOCKER DEMO

## Qu'est ce que c'est ?

Une mini application .NET Core + Angular + PostgreSQL, configurée pour être exécutée dans 2 conteneurs Docker.

Les conteneurs peuvent être manipulés indépendamment avec **docker run**, mais aussi de manière plus pratique et user friendly avec docker **compose**.

Cette démo vise à démontrer l'utilité de docker pour les développeurs, en facilitant la création de l'environnement d'exécution sur la machine du développeur.

La mini app Angular + .NET Core est un fork de https://github.com/fiyazbinhasan/asp-net-core-web-api-with-angularjs.

## Images Docker utilisées (FROM)

Aspnetcore https://hub.docker.com/r/microsoft/aspnetcore/

Postgres https://hub.docker.com/_/postgres/

## Pré-requis
Installer docker depuis https://www.docker.com/

Installer .NET Core depuis http://dot.net/

## Créer une image pour le serveur web, avec les binaires de l'application web
Au préalable, compiler et publier l'application avec `./build.sh`

Puis `docker build docker/ -t todo`

L'image est alors disponible, vérifier avec `docker images`

## Publier sur le registry azure privé
docker tag todo todo:V1

docker tag todo:V1 dockercontainerdemo.azurecr.io/todo:V1

docker push dockercontainerdemo.azurecr.io/todo:V1

OU utiliser le script **docker-build.sh**

http://webappdockerdemo.azurewebsites.net

## Activer le déploiement continu dans la webapp
webapp/paramètres du conteneur: déploiement continu --> activé

copier/coller l'URL de webhook

vérifier dans registry/webhooks que l'URL correspond

## Lancer chaque conteneur manuellement

Ajouter un nouveau réseau `docker network create todo_nw`, puis vérifier sa création avec `docker network ls`

PostgreSQL
`docker run --name postgres --network todo_nw -v data:/var/lib/postgresql/data/pgdata -e POSTGRES_USER=myuser -e POSTGRES_PASSWORD=none -e POSTGRES_DB=todo -e PGDATA=/var/lib/postgresql/data/pgdata -d postgres`

Application web
`docker run --name todo --network todo_nw -p 5000:80 -d todo`

Ubuntu bash pour tester l'accessibilité des 2 autres conteneurs sur le network todo_nw
`docker run --name ub_shell --network todo_nw --rm -it ubuntu`

Il est inutile d'ouvrir le port 5432 sur Postgres, au sein du même réseau les conteneurs peuvent communiquer entre eux sur tous les ports que les images déclarent avec **EXPOSE**.

Pour arrêter les conteneurs
`docker stop todo`

## Utiliser docker-compose

Docker-compose permet de définir un environnement applicatif, avec plusieurs conteneurs. Il évite également de devoir manipuler individuellement chaque image (création, suppression), et chaque conteneur (démarrage, arrêt, suppression).

Au préalable, compiler et publier l'application avec `./build.sh`

Depuis le répertoire docker, `docker-compose up`

Pour arrêter les conteneurs, et les supprimer, `docker-compose down`

## TODO
- **OK** Tester avec un network de type bridge, dans lequel on fait tourner les 2 images l'option --network. Vérifier que l'app peut accéder à Postgres
- **OK** Mettre les 2 images dans un compose: celle de l'app build depuis le dockerfile, l'autre est une image Postgres standard
    - Toutefois, quand faire tourner la migration afin de créer les tables dans Postgres ?
    - Spécifier depends_on + une "command" dans l'image web, afin de déployer la migration à la première exécution
- Mettre app + données Postgres dans des volumes
- **OK** Déplacer la connectionString dans appsettings.