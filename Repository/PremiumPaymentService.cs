using PaymentProcessorAPI.DataAccess;
using PaymentProcessorAPI.Models;
using System.Linq;

namespace PaymentProcessorAPI.Repository
{
    public interface IPremiumPaymentService
    {
        public void PremiumProcess(Request request);
    }
    public class PremiumPaymentService : IPremiumPaymentService
    {
        readonly PaymentContext _context;
        public PremiumPaymentService(PaymentContext paymentContext)
        {
            _context = paymentContext;
        }

        public void PremiumProcess(Request request)
        {
            if (request.CardHolder != null)
            {
                request.PaymentStateId = 1;
                _context.Add(request);
                _context.SaveChanges();
            }
            //Processing Payment
            if (request.PaymentStateId == 1)
            {
                int i = 0;
                while (i < 5)
                {
                    Request thisRequest = _context.Requests.FirstOrDefault(x => x.CreditCardNumber == request.CreditCardNumber && x.CardHolder == request.CardHolder && x.Amount == request.Amount);
                    thisRequest.PaymentStateId = 2;
                    _context.Update(thisRequest);
                    _context.SaveChanges();
                    if (thisRequest.PaymentStateId == 2)
                    break;
                }
            }
        }
    }
}
