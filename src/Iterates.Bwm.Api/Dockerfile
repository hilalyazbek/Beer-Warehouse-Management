FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_ENVIRONMENT = Development
WORKDIR /app
EXPOSE 443
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Iterates.Bwm.Api/Iterates.Bwm.Api.csproj", "src/Iterates.Bwm.Api/"]
RUN dotnet restore "src/Iterates.Bwm.Api/Iterates.Bwm.Api.csproj"
COPY . .
WORKDIR "/src/src/Iterates.Bwm.Api"
RUN dotnet build "Iterates.Bwm.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Iterates.Bwm.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Iterates.Bwm.Api.dll", "--environment=Development"]]
