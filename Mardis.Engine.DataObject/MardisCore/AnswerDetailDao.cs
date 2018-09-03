using System;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class AnswerDetailDao : ADao
    {
        public AnswerDetailDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public AnswerDetail GetAnswerDetail(Guid idQuestionDetail, int copyNumber, Guid idAnswer, Guid idAccount)
        {
            return Context.AnswerDetails
                .FirstOrDefault(
                    a => a.IdAnswer == idAnswer &&
                    a.CopyNumber == copyNumber &&
                    a.IdQuestionDetail == idQuestionDetail &&
                    a.StatusRegister == CStatusRegister.Active &&
                    a.Answer.IdAccount == idAccount);
        }

        public AnswerDetail GetAnswerDetail(int copyNumber, Guid idAnswer, Guid idAccount)
        {
            return Context.AnswerDetails
                .FirstOrDefault(
                    a => a.IdAnswer == idAnswer &&
                    a.CopyNumber == copyNumber &&
                    a.StatusRegister == CStatusRegister.Active &&
                    a.Answer.IdAccount == idAccount);
        }

    }
}
