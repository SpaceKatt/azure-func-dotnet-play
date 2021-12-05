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

## Setting Up CI/CD

See tutorial for [Deploying Bicep with GitHub Actions](https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/deploy-github-actions?tabs=CLI).

```bash
 az ad sp create-for-rbac --name myApp --role contributor --scopes /subscriptions/${AZURE_SUB_ID}/resourceGroups/spacekatt-func-play --sdk-auth
 ```
