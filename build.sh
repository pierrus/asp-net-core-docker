rm -r build
rm -r docker/build
dotnet publish src/todo -o ./build -f netcoreapp1.1
cp -a src/todo/build/. docker/build