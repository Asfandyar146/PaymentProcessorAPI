using PaymentProcessorAPI.DataAccess;
using PaymentProcessorAPI.Models;
using System.Linq;

namespace PaymentProcessorAPI.Repository
{
    public interface ICheapPaymentGateway
    {
        public void CheapProcess(Request request);
    }

    public class CheapPaymentGateway: ICheapPaymentGateway
    {
        readonly PaymentContext _context;
        public CheapPaymentGateway(PaymentContext paymentContext)
        {
            _context = paymentContext;
        }
        public void CheapProcess(Request request)
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
                Request thisRequest = _context.Requests.FirstOrDefault(x => x.CreditCardNumber == request.CreditCardNumber && x.CardHolder == request.CardHolder && x.Amount == request.Amount);

                thisRequest.PaymentStateId = 2;
                _context.Update(thisRequest);
                _context.SaveChanges();
            }
        }
    }

}
