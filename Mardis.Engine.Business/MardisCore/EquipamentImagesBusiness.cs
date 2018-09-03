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
    public class EquipamentImagesBusiness : ABusiness
    {
        private readonly CampaignDao _campaignDao;
        private readonly EquipamentImagesDao _EquipamentImagesDao;



        public EquipamentImagesBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _campaignDao = new CampaignDao(mardisContext);
            _EquipamentImagesDao = new EquipamentImagesDao(mardisContext);
        }
        public List<EquipamentImages> GetEquipamentImages(Guid idAccount)
        {
            return _EquipamentImagesDao.GetEquipamentImages(idAccount);
        }
        public IEnumerable<EquipImage> GetImageEquipamentusri(string sql)
        {
            return _EquipamentImagesDao.GetImageEquipamentusri(sql);
        }
        public Boolean InsertImageEquipament(EquipamentImages nuevo)
        {
            
            return _EquipamentImagesDao.SaveImageEquipament(nuevo);
            
        }
    }
}
