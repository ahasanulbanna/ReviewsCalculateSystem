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

        public JsonResult getAsingTaskById(int Id)
        {
            var asignTask = db.ReviewerTaskAsigns.Where(x => x.ReviewerId == Id).Select(x =>new {x.Product,x.NumberOfReviewCollect,x.ReviewerId }).ToList();
            return new JsonResult
            {
                Data = asignTask,
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
        JsonResult getAsingTaskById(int Id);
    }


}
