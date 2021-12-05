var envConfig = json(loadTextContent('./config/dev.json'))

module devModule './modules/functions.bicep' ={
  name: envConfig.deploymentName
  params: {
    environmentLabel: envConfig.environemntLabel
  }
}

