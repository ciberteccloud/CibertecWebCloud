using System.Web.Http;
using Cibertec.Models;
using Cibertec.UnitOfWork;

namespace Cibertec.WebApi.Controllers
{
    [RoutePrefix("orderitem")]
    public class OrderItemController : BaseController
    {
        public OrderItemController(IUnitOfWork unit) : base(unit)
        {
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.OrderItems.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(OrderItem orderItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.OrderItems.Insert(orderItem);
            return Ok(new { id = id });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(OrderItem orderItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_unit.OrderItems.Update(orderItem)) return BadRequest("Incorrect id");
            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var result = _unit.OrderItems.Delete(new OrderItem { Id = id });
            return Ok(new { delete = true });
        }

        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.OrderItems.GetAll());
        }
    }
}
