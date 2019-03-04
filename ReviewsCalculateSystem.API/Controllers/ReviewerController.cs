using ReviewsCalculateSystem.Models.Models;
using ReviewsCalculateSystem.Services;
using System.Linq;
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
        [Route("GetAllReviewerRequest")]
        public IHttpActionResult GetAllReviewerRequest()
        {
            return Ok(services.GetAllReviewerRequest().Data);
        }

        [HttpGet]
        [Route("AcceptReviewerRequest/{Id}")]
        public IHttpActionResult AcceptReviewerRequest(int Id)
        {
            return Ok(services.AcceptReviewerRequest(Id).Data);
        }

        [HttpGet]
        [Route("GetAllReviewer")]
        public IHttpActionResult GetAllReviewer()
        {
            return Ok(services.GetAllReviewer().Data);
        }




        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello" + identity.Name);
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            return Ok("Hello" + identity.Name +" Role: " +string.Join(",",roles.ToList()));
        }

    }
}
