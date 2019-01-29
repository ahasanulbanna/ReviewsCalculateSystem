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
    public class ReviewServices : IReviewServices
    {
        private readonly ReviewDbContext db;
        public ReviewServices()
        {
            db = new ReviewDbContext();
        }
        public JsonResult SubmitProductReview(Product productReview)
        {
           db.Reviews.AddRange(productReview.Reviews);
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
    public interface IReviewServices
    {
        JsonResult SubmitProductReview(Product productReview);
    }
}
