FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS publish
WORKDIR .
COPY . .
RUN dotnet restore "./src/API/API.csproj" --runtime alpine-x64
COPY . .
RUN dotnet publish "./src/API/API.csproj" -c Release -o /app/publish \
  --no-restore \
  --runtime alpine-x64 \
  --self-contained true \
  /p:PublishTrimmed=true \
  /p:PublishSingleFile=true 

FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-alpine AS final

RUN adduser --disabled-password \
  --home /app \
  --gecos '' dotnetuser && chown -R dotnetuser /app

# upgrade musl to remove potential vulnerability
RUN apk upgrade musl

USER dotnetuser
WORKDIR /app
EXPOSE 5000
COPY --from=publish /app/publish .

ENTRYPOINT ["./API", "--urls", "http://0.0.0.0:5000"]
