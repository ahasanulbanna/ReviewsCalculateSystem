using ReviewsCalculateSystem.Models;
using ReviewsCalculateSystem.Models.Models;
using System.Collections.Generic;
using System.Linq;
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

        public JsonResult GetReviewByProductId(int productId)
        {
            /*
             * Review list for specefic product             
             */
            return new JsonResult
            {
                Data = db.Reviews.Where(x => x.ProductId == productId).Select(x => x).ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SubmitProductReview(Review review)
        {
            /*
             * When submit the review,
               update NumberOfReviewCollect propety of Products & ReviewerTaskAsigns table's 
            */
            var getProduct = db.Products.Where(x => x.ProductId == review.ProductId).FirstOrDefault();
            var getCollectReview = db.ReviewerTaskAsigns.Where(x => x.ReviewerId == review.ReviewerId && x.ProductId == review.ProductId).FirstOrDefault();
            db.Reviews.Add(review);
            if (getProduct.NumberOfReviewCollect == null && getCollectReview.NumberOfReviewCollect == null)
            {
                getProduct.NumberOfReviewCollect = 0;
                getCollectReview.NumberOfReviewCollect = 0;
            }
            getProduct.NumberOfReviewCollect = 1 + getProduct.NumberOfReviewCollect;
            getCollectReview.NumberOfReviewCollect = 1 + getCollectReview.NumberOfReviewCollect;
            db.Entry(getProduct).CurrentValues.SetValues(getProduct);
            db.Entry(getCollectReview).CurrentValues.SetValues(getCollectReview);
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

        public JsonResult ReviewHistoryForEachProduct(int productId)
        {
            /*         
             *Calculate total review collect for this product
             *Calculate live review 
             *Who are working this product?
             */
            var totalReview = db.Reviews.Where(x => x.ProductId == productId).Count();
            var liveReview = db.Reviews.Where(x => x.ProductId == productId && x.ReviewStatus == true).Count();
            var workingReviewer = db.ReviewerTaskAsigns.Where(x => x.ProductId == productId).Select(x => x.Reviewer).ToList();
            List<Custom> workingReviewerReviewEachProduct = new List<Custom>();
            foreach (var review in workingReviewer)
            {
                var eachProductReviewerReview = db.Reviews.Where(x => x.ProductId == productId && x.ReviewerId == review.ReviewerId).Count();
                workingReviewerReviewEachProduct.Add(new Custom(review.Name, eachProductReviewerReview));
            }

            return new JsonResult
            {
                Data = new { totalReview, liveReview, workingReviewerReviewEachProduct },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AdminReviewUpdateByChecking(List<Review> reviewList)
        {
            /*
             * Admin check review which are live on for specefic product
             * Only update ReviewStatus property of Reviews table
             */
            foreach (var review in reviewList)
            {
                var dbreview = db.Reviews.Find(review.ReviewId);
                db.Entry(dbreview).CurrentValues.SetValues(review.ReviewStatus);
                db.SaveChanges();
            }
            return new JsonResult
            {
                Data = "IsOk",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }

    public class Custom
    {
        /*
         * Use on ReviewHistoryForEachProduct for who are working which product and collect how much review
         */
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
        JsonResult GetReviewByProductId(int productId);
        JsonResult ReviewHistoryForEachProduct(int productId);
        JsonResult AdminReviewUpdateByChecking(List<Review> reviewList);
    }
}




