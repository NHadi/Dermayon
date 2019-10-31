FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["samples/SocialMedia/Dermayon.Sample.SocialMedia.User/Dermayon.Sample.SocialMedia.User.csproj", "samples/SocialMedia/Dermayon.Sample.SocialMedia.User/"]
COPY ["Dermayon.CrossCutting.IoC/Dermayon.CrossCutting.IoC.csproj", "Dermayon.CrossCutting.IoC/"]
COPY ["Dermayon.Infrastructure.Data.MongoRepositories/Dermayon.Infrastructure.Data.MongoRepositories.csproj", "Dermayon.Infrastructure.Data.MongoRepositories/"]
COPY ["Dermayon.Common/Dermayon.Common.csproj", "Dermayon.Common/"]
COPY ["Dermayon.Infrastructure.Data.EFRepositories/Dermayon.Infrastructure.Data.EFRepositories.csproj", "Dermayon.Infrastructure.Data.EFRepositories/"]
COPY ["Dermayon.Infrastructure.Data.DapperRepositories/Dermayon.Infrastructure.Data.DapperRepositories.csproj", "Dermayon.Infrastructure.Data.DapperRepositories/"]
COPY ["Dermayon.Infrastructure.EvenMessaging.Kafka/Dermayon.Infrastructure.EvenMessaging.Kafka.csproj", "Dermayon.Infrastructure.EvenMessaging.Kafka/"]
RUN dotnet restore "samples/SocialMedia/Dermayon.Sample.SocialMedia.User/Dermayon.Sample.SocialMedia.User.csproj"
COPY . .
WORKDIR "/src/samples/SocialMedia/Dermayon.Sample.SocialMedia.User"
RUN dotnet build "Dermayon.Sample.SocialMedia.User.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dermayon.Sample.SocialMedia.User.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dermayon.Sample.SocialMedia.User.dll"]