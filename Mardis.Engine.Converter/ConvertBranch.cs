using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.BranchViewModels;
using Mardis.Engine.Web.ViewModel.TaskViewModels;

namespace Mardis.Engine.Converter
{
  
    public class ConvertBranch
    {
      

        public static BranchProfileViewModel ConvertBranchToBranchProfileViewModel(Branch branch,List<Sms> ListaSms)
        {
            return new BranchProfileViewModel()
            {
                Id = branch.Id,
                Name = branch.Name,
                OwnerName = branch.PersonOwner.Name,
                DistrictName = branch.District.Name,
                Zone = branch.Zone,
                ParishName = branch.Parish.Name,
                Neighborhood = branch.Neighborhood,
                Direction = branch.MainStreet + " y " + branch.SecundaryStreet,
                Reference = branch.Reference,
                OwnerPhone = branch.PersonOwner.Phone,
                OwnerMobile = branch.PersonOwner.Mobile,
                BranchImages = GetBranchImages(branch),
                TaskCampaigns = GetTaskCampaigns(branch),
                BranchTypeBusiness = branch.TypeBusiness,
                Latitude = branch.LatitudeBranch.Replace(",", "."),
                Longitude = branch.LenghtBranch.Replace(",", "."),
                SmsListas = ListaSms
                

            };
        }

        private static List<BranchImagesProfileViewModel> GetBranchImages(Branch branch)
        {
            return branch.BranchImages
                .Select(i => new BranchImagesProfileViewModel()
                {
                    Id = i.Id,
                    Base64Image = i.UrlImage,
                    FileName = i.NameFile
                })
                .ToList();
        }

        private static List<BranchTaskCampaignsProfileViewModel> GetTaskCampaigns(Branch branch)
        {
            return branch.TaskCampaigns
                .Select(t => new BranchTaskCampaignsProfileViewModel()
                {
                    Id = t.Id,
                    MerchantName = t.Merchant.Profile.Name,
                    CampaignName = t.Campaign.Name,
                    StartDate = t.StartDate,
                    StatusName = t.StatusTask.Name,
                    CampaignServices = GetCampaignServices(t.Campaign.CampaignServices),
                    TaskCode = t.Code
                    ,Idcampaign=t.IdCampaign
                })
                .ToList();
        }

        private static List<BranchCampaignServicesProfileViewModel> GetCampaignServices(List<CampaignServices> campaignServices)
        {
            return campaignServices.
                Select(c => new BranchCampaignServicesProfileViewModel()
                {
                    Id = c.Id,
                    IdService = c.IdService,
                    ServiceName = c.Service.Name
                })
                .ToList();
        }

        public static BranchRegisterViewModel ToBranchRegisterViewModel(Branch branch)
        {

            if (branch ==null)
            {
                return new BranchRegisterViewModel();
            }

            return new BranchRegisterViewModel
            {
                Id = branch.Id,
                AdministratorCellPhone = branch.PersonAdministration.Mobile,
                AdministratorDocument = branch.PersonAdministration.Document,
                AdministratorName = branch.PersonAdministration.Name,
                AdministratorPhone = branch.PersonAdministration.Phone,
                AdministratorSurname = branch.PersonAdministration.SurName,
                IsAdministratorOwner = branch.IsAdministratorOwner == "SI",
                Name = branch.Name,
                Code = branch.Code,
                CreationDate = branch.CreationDate,
                Label = branch.Label,
                Latitude = branch.LatitudeBranch,
                Longitude = branch.LenghtBranch,
                ExternalCode = branch.ExternalCode,
                IdCountry = branch.IdCountry.ToString(),
                IdDistrict = branch.IdDistrict.ToString(),
                IdParish = branch.IdParish.ToString(),
                IdProvince = branch.IdProvince.ToString(),
                IdSector = branch.IdSector.ToString(),
                MainStreet = branch.MainStreet,
                Neighborhood = branch.Neighborhood,
                NumberBranch = branch.NumberBranch,
                OwnerName = branch.PersonOwner.Name,
                IdPersonAdministrator = branch.IdPersonAdministrator,
                IdPersonOwner = branch.IdPersonOwner,
                OwnerCellPhone = branch.PersonOwner.Mobile,
                OwnerDocument = branch.PersonOwner.Document,
                OwnerPhone = branch.PersonOwner.Phone,
                OwnerSurname = branch.PersonOwner.SurName,
                OwnerTypeDocument = branch.PersonOwner.TypeDocument,
                Reference = branch.Reference,
                SecundaryStreet = branch.SecundaryStreet,
                StatusRegister = branch.StatusRegister,
                Zone = branch.Zone,
                TypeBusiness = branch.TypeBusiness,
                BranchCustomers = branch.BranchCustomers.Select(c => new BranchCustomersViewModel()
                {
                    Id = c.Id,
                    IdCustomer = c.IdCustomer,
                    IdChannel = c.IdChannel,
                    IdTypeBusiness = c.IdTypeBusiness,
                    IdBranch = c.IdBranch,
                    NameChannel = c.Channel.Name,
                    NameCustomer = c.Customer.Name,
                    NameTypeBusiness = c.TypeBusiness.Name
                }).ToList()
            };
        }

