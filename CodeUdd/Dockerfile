#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CodeUdd/CodeUdd.csproj", "CodeUdd/"]
COPY ["CodeUdd.Dto/CodeUdd.Dto.csproj", "CodeUdd.Dto/"]
COPY ["CodeUdd.Data/CodeUdd.Data.csproj", "CodeUdd.Data/"]
RUN dotnet restore "CodeUdd/CodeUdd.csproj"
COPY . .
WORKDIR "/src/CodeUdd"
RUN dotnet build "CodeUdd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeUdd.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeUdd.dll"]


