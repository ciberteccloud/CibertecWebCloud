using System.Web.Http;
using Cibertec.Models;
using Cibertec.UnitOfWork;

namespace Cibertec.WebApi.Controllers
{
    [RoutePrefix("supplier")]
    public class SupplierController : BaseController
    {
        public SupplierController(IUnitOfWork unit) : base(unit)
        {
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Suppliers.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Supplier supplier)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.Suppliers.Insert(supplier);
            return Ok(new { id = id });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Supplier supplier)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_unit.Suppliers.Update(supplier)) return BadRequest("Incorrect id");
            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var result = _unit.Suppliers.Delete(new Supplier { Id = id });
            return Ok(new { delete = true });
        }

        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Suppliers.GetAll());
        }
    }
}
