# ASP.NET CORE + DOCKER DEMO

## Qu'est ce que c'est ?

Une mini application .NET Core + Angular + PostgreSQL, configurée pour être exécutée dans 2 conteneurs Docker.

## Images Docker utilisées

Aspnetcore https://hub.docker.com/r/microsoft/aspnetcore/
Postgres https://hub.docker.com/_/postgres/

## Pré-requis
Installer docker depuis https://www.docker.com/
Installer .NET Core depuis http://dot.net/

## Compiler l'application et la publier
./build.sh

## Créer une image pour le serveur web, avec les binaires de l'application web
docker build docker/ -t todo
L'image est alors disponibles, vérifier avec `docker images`

## Lancer chaque conteneur manuellement
docker run --name todo --network todo_nw -p 5000:80 -d todo
docker run --name postgres --network todo_nw -v /Users/Pierre/Documents/Projets/asp-net-core-docker/data:/var/lib/postgresql/data/pgdata -e POSTGRES_USER=myuser -e POSTGRES_PASSWORD=none -e POSTGRES_DB=todo -e PGDATA=/var/lib/postgresql/data/pgdata -d postgres
docker run --name ub_shell --network todo_nw --rm -it ubuntu
--> Inutile d'ouvrir le port 5432 sur Postgres, au sein du même réseau ils peuvent communiquer

## Utiliser docker-compose
Depuis le répertoire docker, `docker-compose up`
Pour arrêter les conteneurs, et les supprimer, `docker-compose down`

## TODO
- OK Tester avec un network de type bridge, dans lequel on fait tourner les 2 images l'option --network. Vérifier que l'app peut accéder à Postgres
- OK Mettre les 2 images dans un compose: celle de l'app build depuis le dockerfile, l'autre est une image Postgres standard
    - Toutefois, quand faire tourner la migration afin de créer les tables dans Postgres ?
    - Spécifier depends_on + une "command" dans l'image web, afin de déployer la migration à la première exécution
- Mettre app + données Postgres dans des volumes