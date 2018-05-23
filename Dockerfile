FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /app

# Copy the project file
COPY *.sln ./
COPY ElectricityService.App/*.csproj ./ElectricityService.App/
COPY ElectricityService.Core/*.csproj ./ElectricityService.Core/
COPY ElectricityService.Infrastructure/*.csproj ./ElectricityService.Infrastructure/


# Restore the packages
RUN dotnet restore

# Copy everything else
COPY . ./
WORKDIR /app/ElectricityService.App

FROM build AS publish
# Build the release
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM microsoft/aspnetcore:2.0 AS runtime
WORKDIR /app

# Copy the output from the build env
COPY --from=publish /app/ElectricityService.App/out ./

EXPOSE 5000

ENTRYPOINT [ "dotnet", "ElectricityService.App.dll" ]