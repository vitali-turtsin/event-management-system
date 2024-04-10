FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EventManagementSystem/EventManagementSystem.csproj", "EventManagementSystem/"]
COPY ["BLL.App/BLL.App.csproj", "BLL.App/"]
COPY ["BLL.Base/BLL.Base.csproj", "BLL.Base/"]
COPY ["BLL.Contracts.Base/BLL.Contracts.Base.csproj", "BLL.Contracts.Base/"]
COPY ["Contracts.Base/Contracts.Base.csproj", "Contracts.Base/"]
COPY ["Domain.Contracts.Base/Domain.Contracts.Base.csproj", "Domain.Contracts.Base/"]
COPY ["DAL.Contracts.Base/DAL.Contracts.Base.csproj", "DAL.Contracts.Base/"]
COPY ["BLL.DTO/BLL.DTO.csproj", "BLL.DTO/"]
COPY ["DAL.DTO/DAL.DTO.csproj", "DAL.DTO/"]
COPY ["Domain.App/Domain.App.csproj", "Domain.App/"]
COPY ["Domain.Base/Domain.Base.csproj", "Domain.Base/"]
COPY ["Extensions.Base/Extensions.Base.csproj", "Extensions.Base/"]
COPY ["BLL.Contracts.App/BLL.Contracts.App.csproj", "BLL.Contracts.App/"]
COPY ["DAL.Contracts.App/DAL.Contracts.App.csproj", "DAL.Contracts.App/"]
COPY ["DAL.EF.App/DAL.EF.App.csproj", "DAL.EF.App/"]
COPY ["DAL.Base/DAL.Base.csproj", "DAL.Base/"]
COPY ["DAL.EF.Base/DAL.EF.Base.csproj", "DAL.EF.Base/"]
COPY ["PublicApi.v1.DTO/PublicApi.v1.DTO.csproj", "PublicApi.v1.DTO/"]
RUN dotnet restore "./EventManagementSystem/EventManagementSystem.csproj"
COPY . .
WORKDIR "/src/EventManagementSystem"
RUN dotnet build "./EventManagementSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventManagementSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventManagementSystem.dll"]