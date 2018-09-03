using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis_Engine_WebCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mardis.Engine.WebCore.Controllers
{
    public class OrdersController : Controller
    {

        private readonly BranchBusiness _branchBusiness;
        private readonly MardisContext _mardisContext;


        public OrdersController(MardisContext mardisContext)
        {
            _branchBusiness = new BranchBusiness(mardisContext);
            _mardisContext = mardisContext;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            //var branches = _branchBusiness.GetAll();
            var node= DataSourceLoader.Load(_mardisContext.Regions, loadOptions);
            return node;
        }

    }
}