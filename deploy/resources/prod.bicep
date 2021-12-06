var envConfig = json(loadTextContent('./config/prod.json'))

module prodModule './modules/functions.bicep' ={
  name: envConfig.deploymentName
  params: {
    environmentLabel: envConfig.environemntLabel
  }
}

output functionAppName string = prodModule.outputs.functionAppName
