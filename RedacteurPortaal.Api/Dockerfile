#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RedacteurPortaal.Api/RedacteurPortaal.Api.csproj", "RedacteurPortaal.Api/"]
COPY ["RedacteurPortaal.Grains/RedacteurPortaal.Grains.csproj", "RedacteurPortaal.Grains/"]
COPY ["RedacteurPortaal.Helpers/RedacteurPortaal.Helpers.csproj", "RedacteurPortaal.Helpers/"]

RUN dotnet nuget add source --username joepvand --password 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 --store-password-in-clear-text --name github "https://nuget.pkg.github.com/Redactie-Portaal/index.json"
RUN dotnet restore "RedacteurPortaal.Api/RedacteurPortaal.Api.csproj"
COPY . .
WORKDIR "/src/RedacteurPortaal.Api"
RUN dotnet build "RedacteurPortaal.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedacteurPortaal.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedacteurPortaal.Api.dll"]
