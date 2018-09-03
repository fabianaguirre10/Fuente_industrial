using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Mardis.Engine.Business.MardisCommon;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Mardis.Engine.Web.Services
{
    public class ServicesLocalizationController : AController<ServicesLocalizationController>
    {
        ProvinceBusiness provinceBusiness;
        CountryBusiness countryBusiness;
        DistrictBusiness districtBusiness;
        ParishBusiness parishBusiness;
        SectorBusiness sectorBusiness;

        public ServicesLocalizationController(UserManager<ApplicationUser> userManager,
                                            IHttpContextAccessor httpContextAccessor,
                                            MardisContext mardisContext,
                                            ILogger<ServicesLocalizationController> logger,
                                            ILogger<ServicesFilterController> loggeFilter,
                                            IMemoryCache memoryCache) :
            base(userManager, httpContextAccessor, mardisContext, logger)
        {
            countryBusiness = new CountryBusiness(mardisContext, memoryCache);
            provinceBusiness = new ProvinceBusiness(mardisContext, memoryCache);
            districtBusiness = new DistrictBusiness(mardisContext);
            parishBusiness = new ParishBusiness(mardisContext);
            sectorBusiness = new SectorBusiness(mardisContext);
        }

        [HttpGet]
        public List<Province> GetProvincesByCountryId(Guid countryId)
        {
            return provinceBusiness.GetProvincesByCountry(countryId);
        }

        [HttpGet]
        public List<Country> GetAllCountries()
        {
            return countryBusiness.GetCountries();
        }

        [HttpGet]
        public List<District> GetDistrictsByProvinceId(Guid idProvince)
        {
            return districtBusiness.GetDistrictByProvince(idProvince);
        }

        [HttpGet]
        public List<Parish> GetParishesByDistrictId(Guid idDistrict)
        {
            return parishBusiness.GetParishByDistrict(idDistrict);
        }

        [HttpGet]
        public List<Sector> GetSectorsByDistrictId(Guid idDistrict)
        {
            return sectorBusiness.GetSectorByDistrict(idDistrict);
        }
    }
}
