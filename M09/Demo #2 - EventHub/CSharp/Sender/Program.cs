// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Configuration;
using Sender;

// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// Get values from the config given their key and their target type.
EventHubsInfo info = config.GetRequiredSection("EventHubsInfo").Get<EventHubsInfo>();
EventHubsSender sender = new EventHubsSender(info);
await sender.SendEvents();