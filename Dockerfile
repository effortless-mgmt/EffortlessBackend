FROM microsoft/dotnet:sdk AS build-env

COPY . /app

WORKDIR /app/EffortlessApi
RUN ["dotnet", "restore"]
WORKDIR /app/EffortlessLibrary
RUN ["dotnet", "restore"]

WORKDIR /app/EffortlessApi
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/EffortlessApi/out .
EXPOSE 80/tcp
ENTRYPOINT [ "dotnet", "EffortlessApi.dll" ]
