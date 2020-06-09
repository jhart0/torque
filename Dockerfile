FROM jhart0/aspnet_python_node AS base
WORKDIR /app
EXPOSE 80

FROM jhart0/dotnet_python_node AS build
WORKDIR /src
COPY . .
RUN dotnet build . -c Release -o /app

FROM build AS publish
RUN dotnet publish . -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "torque.dll"]
