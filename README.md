# Webhooks Testing

Webhook testing client to receive callbacks from external systems.

## Docker build

Docker build command is as follows

```shell
docker build . -f .\Build\Dockerfile --tag webhook-tester:local
```

## Deploy DB schema

To get all commands

```shell
dotnet run -- help
```

To deploy all db changes

```shell
dotnet run -- db-apply
```
