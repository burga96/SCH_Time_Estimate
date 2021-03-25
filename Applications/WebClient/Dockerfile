FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli

WORKDIR /src

COPY ["Core/Domain/Core.Domain.csproj", "Core/Domain/"]

COPY ["Core/Infrastructure/DataAccess/Core.Infrastructure.DataAccess.csproj", "Core/Infrastructure/DataAccess/"]
 
COPY ["Core/ApplicationServices/Core.ApplicationServices.csproj", "Core/ApplicationServices/"]

COPY ["Applications/WebClient/Applications.WebClient.csproj", "Applications/WebClient/"]

RUN dotnet restore "Applications/WebClient/Applications.WebClient.csproj"
COPY . .
WORKDIR /src/Applications/WebClient

RUN dotnet build "Applications.WebClient.csproj" -c Release -o /build

FROM build AS publish
RUN dotnet publish "Applications.WebClient.csproj" -c Release -o /build

FROM base AS final
LABEL author = "sburgic@smartcodehouse.com"
WORKDIR /app
COPY --from=publish /build .

RUN mkdir -p /Logs/Staging/WebClient

ENV ASPNETCORE_ENVIRONMENT=Staging

ENTRYPOINT ["dotnet", "Applications.WebClient.dll"]


 