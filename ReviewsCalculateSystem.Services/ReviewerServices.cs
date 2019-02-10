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
    public class ReviewerServices : IReviewerServices
    {
        private readonly ReviewDbContext db;
        public ReviewerServices()
        {
            db = new ReviewDbContext();
        }


        public JsonResult CreateReviewer(Reviewer reviewer)
        {
            /*
             * Reviewer submit their personal information and waiting for admin confirmation to started journey as reviewer
             */
            
            db.Reviewers.Add(reviewer);
            db.SaveChanges();
            return new JsonResult
            {
                Data = new { Result = "IsOk" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAllReviewer()
        {
            /*
             * Reviewr list who are working on now
             */
            return new JsonResult
            { Data = db.Reviewers.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
         

        }
    }
    public interface IReviewerServices
    {
        JsonResult CreateReviewer(Reviewer reviewer);
        JsonResult GetAllReviewer();
    }
}
