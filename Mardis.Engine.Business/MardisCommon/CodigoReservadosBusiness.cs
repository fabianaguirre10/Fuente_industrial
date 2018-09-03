using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;

namespace Mardis.Engine.Business.MardisCommon
{
    public class CodigoReservadosBusiness
    {
        readonly CodigoReservadosDao _codigoReservadosDaoDao;

        public CodigoReservadosBusiness(MardisContext mardisContext)
        {
            _codigoReservadosDaoDao = new CodigoReservadosDao(mardisContext);
        }

        /// <summary>
        /// Dame Sectores por Distritos
        /// </summary>
        /// <param name="idDistrict"></param>
        /// <returns></returns>
        public List<CodigoReservados> GetCodigoReservado(Guid idaccount)
        {
            return _codigoReservadosDaoDao.GetCodigoReservado(idaccount);
        }
        public int GetAllcodigo(Guid idaccount)
        {
            return _codigoReservadosDaoDao.GetAllCount(idaccount);
        }
        public bool SaveReservados(List<CodigoReservados> nuevos)
        {
            return _codigoReservadosDaoDao.SaveCodigos(nuevos);
        }
        public List<CodigoReservados> GetCodigos(Guid idaccount,String imail)
        {
            return _codigoReservadosDaoDao.GetListaCodigos(idaccount,imail);
        }
    }
}
