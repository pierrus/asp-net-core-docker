rm -r build
rm -r docker/build
dotnet publish src/todo -o build
cp -a build/. docker/build
docker build docker/ -t todo