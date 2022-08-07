FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY ./BasketApp.Core/*.csproj ./BasketApp.Core/
COPY ./BasketApp.Data/*.csproj ./BasketApp.Data/
COPY ./BasketApp.Service/*.csproj ./BasketApp.Service/
COPY ./BasketApp.API/*.csproj ./BasketApp.API/
COPY ./SharedLibrary/*.csproj ./SharedLibrary/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./BasketApp.API/*.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT [ "dotnet","BasketApp.API.dll"]