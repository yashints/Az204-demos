resource keyVault 'Microsoft.KeyVault/vaults@2019-09-01' = {
  name: 'az204kvdemo21'
  location: resourceGroup().location
  properties: {
    enabledForDeployment: true
    enabledForTemplateDeployment: true
    enabledForDiskEncryption: true
    tenantId: '72f988bf-86f1-41af-91ab-2d7cd011db47'
    accessPolicies: [
      {
        tenantId: '72f988bf-86f1-41af-91ab-2d7cd011db47'
        objectId: '7c3eabb9-28e3-4f6d-9357-c3b773b866f2'
        permissions: {
          keys: [
            'get'
          ]
          secrets: [
            'list'
            'get'
          ]
        }
      }
    ]
    sku: {
      name: 'standard'
      family: 'A'
    }
  }
}
