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
    public class EquipamentImagesDao : ADao
    {
        public EquipamentImagesDao(MardisContext mardisContext) : base(mardisContext)
        {
        }
        public List<EquipamentImages> GetEquipamentImages(Guid idAccount)
        {
            return Context.EquipamentImages.Where(cs => cs.IdAccount == idAccount).ToList();
        }
        public List<EquipamentImages> GetEquipamentImagesIdType(int idEquiment,int type)
        {
            return Context.EquipamentImages.Where(cs => cs.IdEquipament == type && cs.ContentType==type.ToString()).ToList();
        }
        public IEnumerable<EquipImage> GetImageEquipamentusri(string sql)
        {
            return Context.Query<EquipImage>(sql);
        } 
        public Boolean SaveImageEquipament(EquipamentImages nuevo)
        {
            try
            {
                Context.EquipamentImages.Add(nuevo);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;

            }
        }

    }
}
