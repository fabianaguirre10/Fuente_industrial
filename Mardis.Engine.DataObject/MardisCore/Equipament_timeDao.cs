using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Web.ViewModel.EquipmentViewModels;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class Equipament_timeDao : ADao
    {
        public Equipament_timeDao(MardisContext mardisContext) : base(mardisContext)
        {
        }
        public List<Equipament_time> GetEquipamentTimeImages(int idEquipament)
        {
            return Context.Equipament_times.Where(cs => cs.Idequipament == idEquipament).ToList();
        }
        public List<Equipament_time> GetEquipamentTimeImagesFotos(int idEquipament)
        {
            return Context.Equipament_times.Where(cs => cs.Idequipament > idEquipament).ToList();
        }
        public List<EquipamentImages> GetEquipamentImagesIdType(int idEquiment, int type)
        {
            return Context.EquipamentImages.Where(cs => cs.IdEquipament == idEquiment && cs.ContentType.Equals(type.ToString().Trim())).ToList();
        }

    }
}
