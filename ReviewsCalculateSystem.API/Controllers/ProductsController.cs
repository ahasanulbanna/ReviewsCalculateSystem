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

        [Route("AddProduct")]
        public IHttpActionResult AddProduct(Product product)
        {
            return Ok(services.AddProduct(product).Data);
        }
    }
}
