FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY EQueueNameServer/EQueueNameServer.csproj EQueueNameServer/
RUN dotnet restore EQueueNameServer/EQueueNameServer.csproj
COPY . .
WORKDIR /src/EQueueNameServer
RUN dotnet build EQueueNameServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EQueueNameServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EQueueNameServer.dll"]
