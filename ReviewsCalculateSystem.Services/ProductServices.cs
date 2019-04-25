using ReviewsCalculateSystem.Models;
using ReviewsCalculateSystem.Models.Models;
using System.Linq;
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

        public JsonResult AddProduct(Product product, int productId)
        {
            var ExistProduct = db.Products.Any(x => x.ProductId == product.ProductId);
            if (productId > 0)
            {
                Product dbproduct = db.Products.Find(productId);
                if (!db.ReviewerTaskAsigns.Any(x => x.ProductId == productId))
                {
                    dbproduct.ReviewStartDate = product.ReviewStartDate;
                }
                dbproduct.ReviewEndDate = product.ReviewEndDate;
                dbproduct.ProductName = product.ProductName;
                dbproduct.ProductLink = product.ProductLink;
                dbproduct.ProductAsin = product.ProductAsin;
                dbproduct.NumberOfReviewNeed = product.NumberOfReviewNeed;
                db.SaveChanges();
            }
            if (!ExistProduct)
            {
                product.Reviews = null;
                product.CurrentStatus = true;
                db.Products.Add(product);
                db.SaveChanges();
            }
            return new JsonResult
            {
                Data = new { Result = "IsOk" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAllCurrentProductList(int pageSize, int pageNumber, string searchText)
        {
            var getCurrentProducts = db.Products.Where(x => x.CurrentStatus == true).Select(x => x).ToList().OrderByDescending(x=>x.NumberOfReviewNeed);
            return new JsonResult
            {
                Data = new { CurrentProducts=getCurrentProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize), Total = getCurrentProducts.Count() },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAllProductList(int pageSize, int pageNumber, string searchText)
        {
            var productList = db.Products.OrderByDescending(x => x.CurrentStatus).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new JsonResult
            {
                Data = new { Result = productList, Total = productList.Count() },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProductById(int productId)
        {
            var asigningTaskInfo = db.ReviewerTaskAsigns.Where(x => x.ProductId == productId).Select(x => new { x.Reviewer.Name, x.ReviewCollectMargin, x.NumberOfReviewCollect, x.PerReviewCost }).ToList();
            var productInfo1 = db.Products.Where(x => x.ProductId == productId).Select(x => x).FirstOrDefault();

            return new JsonResult
            {
                Data = new
                {
                    productInfo = db.Products.Where(x => x.ProductId == productId).Select(x => x).FirstOrDefault(),
                    asigningTaskInfo,
                    totalMargin = asigningTaskInfo.Sum(x => x.ReviewCollectMargin)
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet

            };
        }
    }
    public interface IProductServices
    {
        JsonResult AddProduct(Product product, int productId);
        JsonResult GetAllProductList(int pageSize, int pageNumber, string searchText);
        JsonResult GetAllCurrentProductList(int pageSize, int pageNumber, string searchText);
        JsonResult GetProductById(int productId);

    }
}
