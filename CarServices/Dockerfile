#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CarServices/CarServices.csproj", "CarServices/"]
COPY ["Auth/Auth.csproj", "Auth/"]
RUN dotnet restore "CarServices/CarServices.csproj"
COPY . .
WORKDIR "/src/CarServices"
RUN dotnet build "CarServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarServices.dll"]