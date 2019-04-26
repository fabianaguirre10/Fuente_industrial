using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisPedidos;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.Web.ViewModel.Filter;
using Mardis.Engine.Web.ViewModel.Pagos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataObject.MardisPedidos
{
    public class PagosDao : ADao
    {
        public PagosDao(MardisContext mardisContext) : base(mardisContext)
        {
            CoreFilterDetailDao = new CoreFilterDetailDao(mardisContext);
            CoreFilterDao = new CoreFilterDao(mardisContext);
        }
        public List<Pagos> GetPagos()
        {
            return Context.Pagos.Include(a => a.ChequePagos).Include(a => a.PagosDetalles).ToList();
        }
        public List<PagosEntidad> GetPaginatedPagosList(List<FilterValue> filterValues, int pageSize, int pageIndex)
        {
            var strPredicate = "1==1";
            strPredicate += GetFilterPredicate(filterValues);
            var resultList = Context.Pagos.Select(x => new
            {
                x.Idpago,
                x.CodCliente,
                x.ValorTotalPago,
                x.FormaPago,
                x.Fecha,
                x.idvendedor

            }).Where(strPredicate).
            OrderBy(b => b.Idpago)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
            List<PagosEntidad> listaresultado = new List<PagosEntidad>();
            foreach (var n in resultList)
            {
                PagosEntidad nuevo = new PagosEntidad();
                nuevo.Idpago = n.Idpago;
                nuevo.CodCliente = n.CodCliente;
                nuevo.Fecha = n.Fecha;
                nuevo.FormaPago = n.FormaPago;
                nuevo.idvendedor = n.idvendedor;
                nuevo.ValorTotalPago = n.ValorTotalPago;
                nuevo.Branches = Context.Branches.Where(x => x.Code.Equals(n.CodCliente)).Include(y=> y.PersonOwner).FirstOrDefault();
                listaresultado.Add(nuevo);

            }



            return listaresultado;
        }
        public int GetPaginatedPagosCount(List<FilterValue> filterValues, int pageSize, int pageIndex)
        {
            var strPredicate = "";

            strPredicate += GetFilterPredicate(filterValues);

            return Context.Pagos
                .Count();
        }
        public List<PagosEntidad> GetPagosProfile(String CodCliente)
        {
            var resultList = Context.Pagos.Select(x => new
            {
                x.Idpago,
                x.CodCliente,
                x.ValorTotalPago,
                x.FormaPago,
                x.Fecha,
                x.idvendedor,


            })

                .Where(x => x.CodCliente.Equals(CodCliente)).ToList();

            List<PagosEntidad> Resultado_consulta = new List<PagosEntidad>();
            foreach (var n in resultList)
            {

                PagosEntidad nuevo = new PagosEntidad();
                nuevo.Idpago = n.Idpago;
                nuevo.CodCliente = n.CodCliente;
                nuevo.Fecha = n.Fecha;
                nuevo.FormaPago = n.FormaPago;
                nuevo.idvendedor = n.idvendedor;
                nuevo.ValorTotalPago = n.ValorTotalPago;
                nuevo.ChequePagos = new List<ChequePagos>();
                nuevo.ChequePagos = Context.ChequePagos.Where(x => x.idpago.Equals(n.Idpago)).ToList();
                nuevo.PagosDetalles = new List<PagosDetalles>();
                nuevo.PagosDetalles = Context.PagosDetalles.Where(x => x.idpago.Equals(n.Idpago)).ToList();
                nuevo.FOTO = "https://mardisenginefotos.blob.core.windows.net/industrial/" + n.Idpago + ".jpg";
                Resultado_consulta.Add(nuevo);
            }



            //.Where(usr => usr.Equipaments. == typeUser &&
            //              usr.StatusRegister == CStatusRegister.Active &&
            //              usr.IdAccount == idAccount)

            return Resultado_consulta;
        }

    }
}
