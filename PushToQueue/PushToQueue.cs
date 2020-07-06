using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PushToQueue
{
    public static class PushToQueue
    {
        [FunctionName("PushToQueue")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string queueName = Environment.GetEnvironmentVariable("QueueName");
            string serviceBusConnectionString = System.Environment.GetEnvironmentVariable("ServiceBusConnectionString");
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            string responseMessage = serviceBusConnectionString + ":" + queueName;

            var client = new QueueClient(serviceBusConnectionString, queueName);
            await SendMessagesAsync(client, 10);
            return new OkObjectResult(responseMessage);
        }

        static async Task SendMessagesAsync(IQueueClient queueClient, int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the queue.
                    string messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message: {messageBody}");

                    // Send the message to the queue.
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
