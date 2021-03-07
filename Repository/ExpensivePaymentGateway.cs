using PaymentProcessorAPI.DataAccess;
using PaymentProcessorAPI.Models;
using System.Linq;

namespace PaymentProcessorAPI.Repository
{
    public interface IExpensivePaymentGateway
    {
        public void ExpensiveProcess(Request request);
    }

    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        readonly PaymentContext _context;
        readonly ICheapPaymentGateway _cheapPaymentGateway;
        public ExpensivePaymentGateway(PaymentContext paymentContext, ICheapPaymentGateway cheapPaymentGateway)
        {
            _context = paymentContext;
            _cheapPaymentGateway = cheapPaymentGateway;
        }
        public void ExpensiveProcess(Request request)
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
                    try
                    {
                        Request thisRequest = _context.Requests.FirstOrDefault(x => x.CreditCardNumber == request.CreditCardNumber && x. CardHolder == request.CardHolder && x.Amount == request.Amount);

                        thisRequest.PaymentStateId = 2;
                        _context.Update(thisRequest);
                        _context.SaveChanges();
                    }
                    catch
                    {
                        _cheapPaymentGateway.CheapProcess(request);
                    }
                }
        }
    }

}
