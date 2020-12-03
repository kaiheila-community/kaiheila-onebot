# Build and Test
FROM mcr.microsoft.com/dotnet/sdk AS build
LABEL maintainer="afanyiyu@hotmail.com"
# Copy Files
COPY . /app
WORKDIR /app
# Build
RUN dotnet restore
RUN dotnet build ./src/kaiheila-onebot.csproj --configuration Release --no-restore
RUN dotnet test ./test/kaiheila-onebot-test.csproj --no-restore --verbosity normal

# Pack
FROM mcr.microsoft.com/dotnet/runtime AS final
# Copy Files
WORKDIR /app
COPY --from=build /app/src/bin/Release/net5.0 /app
RUN mkdir storage
VOLUME /app/storage
EXPOSE 5700 6700
ENTRYPOINT docker kaiheila-onebot.dll
