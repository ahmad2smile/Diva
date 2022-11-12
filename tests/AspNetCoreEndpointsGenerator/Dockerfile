FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/AspNetCoreEndpointsGenerator/AspNetCoreEndpointsGenerator.csproj", "tests/AspNetCoreEndpointsGenerator/"]
RUN dotnet restore "tests/AspNetCoreEndpointsGenerator/AspNetCoreEndpointsGenerator.csproj"
COPY . .
WORKDIR "/src/tests/AspNetCoreEndpointsGenerator"
RUN dotnet build "AspNetCoreEndpointsGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCoreEndpointsGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreEndpointsGenerator.dll"]
