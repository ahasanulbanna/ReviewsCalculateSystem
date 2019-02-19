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
    public class TaskAsignServices : ITaskAsignServices
    {
        private readonly ReviewDbContext db;
        public TaskAsignServices()
        {
            db = new ReviewDbContext();
        }

        public JsonResult getAllAsingTaskById(int reviewerId)
        {
            var asignTask = db.ReviewerTaskAsigns.Where(x => x.ReviewerId == reviewerId).Select(x =>new {x.Product,x.NumberOfReviewCollect,x.ReviewCollectMargin }).ToList();
            return new JsonResult
            {
                Data = asignTask,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult getCurrentAsingTaskById(int reviewerId)
        {
            var asignTask = db.ReviewerTaskAsigns.Where(x => x.ReviewerId == reviewerId && x.isComplete == false).Select(x => new { x.Product, x.NumberOfReviewCollect, x.ReviewCollectMargin }).ToList();
            return new JsonResult
            {
                Data = asignTask,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult reviewerReviewForEachProductById(int reviewerId, int productId)
        {
            var productReviewInfo = db.ReviewerTaskAsigns.Where(x=>x.ReviewerId==reviewerId && x.ProductId==productId).Select(x =>new { x.Product,x.ReviewCollectMargin,x.NumberOfReviewCollect }).FirstOrDefault();
            return new JsonResult
            {
                Data = productReviewInfo,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult reviewerDetailsInfoList(int pageSize, int pageNumber, string searchText)
        {
            var reviewer = db.Reviewers.Where(x=>x.AdminApprove==true).Select(x => x).ToList();
            List<ReviewerInfo> reviewerInfo = new List<ReviewerInfo>();
            foreach (var r in reviewer)
            {
               
                var workingBookCount = db.ReviewerTaskAsigns.Where(x => x.ReviewerId == r.ReviewerId && x.isComplete==false).GroupBy(x => x.ProductId ).Count();
                var totalReviewMargin =Convert.ToInt16(db.ReviewerTaskAsigns.Where(x => x.ReviewerId == r.ReviewerId && x.isComplete==false).Sum(x=>x.ReviewCollectMargin));
                var totalReviewCollect =Convert.ToInt16(db.ReviewerTaskAsigns.Where(x => x.ReviewerId == r.ReviewerId && x.isComplete==false).Sum(x=>x.NumberOfReviewCollect));
                reviewerInfo.Add(new ReviewerInfo ( r.Name, r.ReviewerId, workingBookCount,totalReviewCollect,totalReviewMargin ));               
            }
            return new JsonResult
            {
                Data =new {
                    Result = reviewerInfo.OrderBy(x => x.WorkingBookCount).Skip((pageNumber - 1) * pageSize).Take(pageSize),
                    Total= reviewerInfo.Count()},
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult taskAsign(List<ReviewerTaskAsign> reviewerTaskAsign)
        {
            foreach (var reviewer in reviewerTaskAsign)
            {
                var currentReviewer = db.ReviewerTaskAsigns.Where(x => x.ProductId == reviewer.ProductId && x.ReviewerId == reviewer.ReviewerId).Select(x => x).FirstOrDefault();
                if (currentReviewer != null)
                {
                    currentReviewer.ReviewCollectMargin = currentReviewer.ReviewCollectMargin + reviewer.ReviewCollectMargin;
                    db.Entry(currentReviewer).CurrentValues.SetValues(currentReviewer);
                    /*List Update Ex.
                     reviewerTaskAsign.First(x => x.ReviewerId == currentReviewer.ReviewerId).ReviewCollectMargin = currentReviewer.ReviewCollectMargin + reviewer.ReviewCollectMargin;
                    */
                    reviewerTaskAsign.Remove(reviewer);
                }
                if (reviewerTaskAsign.Count == 0)
                {
                    break;
                }
            }
            if (reviewerTaskAsign.Count > 0)
            {
                db.ReviewerTaskAsigns.AddRange(reviewerTaskAsign);
            }
           
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
    public interface ITaskAsignServices
    {
        JsonResult taskAsign(List<ReviewerTaskAsign> reviewerTaskAsign);
        JsonResult getAllAsingTaskById(int reviewerId);
        JsonResult getCurrentAsingTaskById(int reviewerId);
        JsonResult reviewerReviewForEachProductById(int reviewerId, int productId);
        JsonResult reviewerDetailsInfoList(int pageSize, int pageNumber, string searchText);

    }

    public class ReviewerInfo
    {      
        public ReviewerInfo(string name, int reviewerId, int workingBookCount, int totalReviewCollect, int totalReviewMargin)
        {
            Name = name;
            ReviewerId = reviewerId;
            WorkingBookCount = workingBookCount;
            TotalReviewCollect = totalReviewCollect;
            TotalReviewMargin = totalReviewMargin;
        }

        public string Name { get; set; }
        public int ReviewerId { get; set; }
        public int? WorkingBookCount { get; set; }
        public int? TotalReviewMargin { get; set; }
        public int TotalReviewCollect { get; set; }

    }


}
