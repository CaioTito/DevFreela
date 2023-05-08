using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payments;

public class PaymentService : IPaymentService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _paymentsUrlBase;
    public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _paymentsUrlBase = configuration.GetSection("Services:Payments").Value;
    }

    public async Task<bool> ProcessPayment(PaymentInfoDTO paymentInforDTO)
    {
        var url = $"{_paymentsUrlBase}/api/payments";

        var paymentInfoJson = JsonSerializer.Serialize(paymentInforDTO);
        var paymentInfoContent = new StringContent(
            paymentInfoJson,
            Encoding.UTF8,
            "application/json"
            );

        var httpClient = _httpClientFactory.CreateClient("Payments");

        var response = await httpClient.PostAsync(url, paymentInfoContent);

        return response.IsSuccessStatusCode;
    }
}
