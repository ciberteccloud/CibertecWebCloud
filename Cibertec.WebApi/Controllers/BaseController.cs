using System.Web.Http;
using Cibertec.UnitOfWork;

namespace Cibertec.WebApi.Controllers
{

    public class BaseController : ApiController
    {
        protected readonly IUnitOfWork _unit;
        public BaseController()
        {
            _unit = new TiboxUnitOfWork();
        }
    }
}
