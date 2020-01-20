FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
COPY WineryStore.Contracts/WineryStore.Contracts.csproj ./WineryStore.Contracts/
RUN dotnet restore "WineryStore.Contracts/WineryStore.Contracts.csproj"

COPY WineryStore.Persistence/WineryStore.Persistence.csproj ./WineryStore.Persistence/
RUN dotnet restore "WineryStore.Persistence/WineryStore.Persistence.csproj"

COPY WineryStore.API/WineryStore.API.csproj ./WineryStore.API/
RUN dotnet restore "WineryStore.API/WineryStore.API.csproj"

# Copy everything else and build
COPY . ./

RUN dotnet publish "WineryStore.API/WineryStore.API.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WineryStore.API.dll"]