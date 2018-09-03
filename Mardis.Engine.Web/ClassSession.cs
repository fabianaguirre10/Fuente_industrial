using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mardis.Engine.Web
{
    public  class ClassSession
    {
        private static String variable;

        public static String  Propiedad
        {
            get { return variable; }
            set { variable = value; }
        }

    }
}
