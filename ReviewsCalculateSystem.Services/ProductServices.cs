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

        public JsonResult GetAllProductList()
        {
            return new JsonResult
            {
                Data = db.Products.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProductById(int Id)
        {
            return new JsonResult
            {
                Data = db.Products.Where(x => x.ProductId == Id).Select(x => x).FirstOrDefault(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet

            };
        }
    }
    public interface IProductServices
    {
        JsonResult AddProduct(Product product);
        JsonResult GetAllProductList();
        JsonResult GetAllCurrentProductList();
        JsonResult GetProductById(int Id);
     
    }
}
