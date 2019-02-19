using ReviewsCalculateSystem.Models.Models;
using ReviewsCalculateSystem.Services;
using System.Web.Http;

namespace ReviewsCalculateSystem.API.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductServices services;
        public ProductsController()
        {
            services = new ProductServices();
        }

        [HttpPost]
        [Route("AddProduct")]
        public IHttpActionResult AddProduct(Product product)
        {
            return Ok(services.AddProduct(product).Data);
        }
        [HttpGet]
        [Route("GetAllProductList")]
        public IHttpActionResult GetAllProductList(int pageSize, int pageNumber, string searchText)
        {
            return Ok(services.GetAllProductList(pageSize,pageNumber,searchText).Data);
        }
        [HttpGet]
        [Route("GetAllCurrentProductList")]
        public IHttpActionResult GetAllCurrentProductList()
        {
            return Ok(services.GetAllCurrentProductList().Data);
        }

        [HttpGet]
        [Route("GetProductById/{ProductId}")]
        public IHttpActionResult GetProductById(int ProductId)
        {
            return Ok(services.GetProductById(ProductId).Data);
        }
    }
}
