using System;
using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataObject.MardisCommon
{
    public class CodigoReservadosDao: ADao
    {
        public CodigoReservadosDao(MardisContext mardisContext) 
               : base(mardisContext)
        {

        }
        public List<CodigoReservados> GetCodigoReservado(Guid idaccont)
        {

            var itemReturn = Context.CodigoReservados
                                    .Where(tb => tb.idAccount == idaccont &&
                                           tb.estado =="R")
                                    .OrderBy(tb => tb.Code)
                                    .ToList();

             
            return itemReturn;
        }
        public Boolean SaveCodigos(List<CodigoReservados> nuevos)
        {
            
            Context.CodigoReservados.AddRange(nuevos);
            Context.SaveChanges();
            foreach(var x in nuevos)
            {
                x.codeunico = Convert.ToString(x.Code) + Convert.ToString(x.Id);
            }
            Context.SaveChanges();
            return true;

        }
        public List<CodigoReservados> GetListaCodigos(Guid idaccount ,String imei)
        {

            var itemReturn = Context.CodigoReservados
                                    .Where(tb => tb.idAccount == idaccount &&
                                           tb.estado == "R" && tb.imei_id== imei)
                                    .OrderBy(tb => tb.Code)
                                    .ToList();


            return itemReturn;
        }
        public int GetAllCount(Guid idaccount)
        {
            var CountCodigo = Context.CodigoReservados.Where(x => x.idAccount == idaccount).Max(x=> x.Code);
            if (CountCodigo == null)
            {
                return 0;
            }
            else
            {
                return CountCodigo;
            }


        }
    }
}
