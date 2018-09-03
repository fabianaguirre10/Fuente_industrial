using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Framework
{
    public class IntegerUtil
    {
        public static int GetValue(string value)
        {
            int returnValue = 0;

            if (!Int32.TryParse(value, out returnValue))
            {
                returnValue = 0;
            }

            return returnValue;
        }
    }
}
