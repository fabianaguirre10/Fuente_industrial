using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class MyTaskViewModel
    {
        //Información de Tarea
        public Guid IdTask { get; set; }

        public string TaskCode { get; set; }

        public Guid IdAccount { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime StartDate { get; set; }

        public string CommentTaskNotImplemented { get; set; }

        public string Route { get; set; }

        public string Description { get; set; }

        public Guid IdTaskNotImplementedReason { get; set; }

        //Información de Mercaderista

        public Guid IdMerchant { get; set; }

        public string MerchantName { get; set; }

        public Guid IdMerchantPerson { get; set; }

        public string MerchantSurname { get; set; }

        //Información de Campaña

        public Guid IdCampaign { get; set; }

        public string CampaignName { get; set; }

        //Información de Local

        public Guid IdBranch { get; set; }

        public string BranchName { get; set; }

        public string BranchLabel { get; set; }

        public string BranchMardisCode { get; set; }

        public string BranchExternalCode { get; set; }

        public string BranchLongitude { get; set; }

        public string BranchLatitude { get; set; }

        public string BranchNeighborhood { get; set; }

        public string BranchMainStreet { get; set; }

        public string BranchSecundaryStreet { get; set; }

        public string BranchReference { get; set; }

        public string BranchTypeBusiness { get; set; }
        
        //Información de Cliente

        public Guid IdCustomer { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public Guid IdStatusTask { get; set; }

        public string StatusTaskName { get; set; }

        public string BranchOwnerName { get; set; }

        public string BranchOwnerDocument { get; set; }

        public string BranchOwnerPhone { get; set; }

        public string BranchOwnerMobile { get; set; }

        public string BranchProvince { get; set; }

        public string BranchCity { get; set; }

        public string BranchSector { get; set; }

        public string BranchParish { get; set; }

        public string BranchZone { get; set; }

        public Guid IdTaskNotImplemented { get; set; }

        public List<MyTaskServicesViewModel> ServiceCollection { get; set; }

        public List<BranchImages> BranchImages { get; set; }

        public string CurrentSection { get; set; }

        public int CurrentService { get; set; }


        public string CodeGemini { get; set; }
    }
}
