using PaymentProcessorAPI.DataAccess;
using PaymentProcessorAPI.Models;

namespace PaymentProcessorAPI.Repository
{
    public interface IFailedService
    {
        public void TransactionFailed(Request request);
    }

    public class FailedService : IFailedService
    {
        readonly PaymentContext _context;
        public FailedService(PaymentContext paymentContext)
        {
            _context = paymentContext;
        }
        public void TransactionFailed(Request request)
        {
            request.PaymentStateId = 3;
            _context.Add(request);
            _context.SaveChanges();
        }
    }
}
