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
        public IHttpActionResult SubmitProductReview([FromBody]Review model)
        {
            return Ok(services.SubmitProductReview(model).Data);
        }
        [HttpGet]
        [Route("GetReviewByProductId/{Id}")]
        public IHttpActionResult GetReviewByProductId(int Id)
        {
            return Ok(services.GetReviewByProductId(Id).Data);
        }
        [HttpGet]
        [Route("ReviewHistoryForEachProduct/{Id}")]
        public IHttpActionResult ReviewHistoryForEachProduct(int Id)
        {
            return Ok(services.ReviewHistoryForEachProduct(Id).Data);
        }
    }
}
