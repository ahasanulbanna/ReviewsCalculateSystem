using ReviewsCalculateSystem.Models;
using ReviewsCalculateSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ReviewsCalculateSystem.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly ReviewDbContext db;
        public AdminServices()
        {
            db = new ReviewDbContext();
        }
        public JsonResult CreateAdmin(Admin admin)
        {
            db.Admins.Add(admin);
            db.SaveChanges();
            return new JsonResult
            {
                Data = new
                {
                    Result = "IsOk"
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
    public interface IAdminServices
    {
        JsonResult CreateAdmin(Admin admin);
    }
}
