using PaymentProcessorAPI.Models;
using PaymentProcessorAPI.Repository;
using System;

namespace PaymentProcessorAPI.Services
{
    public class GatewayService
    {
        readonly ICheapPaymentGateway _cheapPaymentGateway;
        readonly IExpensivePaymentGateway _expensivePaymentGateway;
        readonly IPremiumPaymentService _premiumPaymentService;
        readonly IFailedService _failedService;

        public GatewayService(ICheapPaymentGateway cheapPaymentGateway, 
            IExpensivePaymentGateway expensivePaymentGateway,
            IPremiumPaymentService premiumPaymentService,
            IFailedService failedService)
        {
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _premiumPaymentService = premiumPaymentService;
            _failedService = failedService;
        }

        public bool Validate(Request request)
        {
            if (request.ExpirationDate > DateTime.Now.Date )
            {
                if (request.CreditCardNumber.Length == 14)
                {
                    if (request.CardHolder != null)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public void ProcessPayment(Request request)
        {
            try
            {
                if (request.Amount < 20)
                {
                    _cheapPaymentGateway.CheapProcess(request);
                }
                else
                {
                    if (request.Amount >= 21)
                    {
                        if (request.Amount <= 500)
                        {
                            _expensivePaymentGateway.ExpensiveProcess(request);
                        }
                    }
                }
                if (request.Amount > 500)
                {
                    _premiumPaymentService.PremiumProcess(request);
                }
            }
            catch
            {
                _failedService.TransactionFailed(request);
            }
        }
    }
}
