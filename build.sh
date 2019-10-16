[ -d "build" ] && rm -r build
[ -d "src/todo/build" ] && rm -r src/todo/build

[ -d "docker/todo.web/build" ] && rm -r docker/todo.web/build
[ -d "docker/todo.health/build" ] && rm -r docker/todo.health/build

dotnet publish src/todo -o build/todo.web -f netcoreapp2.2
dotnet publish src/todo.health -o build/todo.health -f netcoreapp3.0

cp -a ./build/todo.web docker/todo.web/build
cp -a ./build/todo.health docker/todo.health/build