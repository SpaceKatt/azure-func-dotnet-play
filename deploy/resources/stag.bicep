var envConfig = json(loadTextContent('./config/stag.json'))

module stagModule './modules/functions.bicep' ={
  name: envConfig.deploymentName
  params: {
    environmentLabel: envConfig.environemntLabel
  }
}

output functionAppName string = stagModule.outputs.functionAppName
