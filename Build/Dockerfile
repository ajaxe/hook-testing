FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS builder
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
RUN mkdir -p /home/app/
WORKDIR /home/app/
COPY ./src/WebhookTester/WebhookTester.csproj  .
RUN dotnet restore ./WebhookTester.csproj

COPY ./src/WebhookTester/ .
RUN dotnet publish -c Release -r linux-musl-x64 -o published --self-contained

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine

RUN apk add bash icu-libs krb5-libs libgcc libintl libssl3 libstdc++ zlib curl

WORKDIR /home/app/
COPY --from=builder /home/app/published .

RUN chown app:app -R /home/app
USER app

ENV ASPNETCORE_URLS=http://*:5000 \
    ASPNETCORE_ENVIRONMENT=Production

EXPOSE 5000

HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 \
    CMD curl --fail http://localhost:5000/healthcheck || exit

CMD ["dotnet", "./WebhookTester.dll"]