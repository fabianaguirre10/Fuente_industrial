using AutoMapper;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataAccess.MardisPedidos;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.DataObject.MardisPedidos;
using Mardis.Engine.Web.ViewModel.BranchViewModels;
using Mardis.Engine.Web.ViewModel.Filter;
using Mardis.Engine.Web.ViewModel.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Business.MardisPedidos
{
   public class PagosBusiness : ABusiness
    {
        private readonly PagosDao _pagosDao;
        private readonly BranchDao _branchDao;
        private readonly ChequePagosDao _chequePagos;
        public PagosBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _pagosDao = new PagosDao(mardisContext);
            _branchDao = new BranchDao(mardisContext);
            _chequePagos = new ChequePagosDao(mardisContext);
        }
        public List<Pagos> GetPedidos()
        {
            return _pagosDao.GetPagos();

        }

        public BranchListViewModelPago GetPaginatedPagosBranch(List<FilterValue> filterValues, int pageSize, int pageIndex,Guid idAccount)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Branch, BranchItemViewModel>();
            });
            var itemResult = new BranchListViewModelPago();
            var branches = _branchDao.GetPaginatedBranchesList(filterValues, pageSize, pageIndex, idAccount);
            var countBranches = _branchDao.GetPaginatedBranchesCount(filterValues, pageSize, pageIndex, idAccount);

            itemResult.BranchList = Mapper.Map<List<BranchItemViewModel>>(branches);

            return ConfigurePagination(itemResult, pageIndex, pageSize, filterValues, countBranches);

        }

        public PagosRegisterViewModel GetPago_Profile(String Id, Guid Idaccount)
        {

            var model = new PagosRegisterViewModel();
            List<PagosEntidad> equipmentsmodel = new List<PagosEntidad>();
            equipmentsmodel = _pagosDao.GetPagosProfile(Id);
            model.pagosEntidads = equipmentsmodel;
            if (_branchDao.GetOneCode(Id, Idaccount) != null)
                model.Branches = _branchDao.GetOneCode(Id, Idaccount);
            return model;

        }



    }
}
