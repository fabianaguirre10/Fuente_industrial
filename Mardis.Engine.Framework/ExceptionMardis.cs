using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Framework
{
    /// <summary>
    /// Excepción de Maris
    /// </summary>
    public class ExceptionMardis : Exception
    {
        public ExceptionMardis()
        {
        }

        public ExceptionMardis(string message)
        : base(message)
        {
        }

        public ExceptionMardis(string message, Exception inner)
        : base(message, inner)
        {
        }

        public ExceptionMardis(Exception exception)
            : base(exception.Message)
        {
            
        }
    }
}
