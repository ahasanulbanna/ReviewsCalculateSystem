using ReviewsCalculateSystem.Models.Models;
using ReviewsCalculateSystem.Services;
using System.Collections.Generic;
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

        [HttpPost]
        [Route("AdminReviewUpdateByChecking")]
        public IHttpActionResult AdminReviewUpdateByChecking(List<Review> reviewList)
        {
            return Ok(services.AdminReviewUpdateByChecking(reviewList).Data);
        }

    }
}
