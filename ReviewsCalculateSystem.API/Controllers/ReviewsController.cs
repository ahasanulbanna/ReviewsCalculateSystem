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
    [RoutePrefix("api/reviews")]
    public class ReviewsController : ApiController
    {
        private readonly IReviewServices services;
        public ReviewsController()
        {
            services = new ReviewServices();
        }

        [HttpPost]
        [Route("SubmitProductReview")]
        public IHttpActionResult SubmitProductReview([FromBody]Product product)
        {
            return Ok(services.SubmitProductReview(product).Data);
        }
    }
}
