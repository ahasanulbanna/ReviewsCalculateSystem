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

        public JsonResult ReviewHistoryForEachProduct(int Id)
        {
            var totalReview = db.Reviews.Where(x => x.ProductId == Id).Count();
            var liveReview = db.Reviews.Where(x => x.ProductId == Id && x.ReviewStatus==true).Count();
            var workingReviewer = db.ReviewerTaskAsigns.Where(x => x.ProductId == Id).Select(x=>x.Reviewer).ToList();
            List<Custom> workingReviewerReviewEachProduct = new List<Custom>();
            foreach (var review in workingReviewer)
            {
                var eachProductReviewerReview = db.Reviews.Where(x => x.ProductId == Id && x.ReviewerId == review.ReviewerId).Count();
                workingReviewerReviewEachProduct.Add(new Custom(review.Name,eachProductReviewerReview));
            }

            return new JsonResult
            {
                Data =new { totalReview, liveReview, workingReviewerReviewEachProduct },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }

    public class Custom
    {
        public Custom(string name, int eachProductReviewerReview)
        {
            Name = name;
            CollectReview = eachProductReviewerReview;
        }

        public string Name { get; set; }
        public int CollectReview { get; set; }
    }

    public interface IReviewServices
    {
        JsonResult SubmitProductReview(Review productReview);
        JsonResult GetReviewByProductId(int Id);
        JsonResult ReviewHistoryForEachProduct(int Id);
    }
}
