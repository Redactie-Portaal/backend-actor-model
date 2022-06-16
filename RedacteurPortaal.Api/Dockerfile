#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RedacteurPortaal.Api/RedacteurPortaal.Api.csproj", "RedacteurPortaal.Api/"]
COPY ["RedacteurPortaal.Api/Nuget.Config", "RedacteurPortaal.Api/"]
COPY ["RedacteurPortaal.Grains/RedacteurPortaal.Grains.csproj", "RedacteurPortaal.Grains/"]
COPY ["RedacteurPortaal.Helpers/RedacteurPortaal.Helpers.csproj", "RedacteurPortaal.Helpers/"]
COPY ["RedacteurPortaal.Data/RedacteurPortaal.Data.csproj", "RedacteurPortaal.Data/"]
COPY ["RedacteurPortaal.DomainModels/RedacteurPortaal.DomainModels.csproj", "RedacteurPortaal.DomainModels/"]
COPY ["RedacteurPortaal.Tests/RedacteurPortaal.Tests.csproj", "RedacteurPortaal.Tests/"]

ARG username=joepvand
ARG password=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
#RUN dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org
#RUN dotnet nuget add source https://nuget.pkg.github.com/Redactie-Portaal/index.json --name github --username ${username} --password ${password} --store-password-in-clear-text  
#RUN dotnet nuget add source "https://api.nuget.org/v3/index.json"

#RUN dotnet restore "RedacteurPortaal.Api/RedacteurPortaal.Api.csproj"
RUN dotnet restore "RedacteurPortaal.Api/RedacteurPortaal.Api.csproj" --configfile "RedacteurPortaal.Api/Nuget.Config"
#RUN dotnet restore "RedacteurPortaal.Api/RedacteurPortaal.Api.csproj" -s https://nuget.pkg.github.com/Redactie-Portaal/index.json -s https://api.nuget.org/v3/index.json
COPY . .
WORKDIR "/src/RedacteurPortaal.Api"
RUN dotnet build "RedacteurPortaal.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedacteurPortaal.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedacteurPortaal.Api.dll"]
