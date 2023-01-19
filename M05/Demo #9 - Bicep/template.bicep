param storageAccountName string = 'myStorageAcc2022'
param location string = resourceGroup().location
resource storageaccount 'Microsoft.Storage/storageAccounts@2021-02-01' = {
  name: storageAccountName
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}

resource appInsightsComponents 'Microsoft.Insights/components@2020-02-02' = {
  name: 'name'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

