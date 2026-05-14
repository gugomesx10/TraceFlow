FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["TraceFlow-Training/TraceFlow-Training.csproj", "TraceFlow-Training/"]
COPY ["TraceFlowTraining.Domain/TraceFlowTraining.Domain.csproj", "TraceFlowTraining.Domain/"]
COPY ["TraceFlowTraining.Infrastructure/TraceFlowTraining.Infrastructure.csproj", "TraceFlowTraining.Infrastructure/"]
COPY ["TraceFlowTraining.Application/TraceFlowTraining.Application.csproj", "TraceFlowTraining.Application/"]

RUN dotnet restore "TraceFlow-Training/TraceFlow-Training.csproj"

COPY . .

WORKDIR "/src/TraceFlow-Training"

RUN dotnet build "./TraceFlow-Training.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "./TraceFlow-Training.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TraceFlow-Training.dll"]