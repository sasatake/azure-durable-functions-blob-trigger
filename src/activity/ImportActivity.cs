using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace activity
{
    public class ImportActivity
    {
        [FunctionName (nameof (TestActivity))]
        public static async Task<string> TestActivity ([ActivityTrigger] CloudBlockBlob blob, ILogger log)
        {
            log.LogInformation ($"Start TestActivity with Blob = {blob.Name}");
            await Task.Delay (10000);
            return blob.Name;
        }

    }
}