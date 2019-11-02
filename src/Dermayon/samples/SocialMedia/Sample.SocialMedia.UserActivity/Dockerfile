FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["samples/SocialMedia/Sample.SocialMedia.UserActivity/Sample.SocialMedia.UserActivity.csproj", "samples/SocialMedia/Sample.SocialMedia.UserActivity/"]
COPY ["Dermayon.CrossCutting.IoC/Dermayon.CrossCutting.IoC.csproj", "Dermayon.CrossCutting.IoC/"]
COPY ["Dermayon.Infrastructure.Data.MongoRepositories/Dermayon.Infrastructure.Data.MongoRepositories.csproj", "Dermayon.Infrastructure.Data.MongoRepositories/"]
COPY ["Dermayon.Common/Dermayon.Common.csproj", "Dermayon.Common/"]
COPY ["Dermayon.Infrastructure.Data.EFRepositories/Dermayon.Infrastructure.Data.EFRepositories.csproj", "Dermayon.Infrastructure.Data.EFRepositories/"]
COPY ["Dermayon.Infrastructure.Data.DapperRepositories/Dermayon.Infrastructure.Data.DapperRepositories.csproj", "Dermayon.Infrastructure.Data.DapperRepositories/"]
COPY ["Dermayon.Infrastructure.Data.EventSources/Dermayon.Infrastructure.Data.EventSources.csproj", "Dermayon.Infrastructure.Data.EventSources/"]
COPY ["Dermayon.Infrastructure.EvenMessaging.Kafka/Dermayon.Infrastructure.EvenMessaging.Kafka.csproj", "Dermayon.Infrastructure.EvenMessaging.Kafka/"]
RUN dotnet restore "samples/SocialMedia/Sample.SocialMedia.UserActivity/Sample.SocialMedia.UserActivity.csproj"
COPY . .
WORKDIR "/src/samples/SocialMedia/Sample.SocialMedia.UserActivity"
RUN dotnet build "Sample.SocialMedia.UserActivity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.SocialMedia.UserActivity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.SocialMedia.UserActivity.dll"]
