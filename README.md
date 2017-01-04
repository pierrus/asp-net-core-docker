# TODO list TP

## Etapes principales
* Créer la classe Models.TODO
* Créer l'interface ITodoRepository
* Créer la classe TodoRepository qui implémente ITodoRepository
* Compléter le contrôleur MVC API Controllers.TodoController

## Etapes bonus
* Ajouter une seconde propriété date de création au modèle Todo, renseigner cette propriété à l'ajout d'un nouvel élément dans le contrôleur, puis l'afficher côté client
* Créer un projet de test unitaire, tester le repository, exécuter le test avec "dotnet test"
* Installer MySQL, utilise Entity Framework pour déployer le modèle en base, et le requêter


Postgres image https://hub.docker.com/_/postgres/

Lancer un client en ligne de commande
docker run -it --rm --link postgres:postgres postgres psql -h postgres -d todo -U myuser

## Construire l'image dotnetcore avec les binaires de l'app
rm -r build
rm -r docker/build
dotnet publish src/todo -o build
cp -a build/. docker/build
docker build docker/ -t todo

## LANCER LES CONTENEURS
docker run --name todo --network todo_nw -p 5000:80 -d todo
docker run --name postgres --network todo_nw -v /Users/Pierre/Documents/Projets/asp-net-core-docker/data:/var/lib/postgresql/data/pgdata -e POSTGRES_USER=myuser -e POSTGRES_PASSWORD=none -e POSTGRES_DB=todo -e PGDATA=/var/lib/postgresql/data/pgdata -d postgres
docker run --name ub_shell --network todo_nw --rm -it ubuntu
--> Inutile d'ouvrir le port 5432 sur Postgres, au sein du même réseau ils peuvent communiquer

## TODO
- OK Tester avec un network de type bridge, dans lequel on fait tourner les 2 images l'option --network. Vérifier que l'app peut accéder à Postgres
- Mettre les 2 images dans un compose: celle de l'app build depuis le dockerfile, l'autre est une image Postgres standard
    - Toutefois, quand faire tourner la migration afin de créer les tables dans Postgres ?
    - Spécifier depends_on + une "command" dans l'image web, afin de déployer la migration à la première exécution
- Mettre app + données Postgres dans des volumes