using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataObject.MardisCommon
{
    public class MapDao : ADao
    {
        public MapDao(MardisContext mardisContext) : base(mardisContext)
        {
        }
        public List<Map> GetMap(Guid idaccont)
        {

            var itemReturn = Context.Map
                                    .Where(tb => tb.idAccount == idaccont &&
                                           tb.status == "A")
                                    .OrderBy(tb => tb.Id)
                                    .ToList();


            return itemReturn;
        }
        public Boolean SaveCodigos(Map Nuevo)
        {

            Context.Map.Add(Nuevo);
            Context.SaveChanges();
            return true;

        }
    }
}
