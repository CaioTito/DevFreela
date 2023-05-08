using DevFreela.Core.DTOs;

namespace DevFreela.Core.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(PaymentInfoDTO paymentInforDTO);
    }
}
