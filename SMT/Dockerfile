FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 5218

ENV ASPNETCORE_URLS=http://+:5218

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["SMT/SMT.csproj", "SMT/"]
RUN dotnet restore "SMT\SMT.csproj"
COPY . .
WORKDIR "/src/SMT"
RUN dotnet build "SMT.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "SMT.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMT.dll"]
