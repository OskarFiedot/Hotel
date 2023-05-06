FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base

WORKDIR /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
# USER appuser


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /Hotel

COPY Hotel.Cmd/Hotel.Cmd.Consumer/Hotel.Cmd.Consumer.csproj Hotel.Cmd/Hotel.Cmd.Consumer/Hotel.Cmd.Consumer.csproj

RUN dotnet restore Hotel.Cmd/Hotel.Cmd.Consumer/Hotel.Cmd.Consumer.csproj

COPY CQRS-ES/ CQRS-ES/
COPY Hotel.Cmd/ Hotel.Cmd/
COPY Hotel.Common/ Hotel.Common/

WORKDIR /Hotel/Hotel.Cmd/Hotel.Cmd.Consumer

RUN dotnet build Hotel.Cmd.Consumer.csproj -c Release -o /app/build


FROM build AS publish

RUN dotnet publish Hotel.Cmd.Consumer.csproj -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Hotel.Cmd.Consumer.dll"]
