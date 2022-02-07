#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Assignments.API/Assignments.API.csproj", "Assignments.API/"]
COPY ["Assignments.DAL/Assignments.DAL.csproj", "Assignments.DAL/"]
COPY ["Assignments.Business/Assignments.Business.csproj", "Assignments.Business/"]
RUN dotnet restore "Assignments.API/Assignments.API.csproj"
COPY . .
WORKDIR "/src/Assignments.API"
RUN dotnet build "Assignments.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Assignments.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Assignments.API.dll"]