FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base

WORKDIR /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /Hotel

COPY Hotel.Query/Hotel.Query.Consumer/Hotel.Query.Consumer.csproj Hotel.Query/Hotel.Query.Consumer/Hotel.Query.Consumer.csproj

RUN dotnet restore Hotel.Query/Hotel.Query.Consumer/Hotel.Query.Consumer.csproj

COPY CQRS-ES/ CQRS-ES/
COPY Hotel.Query/ Hotel.Query/
COPY Hotel.Common/ Hotel.Common/

WORKDIR /Hotel/Hotel.Query/Hotel.Query.Consumer

RUN dotnet build Hotel.Query.Consumer.csproj -c Release -o /app/build


FROM build AS publish

RUN dotnet publish Hotel.Query.Consumer.csproj -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Hotel.Query.Consumer.dll"]
