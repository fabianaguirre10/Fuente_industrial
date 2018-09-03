using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.Converter.Comparer
{
    public class DistinctQuestionComparer : IEqualityComparer<Question>
    {
        public bool Equals(Question x, Question y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Question obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}