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
        public IHttpActionResult SubmitProductReview([FromBody]Review review)
        {
            return Ok(services.SubmitProductReview(review).Data);
        }

        [HttpPut]
        [Route("UpdateReview/{reviewId}")]
        public IHttpActionResult UpdateReview(int reviewId,[FromBody]Review review)
        {
            return Ok(services.UpdateReview(reviewId, review).Data);
        }
        [HttpGet]
        [Route("GetReviewByProductIdAndReviewerId")]
        public IHttpActionResult GetReviewByProductIdAndReviewerId(int productId, int reviewerId)
        {
            return Ok(services.GetReviewByProductIdAndReviewerId(productId, reviewerId).Data);
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
        public IHttpActionResult AdminReviewUpdateByChecking(Review review)
        {
            return Ok(services.AdminReviewUpdateByChecking(review).Data);
        }

    }
}
