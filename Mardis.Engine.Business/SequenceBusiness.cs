using System;
using System.Data.SqlClient;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.Business
{
    public class SequenceBusiness:ABusiness
    {
        readonly SequenceDao _sequenceDao;

        public SequenceBusiness(MardisContext mardisContext):base(mardisContext)
        {
            _sequenceDao = new SequenceDao(mardisContext);
        }
        
        public Sequence NextSequence(string code,Guid idAccount)
        {
            var returnItem = _sequenceDao.GetSequenceByCode(code, idAccount);

            if (null != returnItem)
            {

                const string sqlCmd = " UPDATE MardisCore.Sequence SET SequenceCurrent = @1"
                                      + ",ControlSequence = @2 "
                                      + " WHERE Id = @3 "
                                      + " AND ControlSequence = @4 ";

                var sequenceBefore = returnItem.SequenceCurrent++;
                var sequenceAfter = returnItem.SequenceCurrent;
                var sequenceControlBefore = returnItem.ControlSequence++;
                var sequenceControlAfter = returnItem.ControlSequence;

                Context.Database.ExecuteSqlCommand(sqlCmd, new SqlParameter("@1", sequenceAfter),
                                                           new SqlParameter("@2", sequenceControlAfter),
                                                           new SqlParameter("@3", returnItem.Id),
                                                           new SqlParameter("@4", sequenceControlBefore));
            }

            return returnItem;
        }

    }
}
