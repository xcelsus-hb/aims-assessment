#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DeliVeggie.WebAPI/DeliVeggie.WebAPI.csproj", "DeliVeggie.WebAPI/"]
RUN dotnet restore "DeliVeggie.WebAPI/DeliVeggie.WebAPI.csproj"
COPY . .
WORKDIR "/src/DeliVeggie.WebAPI"
RUN dotnet build "DeliVeggie.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliVeggie.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliVeggie.WebAPI.dll"]