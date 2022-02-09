FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app

ARG BUILDCONFIG=Release
ARG VERSION=1.0.0

COPY ["./src/PokeDex.Api/PokeDex.Api.csproj", "./src/PokeDex.Api/"]
COPY ["./src/PokeDex.Api.Application/PokeDex.Api.Application.csproj", "./src/PokeDex.Api.Application/"]
COPY ["./src/PokeDex.Api.Contracts/PokeDex.Api.Contracts.csproj", "./src/PokeDex.Api.Contracts/"]
COPY ["./src/PokeDex.Api.Domain/PokeDex.Api.Domain.csproj", "./src/PokeDex.Api.Domain/"]
COPY ["nuget.config", "./src/"]

RUN dotnet restore "./src/PokeDex.Api/PokeDex.Api.csproj" --configfile ./src/nuget.config

COPY . .
WORKDIR "./src/PokeDex.Api/"
RUN dotnet publish -c ${BUILDCONFIG} -o out /p:Version=${VERSION}

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app/src/PokeDex.Api/out ./
ENTRYPOINT ["dotnet", "PokeDex.Api.dll"]
