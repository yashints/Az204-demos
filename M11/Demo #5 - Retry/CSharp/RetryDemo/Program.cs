using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RetryDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      OperationWithBasicRetryAsync().Wait();
    }

    // Async method that wraps a call to a remote service (details not shown).
    private static async Task TransientOperationAsync()
    {
      CloudStorageAccount _blobaccount;

      _blobaccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=az2o4stg;AccountKey=ku99S82VvY39nhI8TgKdGuYe7FRXWQk5Pvned2pCN3QHUryNshRa6H4OX9v9fY0/Pmh+NkVMVrDee+2hmcimaw==;EndpointSuffix=core.windows.net");
      var cloudBlobClient = _blobaccount.CreateCloudBlobClient();
      var cloudBlobContainer = cloudBlobClient.GetContainerReference("mycontainter");
      var blobs = await cloudBlobContainer.ListBlobsSegmentedAsync(null);

      foreach (var blob in blobs.Results)
      {
        Console.WriteLine(blob.Uri);
      }

    }


    const int retryCount = 5;
    private static readonly TimeSpan delay = TimeSpan.FromSeconds(20);

    public static async Task OperationWithBasicRetryAsync()
    {
      Console.WriteLine("Request list of the blobs");

      int currentRetry = 0;

      for (; ; )
      {
        try
        {

          // Call external service.
          await TransientOperationAsync();

          // Return or break.
          break;
        }
        catch (StorageException ex)
        {
          Trace.TraceError("Operation Exception");

          currentRetry++;

          Console.WriteLine("Retry.....");

          // Check if the exception thrown was a transient exception
          // based on the logic in the error detection strategy.
          // Determine whether to retry the operation, as well as how
          // long to wait, based on the retry strategy.
          if (currentRetry > retryCount)
          {
            // If this isn't a transient error or we shouldn't retry,
            // rethrow the exception.
            throw;
          }
        }
        catch (Exception ex)
        {
          throw;
        }

        // Wait to retry the operation.
        // Consider calculating an exponential delay here and
        // using a strategy best suited for the operation and fault.
        await Task.Delay(delay);
      }
      Console.Read();
    }

    private static bool IsTransient(Exception ex)
    {
      // Determine if the exception is transient.
      // In some cases this is as simple as checking the exception type, in other
      // cases it might be necessary to inspect other properties of the exception.
      //if (ex is OperationTransientException)
      //    return true;

      var webException = ex as StorageException; //WebException;
      if (webException != null)
      {
        // If the web exception contains one of the following status values
        // it might be transient.
        return true;
      }

      // Additional exception checking logic goes here.
      return false;
    }





  }
}
