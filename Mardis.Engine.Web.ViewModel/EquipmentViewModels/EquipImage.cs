using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.EquipmentViewModels
{
   public  class EquipImage
    {
        public string Uri { get; set; }
        public string Table { get; set; }
        public int orden { get; set; }
        public Byte[] valor { get; set; }
    }
}