        public static Branch FromBranchRegisterViewModel(BranchRegisterViewModel viewModel)
        {

            Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<BranchRegisterViewModel, Branch>()
                        .ForMember(dest => dest.IdParish, opt => opt.MapFrom(src => Guid.Parse(src.IdParish)))
                        .ForMember(dest => dest.IdCountry, opt => opt.MapFrom(src => Guid.Parse(src.IdCountry)))
                        .ForMember(dest => dest.IdDistrict, opt => opt.MapFrom(src => Guid.Parse(src.IdDistrict)))
                        .ForMember(dest => dest.IdProvince, opt => opt.MapFrom(src => Guid.Parse(src.IdProvince)))
                        .ForMember(dest => dest.IdSector, opt => opt.MapFrom(src => Guid.Parse(src.IdSector)))
                        .ForMember(dest => dest.IsAdministratorOwner, opt => opt.MapFrom(src => src.IsAdministratorOwner ? "SI" : "NO"))
                        .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                        .ForMember(dest => dest.LatitudeBranch, opt => opt.MapFrom(src => src.Latitude.ToString(CultureInfo.InvariantCulture)))
                        .ForMember(dest => dest.LenghtBranch, opt => opt.MapFrom(src => src.Longitude.ToString(CultureInfo.InvariantCulture)))
                        .ForMember(dest => dest.StatusRegister, opt => opt.MapFrom(src => src.StatusRegister ?? CStatusRegister.Active));
                    cfg.CreateMap<BranchRegisterViewModel, Person>()
                        .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.AdministratorDocument))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AdministratorName))
                        .ForMember(dest => dest.SurName, opt => opt.MapFrom(src => src.AdministratorSurname))
                        .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.AdministratorPhone))
                        .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.AdministratorCellPhone))
                        .ForMember(dest => dest.TypeDocument, opt => opt.MapFrom(src => "CI"));
                    cfg.CreateMap<BranchRegisterViewModel, Person>()
                        .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.OwnerDocument))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OwnerName))
                        .ForMember(dest => dest.SurName, opt => opt.MapFrom(src => src.OwnerSurname))
                        .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.OwnerPhone))
                        .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.OwnerCellPhone))
                        .ForMember(dest => dest.TypeDocument, opt => opt.MapFrom(src => src.OwnerTypeDocument ?? "CI"));
                    cfg.CreateMap<BranchCustomersViewModel, BranchCustomer>();
                }
            );

            var branch = Mapper.Map<BranchRegisterViewModel, Branch>(viewModel);
            branch.PersonAdministration = Mapper.Map<BranchRegisterViewModel, Person>(viewModel);
            branch.PersonOwner = Mapper.Map<BranchRegisterViewModel, Person>(viewModel);

            branch.CreationDate = branch.CreationDate.Year < 2010 ? DateTime.Now : branch.CreationDate;

            return branch;
        }

        public static Branch FromMyTaskViewModel(MyTaskViewModel model, Branch branch)
        {
            branch.Id = model.IdBranch;
            branch.Code = model.BranchMardisCode;
            branch.ExternalCode = model.BranchExternalCode;
            branch.Name = model.BranchName;
            branch.Label = model.BranchLabel;
            branch.Reference = model.BranchReference;
            branch.MainStreet = model.BranchMainStreet;
            branch.SecundaryStreet = model.BranchSecundaryStreet;

            return branch;
        }

    }
}
