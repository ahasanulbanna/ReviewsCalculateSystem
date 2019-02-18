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
    [RoutePrefix("api/ReviewerTaskAsign")]
    public class ReviewerTaskAsignController : ApiController
    {
        private readonly ITaskAsignServices services;
        public ReviewerTaskAsignController()
        {
            services = new TaskAsignServices();
        }

        [HttpPost]
        [Route("taskAsign")]
        public IHttpActionResult taskAsignd(List<ReviewerTaskAsign> model)
        {
            return Ok(services.taskAsign(model).Data);
        }

        [HttpGet]
        [Route("getAllAsingTaskById/{Id}")]
        public IHttpActionResult getAllAsingTaskById(int Id)
        {
            return Ok(services.getAllAsingTaskById(Id).Data);
        }

        [HttpGet]
        [Route("getCurrentAsingTaskById/{Id}")]
        public IHttpActionResult getCurrentAsingTaskById(int Id)
        {
            return Ok(services.getCurrentAsingTaskById(Id).Data);
        }

        [HttpGet]
        [Route("reviewerReviewForEachProductById")]
        public IHttpActionResult reviewerReviewForEachProductById(int reviewerId, int productId)
        {
            return Ok(services.reviewerReviewForEachProductById(reviewerId, productId).Data);
        }
        [HttpGet]
        [Route("reviewerDetailsInfoList")]
        public IHttpActionResult reviewerDetailsInfoList(int pageSize, int pageNumber, string searchText)
        {
            return Ok(services.reviewerDetailsInfoList(pageSize,pageNumber,searchText).Data);
        }
        
    }
}
