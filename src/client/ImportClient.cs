using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using orchestrator;

namespace client
{
    public class ImportClient
    {
        [FunctionName (nameof (StartOrchestoration))]
        public static async Task StartOrchestoration (
            [BlobTrigger ("file/user.csv")] CloudBlockBlob blob, [OrchestrationClient] DurableOrchestrationClient starter,
            ILogger log)
        {
            var instanceId = nameof (ImportOrchestorator.ExecuteImportUser);
            var status = await starter.GetStatusAsync (instanceId: instanceId);

            if (status.RuntimeStatus == OrchestrationRuntimeStatus.Running)
            {
                log.LogInformation ($"Already Exsisted Orchestration with ID = '{instanceId}'.");
            }
            else
            {
                await starter.StartNewAsync (nameof (ImportOrchestorator.ExecuteImportUser), instanceId, blob);
                log.LogInformation ($"Delegate Orchestration with ID = '{instanceId}'.");
            }

        }

    }
}