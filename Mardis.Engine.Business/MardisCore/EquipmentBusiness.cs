using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel.BranchViewModels;
using Mardis.Engine.Web.ViewModel.EquipmentViewModels;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class EquipmentBusiness : ABusiness
    {
        private readonly BranchDao _branchDao;
        private readonly SequenceBusiness _sequenceBusiness;
        private readonly PersonDao _personDao;
        private readonly BranchCustomerDao _branchCustomerDao;
        private readonly TaskCampaignDao _taskCampaignDao;
        private readonly EquipmentDao _equipmentDao;
        private readonly CampaignDao _CampaignDao;
        public EquipmentBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _branchDao = new BranchDao(mardisContext);
            _sequenceBusiness = new SequenceBusiness(mardisContext);
            _personDao = new PersonDao(mardisContext);
            _branchCustomerDao = new BranchCustomerDao(mardisContext);
            _taskCampaignDao = new TaskCampaignDao(mardisContext);
            _equipmentDao = new EquipmentDao(mardisContext);
            _CampaignDao = new CampaignDao(mardisContext);
        }

        public EquipmentListViewModel GetPaginatedEquipments(List<FilterValue> filterValues, int pageSize, int pageIndex, Guid idAccount)
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Equipament, EquipmentItemViewModel>();
            });
            var itemResult = new EquipmentListViewModel();
            var equipments = _equipmentDao.GetPaginatedEquipmentList(filterValues, pageSize, pageIndex, idAccount);
            var countequipments = _equipmentDao.GetPaginatedEquipmentsCount(filterValues, pageSize, pageIndex, idAccount);

            itemResult.EquipmentList = Mapper.Map<List<EquipmentItemViewModel>>(equipments);

            return ConfigurePagination(itemResult, pageIndex, pageSize, filterValues, countequipments);

        }
        public EquipmentRegisterViewModel GetEquipment(int Id, Guid Idaccount)
        {

            var model = new EquipmentRegisterViewModel();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Equipament, EquipmentRegisterViewModel>();
            });
            var equipmentsmodel = new Equipament();
            equipmentsmodel = _equipmentDao.GetEquipament_Edit(Id);
            model = Mapper.Map<EquipmentRegisterViewModel>(equipmentsmodel);

            if (_branchDao.GetOne(model.Idbranch, Idaccount) != null)
                model.BranchName = _branchDao.GetOne(model.Idbranch, Idaccount).Name;

            return model;

        }
        public List<Equipament> GetEquipamentFotos(int codigo)
        {
            return _equipmentDao.GetEquipamentFotos(codigo);
        }
        public EquipmentRegisterViewModel GetEquipment_Profile(int Id, Guid Idaccount)
        {

            var model = new EquipmentRegisterViewModel();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Equipament, EquipmentRegisterViewModel>();
            });
            var equipmentsmodel = new Equipament();
            equipmentsmodel = _equipmentDao.GetEquipamentProfile(Id);
            model = Mapper.Map<EquipmentRegisterViewModel>(equipmentsmodel);
         
            if (_branchDao.GetOne(model.Idbranch, Idaccount) != null)
                model.BranchName = _branchDao.GetOne(model.Idbranch, Idaccount).Name;
            if (model.Branches != null) { 
            if (model.Branches.TaskCampaigns.Count()>0)
            {
                foreach (var item in model.Branches.TaskCampaigns) {

                   var camp= _CampaignDao.GetOne(item.IdCampaign ,Idaccount);
                    model.Branches.TaskCampaigns.Where(x => x.IdCampaign == item.IdCampaign).First().Campaign = camp;
                }

            }
            }

            return model;

        }
        public Equipament SaveEquipment(EquipmentRegisterViewModel models, Guid idaccout)
        {

            var model = new EquipmentRegisterViewModel();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<EquipmentRegisterViewModel, Equipament>();
            });
            var equipmentsmodel = new Equipament();

            equipmentsmodel = Mapper.Map<Equipament>(models);
            equipmentsmodel.IdAccount = idaccout;
            var a = _equipmentDao.SaveEquipment(equipmentsmodel);

            return null;



        }
        public int DeleteEquipment(int ID)
        {


            var a = _equipmentDao.DeleteEquipment(ID);

            return 0;

        }
        #region Metodos Comunes
        public List<Equipament_type> GetUserListByType()
        {
            var userList = _equipmentDao.GetUserListByType();

            //foreach (var user in userList)
            //{
            //    user.Profile = _profileDao.GetById(user.IdProfile);
            //}

            return userList;
        }
        public List<Equipament_status> GetUserListBystatus()
        {
            var userList = _equipmentDao.GetUserListBystatus();

            //foreach (var user in userList)
            //{
            //    user.Profile = _profileDao.GetById(user.IdProfile);
            //}

            return userList;
        }
        #endregion
    }
}
