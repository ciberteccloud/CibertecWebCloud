using System.Web.Http;
using Cibertec.Models;
using Cibertec.UnitOfWork;

namespace Cibertec.WebApi.Controllers
{
    [RoutePrefix("order")]
    public class OrderController : BaseController
    {
        public OrderController(IUnitOfWork unit) : base(unit)
        {
        }
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Orders.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.Orders.Insert(order);
            return Ok(new { id = id });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_unit.Orders.Update(order)) return BadRequest("Incorrect id");
            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var result = _unit.Orders.Delete(new Order { Id = id });
            return Ok(new { delete = true });
        }

        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Orders.GetAll());
        }
    }
}
