#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RedacteurPortaal.Silo/RedacteurPortaal.Silo.csproj", "RedacteurPortaal.Silo/"]
RUN dotnet nuget add source --username joepvand --password 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 --store-password-in-clear-text --name github "https://nuget.pkg.github.com/Redactie-Portaal/index.json"
RUN dotnet restore "RedacteurPortaal.Silo/RedacteurPortaal.Silo.csproj"
COPY . .
WORKDIR "/src/RedacteurPortaal.Silo"
RUN dotnet build "RedacteurPortaal.Silo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedacteurPortaal.Silo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedacteurPortaal.Silo.dll"]