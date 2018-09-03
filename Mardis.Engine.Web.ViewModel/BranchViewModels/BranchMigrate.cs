using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.BranchViewModels
{
    public class BranchMigrate
    {
        public string Code { set; get; }
        public string Document { set; get; }
        public string BranchType { set; get; }
        public string BranchName { set; get; }
        public string BranchStreet { set; get; }
        public string BranchReference { set; get; }
        public string PersonName { set; get; }
        public string LatitudeBranch { set; get; }
        public string LenghtBranch { set; get; }
        public Guid IdProvice { set; get; }
        public Guid IdDistrict { set; get; }
        public Guid IdParish { set; get; }
        public Guid IdSector { set; get; }
        public string Rute { set; get; }
        public string IMEI { set; get; }
        public string Mobil { set; get; }
        public string phone { set; get; }
    }
}
