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

    }
}
