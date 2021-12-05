# Azure Func Dotnet Playgound

Testing ground for C# (Dotnet) Azure Functions

- [Azure Func Dotnet Playgound](#azure-func-dotnet-playgound)
  - [Prerequisites](#prerequisites)
  - [Run Functions Locally](#run-functions-locally)
  - [Setup Deployment](#setup-deployment)
  - [Setting Up CI/CD](#setting-up-cicd)

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
 az ad sp create-for-rbac --name ${AZURE_SP_NAME} --role contributor --scopes /subscriptions/${AZURE_SUB_ID}/resourceGroups/spacekatt-func-play --sdk-auth
 ```

| GitHub Repo Secrets Name | Description |
| :----------------------  | :- |
| `AZURE_CREDENTIALS` | Output of `az ad sp create-for-rbac` command; service principal credentials. |
| `AZURE_FUNCTION_PUB_PROF` | Azure Functions publish profile (XML), downloaded from Azure. |
| `AZURE_RG` | Azure resource group which we created for the project. |
| `AZURE_SUBSCRIPTION` | Azure subscription Id. |
