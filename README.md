# Azure Func Dotnet Playgound

Testing ground for C# (Dotnet) Azure Functions

## Prerequisites

- Install Dotnet 5
- Install Azure Function CLI

## Run Functions Locally

```bash
func start
```

## Setup Deployment

Create resource group.

```bash
az login
az group create --name spacekatt-func-play --location westus
```

Deploy resources into group

```bash
az deployment group create --resource-group spacekatt-func-play --template-file deploy/resources/main.bicep --mode Complete
```

Publish Function App

```bash
func azure functionapp publish fun-spacekattfunc
```
