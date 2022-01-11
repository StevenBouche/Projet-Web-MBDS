#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AssignementAPI/AssignmentAPI.csproj", "AssignementAPI/"]
COPY ["Assignement.DAL/Assignement.DAL.csproj", "Assignement.DAL/"]
RUN dotnet restore "AssignementAPI/AssignmentAPI.csproj"
COPY . .
WORKDIR "/src/AssignementAPI"
RUN dotnet build "AssignmentAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssignmentAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssignmentAPI.dll"]
