using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Mardis.Engine.DataAccess.MardisSecurity;

namespace Mardis.Engine.Web.Model
{
    /// <summary>
    /// Aplication User
    /// </summary>
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser() { }

        public ApplicationUser(User user)
        {
            if (user != null)
            {
                ConverToApplicationUser(user);
            }
        }

        public string UserId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid AccountId { get; set; }

        public Guid PersonId { get; set; }

        //public string UserName { get; set; }

        //public string Email { get; set; }

        //public string PasswordHash { get; set; }

        public string InitialPage { get; set; }

        private void ConverToApplicationUser(User inputUser) {

            UserId = inputUser.Id.ToString();
            ProfileId = inputUser.IdProfile;
            AccountId = inputUser.IdAccount;
            PersonId = inputUser.IdPerson;
            UserName = inputUser.Person.Name + " " + inputUser.Person.SurName;
            Email = inputUser.Email;
            PasswordHash = inputUser.Password;
            InitialPage = inputUser.InitialPage;
        }


    }
}
