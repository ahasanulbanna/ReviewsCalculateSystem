using ReviewsCalculateSystem.Models.Models;
using ReviewsCalculateSystem.Services;
using System.Security.Claims;
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
        public IHttpActionResult CreateReviewer([FromBody]Reviewer reviewer)
        {
            return Ok(services.CreateReviewer(reviewer).Data);
        }

        [HttpGet]
        [Route("GetAllReviewer")]
        public IHttpActionResult GetAllReviewer()
        {
            return Ok(services.GetAllReviewer().Data);
        }

        [Authorize]
        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello" + identity.Name);
        }

    }
}
