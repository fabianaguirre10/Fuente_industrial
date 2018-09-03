using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisSecurity
{
    /// <summary>
    /// Menu del Sistema
    /// </summary>
    [Table("Menu", Schema = "MardisSecurity")]
    public class Menu : IEntity, ISoftDelete
    {
        
        public Menu()
        {
            MenuChildrens = new HashSet<Menu>();
            AuthorizationProfiles = new HashSet<AuthorizationProfile>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string UrlMenu { get; set; }
        public Guid? IdParent { get; set; }
        public string StatusRegister { get; set; }
        public int OrderMenu { get; set; }

        public ICollection<Menu> MenuChildrens { get; set; }

        [ForeignKey("IdParent")]
        public Menu MenuParent { get; set; }
        public ICollection<AuthorizationProfile> AuthorizationProfiles { get; set; }
       
    }
}
