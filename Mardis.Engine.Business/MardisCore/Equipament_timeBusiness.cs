using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Web.ViewModel.EquipmentViewModels;
namespace Mardis.Engine.Business.MardisCore
{
    public class Equipament_timeBusiness : ABusiness
    {

        private readonly Equipament_timeDao _Equipament_timeDao;



        public Equipament_timeBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            
            _Equipament_timeDao = new Equipament_timeDao(mardisContext);
        }
        public List<Equipament_time> GetEquipamentTimeImages(int idEquipament)
        {
            return _Equipament_timeDao.GetEquipamentTimeImages(idEquipament);
        }
        public List<Equipament_time> GetEquipamentTimeImagesFotos(int idEquipament)
        {
            return _Equipament_timeDao.GetEquipamentTimeImagesFotos(idEquipament);
        }

        public List<EquipamentImages> GetEquipamentidtype(int idEquiment, int type)
        {
            return _Equipament_timeDao.GetEquipamentImagesIdType(idEquiment, type);
        }

    }
}
