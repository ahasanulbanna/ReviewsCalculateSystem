using ReviewsCalculateSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ReviewsCalculateSystem.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly ReviewDbContext db;
        public PaymentServices()
        {
            db = new ReviewDbContext();
        }

        public JsonResult Advance(int reviewerId)
        {
            throw new NotImplementedException();
        }

        public JsonResult unpaidReviewCalculateByReviewerId(int reviewerId)
        {
            var workingProduct = db.ReviewerTaskAsigns.Where(x => x.ReviewerId == reviewerId && x.isComplete==false).Select(x => x).ToList();
            var total = 0.00;
            foreach (var product in workingProduct)
            {
                var reviewCount = db.Reviews.Where(x =>x.ReviewerId==product.ReviewerId && x.ProductId==product.ProductId && x.isPay==false).Count();
                total = reviewCount * product.PerReviewCost + total;
            }
            return new JsonResult
            {
                Data = total,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
             
        }

        public JsonResult unpaidReviewForEachProductById(int reviewerId, int productId)
        {
            var product = db.ReviewerTaskAsigns.Where(x => x.ProductId == productId).FirstOrDefault();
            var productReview = db.Reviews.Where(x => x.ReviewerId == reviewerId && x.ProductId == productId && x.isPay==false).Count();
            var total = 0.00;
            total = product.PerReviewCost * productReview;
            return new JsonResult
            {
                Data = total,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }

    public interface IPaymentServices
    {
        JsonResult unpaidReviewCalculateByReviewerId(int reviewerId);
        JsonResult unpaidReviewForEachProductById(int reviewerId, int productId);
        JsonResult Advance(int reviewerId);
    }


}
