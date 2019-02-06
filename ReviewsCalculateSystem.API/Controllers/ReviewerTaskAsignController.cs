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
        public IHttpActionResult staskAsignd(ReviewerTaskAsign model)
        {
            return Ok(services.taskAsign(model).Data);
        }

        [HttpGet]
        [Route("getAsingTaskById/{Id}")]
        public IHttpActionResult getAsingTaskById(int Id)
        {
            return Ok(services.getAsingTaskById(Id).Data);
        }
    }
}
