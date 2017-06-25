
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Cibertec.Models;
using Cibertec.UnitOfWork;

namespace Cibertec.WebApi.Controllers
{
    [RoutePrefix("product")]
    public class ProductController : BaseController
    {        
        public ProductController(IUnitOfWork unit) : base(unit)
        {            
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Products.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Product product)
        {   
            var id = _unit.Products.Insert(product);
            return Ok(new
            {
                id = id
            });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_unit.Products.Update(product)) return BadRequest("Incorrect id");
            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var result = _unit.Products.Delete(new Product { Id = id });
            return Ok(new { delete = true });
        }

        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Products.GetAll());
        }
    }
}
