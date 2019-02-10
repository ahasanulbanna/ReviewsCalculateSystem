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

        public JsonResult taskAsign(ReviewerTaskAsign reviewerTaskAsign)
        {
            db.ReviewerTaskAsigns.Add(reviewerTaskAsign);
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
        JsonResult taskAsign(ReviewerTaskAsign reviewerTaskAsign);
        JsonResult getAllAsingTaskById(int reviewerId);
        JsonResult getCurrentAsingTaskById(int reviewerId);
        JsonResult reviewerReviewForEachProductById(int reviewerId, int productId);

    }


}
