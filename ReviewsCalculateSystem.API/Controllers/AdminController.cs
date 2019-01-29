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
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private readonly IAdminServices service;
        public AdminController()
        {
            service = new AdminServices();
        }

        [HttpPost]
        [Route("CreateAdmin")]
        public IHttpActionResult CreateAdmin(Admin admin)
        {
            return Ok(service.CreateAdmin(admin).Data);
        }
    }
}
