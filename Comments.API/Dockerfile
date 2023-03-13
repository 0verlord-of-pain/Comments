#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Comments.API/Comments.API.csproj", "Comments.API/"]
RUN dotnet restore "Comments.API/Comments.API.csproj"
COPY . .
WORKDIR "/src/Comments.API"
RUN dotnet build "Comments.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Comments.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Comments.API.dll"]