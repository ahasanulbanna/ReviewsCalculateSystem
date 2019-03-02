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

        public JsonResult reviewerPayment()
        {
            var reviewerList = db.Reviewers.Where(x => x.AdminApprove == true).OrderBy(x => x.Name).ToList();
            List<PaymentViewModel> paymentViewModel = new List<PaymentViewModel>();
            foreach (var reviewer in reviewerList)
            {
                var workingProduct = db.ReviewerTaskAsigns.Where(x => x.ReviewerId == reviewer.ReviewerId && x.isComplete == false).Select(x => x).ToList();
                var total = 0.00;
                var totalLiveReview = 0;
                var ReviewerId = reviewer.ReviewerId;
                var reviewerName = db.Reviewers.Where(x => x.ReviewerId == reviewer.ReviewerId).Select(x => x.Name).FirstOrDefault();
                List<PaymentModel> paymentModels = new List<PaymentModel>();
                foreach (var product in workingProduct)
                {
                    var productName = Convert.ToString(db.Products.Where(x => x.ProductId == product.ProductId).Select(x => x.ProductName).FirstOrDefault());
                    var reviewCount = db.Reviews.Where(x => x.ReviewerId == product.ReviewerId && x.ProductId == product.ProductId && x.isPay == false).Count();
                    total = reviewCount * product.PerReviewCost + total;
                    totalLiveReview += reviewCount;
                    paymentModels.Add(new PaymentModel(productName, reviewCount, product.PerReviewCost, reviewCount * product.PerReviewCost));
                }
                paymentViewModel.Add(new PaymentViewModel(ReviewerId, reviewerName, total, paymentModels));
            }
            return new JsonResult
            {
                Data = paymentViewModel,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult unpaidReviewCalculateByReviewerId(int reviewerId)
        {
            /*
             * Fetch working book list
             * Calculate total live review
             * Calculate total live review cost
             * Calculate each product live review cost
             */
            var workingProduct = db.ReviewerTaskAsigns.Where(x => x.ReviewerId == reviewerId && x.isComplete == false).Select(x => x).ToList();
            var total = 0.00;
            var totalLiveReview = 0;
            var reviewerName = db.Reviewers.Where(x => x.ReviewerId == reviewerId).Select(x => x.Name).FirstOrDefault();
            List<PaymentModel> paymentModels = new List<PaymentModel>();
            foreach (var product in workingProduct)
            {
                var productName = Convert.ToString(db.Products.Where(x => x.ProductId == product.ProductId).Select(x => x.ProductName).FirstOrDefault());
                var reviewCount = db.Reviews.Where(x => x.ReviewerId == product.ReviewerId && x.ProductId == product.ProductId && x.isPay == false).Count();
                total = reviewCount * product.PerReviewCost + total;
                totalLiveReview += reviewCount;
                paymentModels.Add(new PaymentModel(productName, reviewCount, product.PerReviewCost, reviewCount * product.PerReviewCost));
            }
            return new JsonResult
            {
                Data = new { reviewerName, totalAmount = total, totalLiveReview, paymentModels },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public JsonResult unpaidReviewForEachProductById(int reviewerId, int productId)
        {
            var product = db.ReviewerTaskAsigns.Where(x => x.ProductId == productId).FirstOrDefault();
            var productReview = db.Reviews.Where(x => x.ReviewerId == reviewerId && x.ProductId == productId && x.isPay == false).Count();
            var total = 0.00;
            total = product.PerReviewCost * productReview;
            return new JsonResult
            {
                Data = total,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }

    public class PaymentViewModel
    {
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public double TotalPaymentAmount { get; set; }
        public List<PaymentModel> PaymentModel { get; set; }
        public PaymentViewModel(int ReviewerId, string ReviewerName, double TotalPaymentAmount, List<PaymentModel> PaymentModel)
        {
            this.ReviewerId = ReviewerId;
            this.ReviewerName = ReviewerName;
            this.TotalPaymentAmount = TotalPaymentAmount;
            this.PaymentModel = PaymentModel;
        }
    }



    public class PaymentModel
    {
        public string BookName { get; set; }
        public int LiveReview { get; set; }
        public double TotalReviewCost { get; set; }
        public double PerReviewCost { get; set; }
        public PaymentModel(string BookName, int LiveReview, double PerReviewCost, double TotalReviewCost)
        {
            this.BookName = BookName;
            this.LiveReview = LiveReview;
            this.PerReviewCost = PerReviewCost;
            this.TotalReviewCost = TotalReviewCost;
        }

    }

    public interface IPaymentServices
    {
        JsonResult reviewerPayment();
        JsonResult unpaidReviewCalculateByReviewerId(int reviewerId);
        JsonResult unpaidReviewForEachProductById(int reviewerId, int productId);
        JsonResult Advance(int reviewerId);
    }


}
