using Mardis.Engine.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.Business
{
    public abstract class ABulkBusiness
    {
        public  string ConnectionString { get; }

        /// <summary>
        /// 
        /// </summary>
        public  MardisContext MardisContext
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder<MardisContext>();
                optionsBuilder.UseSqlServer(ConnectionString);

                var context = new MardisContext(optionsBuilder.Options);
          
                return context;
            }
        }

        protected ABulkBusiness(string conn)
        {
            ConnectionString = conn;
        }
    }
}
