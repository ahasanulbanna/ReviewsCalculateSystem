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

        public JsonResult GetReviewByProductId(int Id)
        {
            return new JsonResult
            {
                Data = db.Reviews.Where(x => x.ProductId == Id).Select(x =>new { x.Product,x.SwapmeetFbProfileLink,x.SwapmeetProductLink}).ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SubmitProductReview(Review productReview)
        {
            var getProduct = db.Products.Where(x => x.ProductId == productReview.ProductId).FirstOrDefault();
            db.Reviews.Add(productReview);
            if (getProduct.NumberOfReviewCollect == null)
            {
                getProduct.NumberOfReviewCollect = 0;
            }
            getProduct.NumberOfReviewCollect=1+ getProduct.NumberOfReviewCollect;
            db.Entry(getProduct).CurrentValues.SetValues(getProduct);
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
        JsonResult SubmitProductReview(Review productReview);
        JsonResult GetReviewByProductId(int Id);
    }
}
