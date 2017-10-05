using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace ConductorAppAdminFunctions
{
    public static class ProcessCommunication
    {
        [FunctionName("ProcessCommunication")]
        public static void Run([QueueTrigger("communication", Connection = "DefaultEndpointsProtocol=https;AccountName=cdttraining;AccountKey=4pG3MXwtbA8ea3RVxZq8Tk2TjGj6M79J5K/jpZ6GZMVRXgNeHrC1XkCx7bPAfh68HmDnkP2X9+a3zSQqzJRH+w==;EndpointSuffix=core.windows.net")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");


        }
    }
}
