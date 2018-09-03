using System;
using Mardis.Engine.DataAccess.MardisSecurity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Mardis.Engine.Web.Libraries.Model
{
    public class ApplicationRole : IdentityRole
    {

        public ApplicationRole(Profile oneProfile)
        {
            ConverToApplicationRole(oneProfile);
        }

        public string RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }


        private void ConverToApplicationRole(Profile inputProfile)
        {
            RoleId = inputProfile.Id.ToString();
            RoleCode = inputProfile.Code;
            RoleName = inputProfile.Name;
        }

    }
}
