using ReviewsCalculateSystem.Models.Models;
using ReviewsCalculateSystem.Services;
using System.Web.Http;

namespace ReviewsCalculateSystem.API.Controllers
{
    [RoutePrefix("api/reviewer")]
    public class ReviewerController : ApiController
    {
        private readonly IReviewerServices services;
        public ReviewerController()
        {
            services = new ReviewerServices();
        }
        [HttpPost]
        [Route("CreateReviewer")]
        public IHttpActionResult CreateReviewer(Reviewer reviewer)
        {
            return Ok(services.CreateReviewer(reviewer).Data);
        }
    }
}
