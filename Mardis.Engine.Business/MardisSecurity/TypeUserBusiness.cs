using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.DataObject.MardisSecurity;
using Mardis.Engine.Framework;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mardis.Engine.Business.MardisSecurity
{
    public class TypeUserBusiness : ABusiness
    {
        private readonly RedisCache _myCache;
        private const string CacheName = "TypeUser";

        public TypeUserBusiness(MardisContext mardisContext, RedisCache memoryCache)
            : base(mardisContext)
        {
            _myCache = memoryCache;
            if (_myCache.Get<List<TypeUser>>(CacheName)== null)
            {
                var typeUserDao = new TypeUserDao(mardisContext);
                _myCache.Set(CacheName, typeUserDao.GetAll());
            }
        }

        public List<SelectListItem> GetAllListItems()
        {
            return _myCache.Get<List<TypeUser>>(CacheName).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            })
                .ToList();
        }

        public TypeUser Get(Guid idTypeUser)
        {
            return _myCache.Get<List<TypeUser>>(CacheName).FirstOrDefault(t => t.Id == idTypeUser);
        }

    }
}
