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
            db.Products.Add(product);
            db.SaveChanges();
            return new JsonResult
            {
                Data = new { Result = "IsOk" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
    public interface IProductServices
    {
        JsonResult AddProduct(Product product);
    }
}
