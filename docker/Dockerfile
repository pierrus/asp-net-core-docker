FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-bionic
WORKDIR /app
COPY build .
EXPOSE 80
ENTRYPOINT ["dotnet", "todo.dll"]