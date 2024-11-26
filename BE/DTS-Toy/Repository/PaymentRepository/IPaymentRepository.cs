using Business.DTO.Address;
using Business.DTO.Payment;
using Business.Helper;
using Business.Response;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PaymentRepository
{
    public interface IPaymentRepository
    {
        Task<Response> GetPaymentsByUserIdAsync(int pageIndex, int pageSize, PaymentStatus? status = null);
        Task<PaginatedList<PaymentDTO>> GetPaymentsAsync(int pageIndex, int pageSize, PaymentStatus? status = null);
        Task<Response> CreatePaymentUrl(Guid paymentId);

        Task<Response> ProcessVnPayCallback(Guid paymentID, CreatePaymentDTO createPayment);
    }
}
