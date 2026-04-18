using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppForTesting;

public class ServiceBusTrigger
{
    private readonly ILogger<ServiceBusTrigger> _logger;

    public ServiceBusTrigger(ILogger<ServiceBusTrigger> logger)
    {
        _logger = logger;
    }



    // This function will be triggered when a message is received on the specified Service Bus queue.
    [Function(nameof(ServiceBusTrigger))]
    public async Task Run(
        [ServiceBusTrigger("myqueue", Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}