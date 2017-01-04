FROM microsoft/aspnetcore
WORKDIR /app
COPY build .
EXPOSE 5000
ENTRYPOINT ["dotnet", "todo.dll"]