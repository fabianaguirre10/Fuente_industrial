using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    /// <summary>
    /// Tabla de Locales en el sistema
    /// </summary>
    [Table("Branch", Schema = "MardisCore")]
    public  class Branch : IEntity, ISoftDelete
    {

     
        [Key]
        public Guid Id { get; set; } = Guid.Empty;

        public Guid IdAccount { get; set; } = Guid.Empty;

        public string Code { get; set; }

        public string ExternalCode { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public Guid IdCountry { get; set; } = Guid.Empty;

        public Guid IdProvince { get; set; } = Guid.Empty;

        public Guid IdDistrict { get; set; } = Guid.Empty;

        public Guid IdParish { get; set; } = Guid.Empty;

        public Guid IdSector { get; set; } = Guid.Empty;

        public string Zone { get; set; }

        public string Neighborhood { get; set; }

        public string MainStreet { get; set; }

        public string SecundaryStreet { get; set; }

        public string NumberBranch { get; set; }

        public string LatitudeBranch { get; set; }

        public string LenghtBranch { get; set; }

        public string Reference { get; set; }

        public Guid IdPersonOwner { get; set; } = Guid.Empty;

        public string IsAdministratorOwner { get; set; }

        public Guid? IdPersonAdministrator { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        public string TypeBusiness { get; set; } 
        public string ESTADOAGGREGATE { get; set; }
        public string RUTAAGGREGATE { get; set; }
        public string IMEI_ID { get; set; }
        public DateTime? routeDate { get; set; } = DateTime.Now;
        [ForeignKey("IdCountry")]
        public Country Country { get; set; }

        [ForeignKey("IdDistrict")]
        public District District { get; set; }

        [ForeignKey("IdParish")]
        public Parish Parish { get; set; }

        [ForeignKey("IdPersonOwner")]
        public virtual Person PersonOwner { get; set; } = new Person();

        [ForeignKey("IdPersonAdministrator")]
        public  Person PersonAdministration { get; set; } 

        [ForeignKey("IdProvince")]
        public Province Province { get; set; }

        [ForeignKey("IdSector")]
        public Sector Sector { get; set; }

        public ICollection<BranchCustomer> BranchCustomers { get; set; } = new HashSet<BranchCustomer>();

        public ICollection<TaskCampaign> TaskCampaigns { get; set; } = new HashSet<TaskCampaign>();

        public List<BranchImages> BranchImages { get; set; } = new List<BranchImages>();
    }
}
