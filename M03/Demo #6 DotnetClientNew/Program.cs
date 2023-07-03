using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.Development.json", false, true)
        .Build();

string connectionString = configuration.GetSection("ConnectionString").Value;
// Create a BlobServiceClient object 
var blobServiceClient = new BlobServiceClient(connectionString);

BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("pdf");

await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
{
  Console.WriteLine("\t" + blobItem.Name);
}
