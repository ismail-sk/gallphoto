FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gallphoto.WebApp/Gallphoto.WebApp.csproj", "Gallphoto.WebApp/"]
RUN dotnet restore "Gallphoto.WebApp/Gallphoto.WebApp.csproj"
COPY . .
WORKDIR "/src/Gallphoto.WebApp"
RUN dotnet build "Gallphoto.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gallphoto.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gallphoto.WebApp.dll"]
