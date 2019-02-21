using System.Threading.Tasks;
using activity;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace orchestrator
{
    public class ImportOrchestorator
    {
        [FunctionName (nameof (ExecuteImportUser))]
        public static async Task ExecuteImportUser (
            [OrchestrationTrigger] DurableOrchestrationContext context,
            ILogger log)
        {
            log.LogInformation ($"Started Orchestration with ID = '{context.InstanceId}'.");
            var blob = context.GetInput<CloudBlockBlob> ();
            var result = await context.CallActivityAsync<string> (nameof (ImportActivity.TestActivity), blob);
            log.LogInformation ($"Finished Orchestration with Result = '{result}'.");
        }

    }
}