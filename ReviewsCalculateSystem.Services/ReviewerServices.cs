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
            db.Reviewers.Add(reviewer);
            db.SaveChanges();
            return new JsonResult
            {
                Data = new { Result = "IsOk" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
    public interface IReviewerServices
    {
        JsonResult CreateReviewer(Reviewer reviewer);
    }
}
