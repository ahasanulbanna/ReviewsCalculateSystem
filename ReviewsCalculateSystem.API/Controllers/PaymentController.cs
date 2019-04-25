using ReviewsCalculateSystem.Models.Models;
using ReviewsCalculateSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReviewsCalculateSystem.API.Controllers
{
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
        private readonly IPaymentServices services;
        public PaymentController()
        {
            services = new PaymentServices();  
        }

        [HttpGet]
        [Route("reviewerPayment")]
        public IHttpActionResult reviewerPayment()
        {
            return Ok(services.reviewerPayment().Data);
        }

        [HttpGet]
        [Route("unpaidReviewCalculateByReviewerId/{Id}")]
        public IHttpActionResult unpaidReviewCalculateByReviewerId(int Id)
        {
            return Ok(services.unpaidReviewCalculateByReviewerId(Id).Data);
        }

        [HttpGet]
        [Route("unpaidReviewForEachProductById")]
        public IHttpActionResult unpaidReviewForEachProductById(int reviewerId, int productId )
        {
            return Ok(services.unpaidReviewForEachProductById(reviewerId, productId).Data);
        }
        
        [HttpPost]
        [Route("payAmountLog")]
        public IHttpActionResult PayAmountLog(PaymentLogViewModel payment)
        {
            return Ok(services.payAmountLog(payment).Data);
        }
        [HttpGet]
        [Route("payment-details-by-reviewer-id")]
        public IHttpActionResult paymentDetailsByReviewerId(int reviewerId)
        {
            return Ok(services.paymentDetailsByReviewerId(reviewerId).Data);
        }
        
    }
}
