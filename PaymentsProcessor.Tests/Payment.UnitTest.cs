using System;
using Xunit;
using PaymentProcessorAPI.Controllers;
using PaymentProcessorAPI.Services;
using PaymentProcessorAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsProcessor.Tests
{
    public class PaymentProcessorTests
    {
        readonly GatewayService gatewayService ;

        [Fact]
        public void Post_RequestIsInvalid_Returns400()
        {
            PaymentsController paymentsController = new PaymentsController(gatewayService);
            var FakeRequest = new Request();
            FakeRequest.CreditCardNumber = "30000000000045";
            FakeRequest.CardHolder = "Person";
            FakeRequest.ExpirationDate = new DateTime(2020,2,3); //invalid Expiration date
            FakeRequest.SecurityCode = "";
            FakeRequest.Amount = 25;

            var result = paymentsController.Post(FakeRequest);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
