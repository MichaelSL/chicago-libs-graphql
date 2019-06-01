FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ChicagoLibraries.GraphQLHost/ChicagoLibraries.GraphQLHost.csproj", "ChicagoLibraries.GraphQLHost/"]
RUN dotnet restore "ChicagoLibraries.GraphQLHost/ChicagoLibraries.GraphQLHost.csproj"
COPY . .
WORKDIR "/src/ChicagoLibraries.GraphQLHost"
RUN dotnet build "ChicagoLibraries.GraphQLHost.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ChicagoLibraries.GraphQLHost.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChicagoLibraries.GraphQLHost.dll"]