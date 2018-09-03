using System.Collections.Generic;
using Mardis.Engine.Business.MardisCommon;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Web.Controllers
{
    public class PersonController : AController<PersonController>
    {

        PersonBusiness personBusiness;

        public PersonController(UserManager<ApplicationUser> userManager,
                                IHttpContextAccessor httpContextAccessor,
                                MardisContext mardisContext,
                                ILogger<PersonController> logger,
                                ILogger<ServicesFilterController> loggeFilter) :
            base(userManager, httpContextAccessor, mardisContext, logger)
        {
            personBusiness = new PersonBusiness(mardisContext);
        }

        [HttpGet]
        public List<Person> GetAllActivePersons()
        {
            return personBusiness.GetActivePersons();
        }

        [HttpGet]
        public Person GetPersonByDocument(string document)
        {
            Person itemReturn = new Person();

            if (string.IsNullOrEmpty(document))
            {
                //Significa que es un item nuevo
                itemReturn = new Person();
            }
            else
            {
                //significa que está en base 
                itemReturn = personBusiness.GetPersonByDocument(document);
            }
            return itemReturn ?? new Person() { Document = document };
        }
    }
}
