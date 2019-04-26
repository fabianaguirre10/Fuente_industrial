using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataAccess.MardisPedidos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.Pagos
{
   public class PagosRegisterViewModel
    {
      
        public virtual Branch Branches { get; set; }
        public List<PagosEntidad> pagosEntidads { get; set; }
    
    }
}
