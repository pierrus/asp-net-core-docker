FROM microsoft/aspnetcore:1.1.1
WORKDIR /app
COPY build .
EXPOSE 80
ENTRYPOINT ["dotnet", "todo.dll"]