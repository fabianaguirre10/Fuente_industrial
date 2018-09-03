using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.Converter.Comparer
{
    public class DistinctQuestionDetaiComparer : IEqualityComparer<QuestionDetail>
    {
        public bool Equals(QuestionDetail x, QuestionDetail y)
        {
            return x.Id == y.Id &&
                    x.Answer == y.Answer &&
                    x.IdQuestion == y.IdQuestion &&
                    x.IdQuestionLink == y.IdQuestionLink &&
                    x.IsNext == y.IsNext &&
                    x.Order == y.Order &&
                    x.Weight == y.Weight;
        }

        public int GetHashCode(QuestionDetail obj)
        {
            return obj.Id.GetHashCode() ^
                    obj.Answer.GetHashCode() ^
                    obj.IdQuestion.GetHashCode() ^
                    obj.IdQuestionLink.GetHashCode() ^
                    obj.IsNext.GetHashCode() ^
                    obj.Order.GetHashCode() ^
                    obj.GetHashCode();
        }
    }
}
