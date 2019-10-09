[ -d "build" ] && rm -r build
[ -d "docker/build" ] && rm -r docker/build
[ -d "src/todo/build" ] && rm -r src/todo/build

dotnet publish src/todo -o build -f netcoreapp1.1
cp -a src/todo/build/. docker/build