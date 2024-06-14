using Ecom.Api.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Services.Interface
{
    public interface IPaymentRepository
    {
        Task AddPayment(Payment pay);
        Task DeletePayment(int id);
        Task<Payment> GetPaymentById(int id);
    }
}
