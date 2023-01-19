param image string = 'ghcr.io/gbaeke/super:1.0.5'

resource la 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name: 'la-aca'
  location: resourceGroup().location
  properties: {
    retentionInDays: 30
    sku: {
      name: 'PerGB2018'
    }
  }
}

resource env 'Microsoft.Web/kubeEnvironments@2021-02-01' = {
  name: 'myenv'
  location: resourceGroup().location
  properties: {
    // not recognized but type is required
    type: 'managed'
    internalLoadBalancerEnabled: false
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: la.properties.customerId
        sharedKey: la.listKeys().primarySharedKey
      }
    }
  }
}

resource frontContainerApp 'Microsoft.Web/containerApps@2021-03-01' = {
  name: 'front'
  location: resourceGroup().location
  kind: 'containerapp'
  properties: {
    kubeEnvironmentId: env.id
    configuration: {
      ingress: {
        external: true
        targetPort: 8080
      }
    }
    template: {
      revisionSuffix: 'rev1'
      containers: [
        {
          image: image
          name: 'super'
          env: [
            {
              name: 'WELCOME'
              value: 'Welcome to Container Apps'
            }
          ]
        }
      ]
      scale: {
        minReplicas: 0
        maxReplicas: 10
        rules: [
          {
            name: 'http-rule'
            http: {
              metadata: {
                concurrentRequests: '5'
              }
            }
          }
        ]
      }
    }
  }
}
