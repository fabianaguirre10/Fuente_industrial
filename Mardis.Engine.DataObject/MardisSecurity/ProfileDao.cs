using System;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisSecurity
{
    public class ProfileDao : ADao
    {
        public ProfileDao(MardisContext context)
            : base(context)
        {

        }
        
        public Profile GetById(Guid idProfile)
        {
            var profile = Context.Profiles;

            var oneProfile = profile
                .FirstOrDefault(p => p.Id == idProfile
                                     && p.StatusRegister == CStatusRegister.Active);


            return oneProfile;
        }
        
        public Profile GetByCode(string code)
        {
            var profile = Context.Profiles;

            var oneProfile = profile
                .FirstOrDefault(p => p.Code == code
                                     && p.StatusRegister == CStatusRegister.Active);


            return oneProfile;
        }
    }
}
