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
        public IHttpActionResult GetAllProductList()
        {
            return Ok(services.GetAllProductList().Data);
        }
        [HttpGet]
        [Route("GetAllCurrentProductList")]
        public IHttpActionResult GetAllCurrentProductList()
        {
            return Ok(services.GetAllCurrentProductList().Data);
        }

        [HttpGet]
        [Route("GetProductById/{Id}")]
        public IHttpActionResult GetProductById(int Id)
        {
            return Ok(services.GetProductById(Id).Data);
        }
    }
}
