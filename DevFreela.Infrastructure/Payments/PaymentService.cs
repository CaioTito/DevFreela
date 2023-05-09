using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payments;

public class PaymentService : IPaymentService
{
    private readonly IMessageBusService _messageBusService;
    private const string QUEUE_NAME = "Payments";
    public PaymentService(IMessageBusService messageBusService)
    {
        _messageBusService = messageBusService;
    }

    public void ProcessPayment(PaymentInfoDTO paymentInforDTO)
    {
        var paymentInfoJson = JsonSerializer.Serialize(paymentInforDTO);

        var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

        _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
    }
}
