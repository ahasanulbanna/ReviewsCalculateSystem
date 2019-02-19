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
    public class ProductServices : IProductServices
    {
        private readonly ReviewDbContext db;
        public ProductServices()
        {
            db = new ReviewDbContext();
        }

        public JsonResult AddProduct(Product product)
        {
            product.Reviews = null;
            product.CurrentStatus = true;
            db.Products.Add(product);
            db.SaveChanges();
            return new JsonResult
            {
                Data = new { Result = "IsOk" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAllCurrentProductList()
        {
            var getCurrentProducts = db.Products.Where(x => x.CurrentStatus == true).Select(x => x).ToList();
            return new JsonResult
            {
                Data = getCurrentProducts,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAllProductList(int pageSize, int pageNumber, string searchText)
        {
            var productList = db.Products.OrderByDescending(x => x.CurrentStatus).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new JsonResult
            {
                Data = new {Result= productList, Total= productList.Count()},
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProductById(int productId)
        {
            var asigningTaskInfo = db.ReviewerTaskAsigns.Where(x => x.ProductId == productId).Select(x =>new { x.Reviewer.Name,x.ReviewCollectMargin,x.NumberOfReviewCollect,x.PerReviewCost } ).ToList();           
            return new JsonResult
            {
                Data =new {
                    productInfo = db.Products.Where(x => x.ProductId == productId).Select(x => x).FirstOrDefault() ,
                    asigningTaskInfo,
                    totalMargin=asigningTaskInfo.Sum(x=>x.ReviewCollectMargin)
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet

            };
        }
    }
    public interface IProductServices
    {
        JsonResult AddProduct(Product product);
        JsonResult GetAllProductList(int pageSize, int pageNumber, string searchText);
        JsonResult GetAllCurrentProductList();
        JsonResult GetProductById(int productId);
     
    }
}
