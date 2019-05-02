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

        public JsonResult GetAllAdminList()
        {
            return new JsonResult
            {
                Data = db.Admins.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult AdminDashbord()
        {
            var totalReviewer = db.Reviewers.Where(x => x.AdminApprove == true).Count();
            var totalPendingReviewer = db.Reviewers.Where(x => x.AdminApprove == false).Count();
            var totalProject = db.Products.Count();
            var totalProjectDone = db.Products.Where(x => x.CurrentStatus == false).Count();
            
            return new JsonResult
            {
                Data = new {totalReviewer,totalPendingReviewer,totalProject,totalProjectDone },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
    public interface IAdminServices
    {
        JsonResult CreateAdmin(Admin admin);
        JsonResult GetAllAdminList();
        JsonResult AdminDashbord();
    }
}
