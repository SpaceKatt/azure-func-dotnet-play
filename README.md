# Azure Func Dotnet Playgound

Testing ground for C# (Dotnet) Azure Functions

- [Azure Func Dotnet Playgound](#azure-func-dotnet-playgound)
  - [Prerequisites](#prerequisites)
  - [Run Functions Locally](#run-functions-locally)
  - [Setup Deployment](#setup-deployment)
  - [CI/CD](#cicd)
    - [Setting Up CI/CD](#setting-up-cicd)
    - [Pipeline Inventory](#pipeline-inventory)

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

## CI/CD

### Setting Up CI/CD

See tutorial for [Deploying Bicep with GitHub Actions](https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/deploy-github-actions?tabs=CLI).

We need to set up a Service Principal to authenticate the GitHub Actions runner CI/CD process(es) with Azure, for each resource group we wish to deploy to.

```bash
 az ad sp create-for-rbac --name ${AZURE_SP_NAME} --role contributor --scopes /subscriptions/${AZURE_SUB_ID}/resourceGroups/${AZURE_RESOURCE_GROUP} --sdk-auth
 ```

 Several secrets need to be set for GitHub Actions to authenticate with Azure.

| GitHub Repo Secrets Name | Description |
| :----------------------  | :- |
| `AZURE_RG_DEV` | Azure resource group which we created for the project's dev env. |
| `AZURE_RG_STAG` | Azure resource group which we created for the project's dev env. |
| `AZURE_RG_PROD` | Azure resource group which we created for the project's prod env. |
| `AZURE_CREDENTIALS_DEV` | Output of `az ad sp create-for-rbac` command; service principal credentials. |
| `AZURE_CREDENTIALS_STAG` | Output of `az ad sp create-for-rbac` command; service principal credentials. |
| `AZURE_CREDENTIALS_PROD` | Output of `az ad sp create-for-rbac` command; service principal credentials. |
| `AZURE_FUNCTION_PUB_PROF_DEV` | Azure Functions publish profile (XML), downloaded from Azure. |
| `AZURE_FUNCTION_PUB_PROF_STAG` | Azure Functions publish profile (XML), downloaded from Azure. |
| `AZURE_FUNCTION_PUB_PROF_PROD` | Azure Functions publish profile (XML), downloaded from Azure. |
| `AZURE_SUBSCRIPTION` | Azure subscription Id. |

> Issue: Function Publish Profile has to be fetched (currently) after deployment. Thus, initial set up will likely end in failure until secret for profile is fetched and set and pipeline is rerun.

> Note: The author recommends only setting up the `*_DEV` secrets at first, then setting up additional envs later.

### Pipeline Inventory

The following CICD pipelines exist in this project...

| Pipeline | Description | Badge |
| :------  | :---------- | :---- |
| [TODO: CI Workflow](./) | Runs CI workflow on pushed branches. | [![CI Workflow](https://github.com/SpaceKatt/azure-func-dotnet-play/actions/workflows/ci.yml/badge.svg)](https://github.com/SpaceKatt/azure-func-dotnet-play/actions/workflows/ci.yml) |
| [CD Workflow](./.github/workflows/main.yml) | Deploys dev resources on merge to `main`. | [![Azure Bicep Deploy and Publish](https://github.com/SpaceKatt/azure-func-dotnet-play/actions/workflows/cd-dev.yml/badge.svg)](https://github.com/SpaceKatt/azure-func-dotnet-play/actions/workflows/cd-dev.yml) |
| [Release Workflow](./) | Deploys staging and prod resources on push to `release/*`. | [![CD Stage/Prod on Release](https://github.com/SpaceKatt/azure-func-dotnet-play/actions/workflows/cd-release.yml/badge.svg)](https://github.com/SpaceKatt/azure-func-dotnet-play/actions/workflows/cd-release.yml) |
