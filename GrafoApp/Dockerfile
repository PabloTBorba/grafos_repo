﻿FROM microsoft/dotnet:2.1-sdk
WORKDIR /app
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out
CMD ASPNETCORE_URLS=http://*:$PORT dotnet out/GrafoApp.dll