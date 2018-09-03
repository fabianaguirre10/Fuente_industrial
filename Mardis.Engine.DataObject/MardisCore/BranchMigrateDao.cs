using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.Filter;
using Mardis.Engine.Web.ViewModel.BranchViewModels;
using Microsoft.EntityFrameworkCore;
using Mardis.Engine.DataObject.MardisSecurity;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class BranchMigrateDao : ADao
    {
        private readonly PersonDao _personDao;
        private readonly BranchDao _brachDao;
        private readonly UserDao _userDao;
        public BranchMigrateDao(MardisContext mardisContext) :
            base(mardisContext)
        {
            _personDao = new PersonDao(mardisContext);
            _userDao = new UserDao(mardisContext);
            _brachDao = new BranchDao(mardisContext);
            CoreFilterDao = new CoreFilterDao(mardisContext);
            CoreFilterDetailDao = new CoreFilterDetailDao(mardisContext);
        }

        public Guid GetProviceByName(string provice)
        {
            try
            {
                var itemReturn = Context.Provinces.Where(x => x.Name == provice);
                var idprovice = itemReturn.Count() > 0 ? itemReturn.First().Id : Guid.Parse("00000000-0000-0000-0000-000000000000");
                return idprovice;
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar provincias" + e.ToString());
            }

        }
        public Guid GetDistrictByName(string district, Guid idprovince)
        {
            try
            {
                var itemReturn = Context.Districts.Where(x => x.Name == district && x.IdProvince == idprovince);
                var iddistrict = itemReturn.Count() > 0 ? itemReturn.First().Id : Guid.Parse("00000000-0000-0000-0000-000000000000");
                return iddistrict;
            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {
                throw new Exception("Error al consultar Cuidad");
            }

        }
        public Guid GetParishByName(string parish, Guid idDistrict)
        {
            try
            {
                var itemReturn = Context.Parishes.Where(x => x.Name == parish && x.IdDistrict == idDistrict);
                var idParishes = itemReturn.Count() > 0 ? itemReturn.First().Id : Guid.Parse("00000000-0000-0000-0000-000000000000");
                return idParishes;
            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {
                throw new Exception("Error al consultar Parroquias");
            }

        }
        public Guid GetSectorByName(string sector, Guid idDistrict)
        {
            try
            {
                var itemReturn = Context.Sectors.Where(x => x.Name == sector && x.IdDistrict == idDistrict);
                var idSectors = itemReturn.Count() > 0 ? itemReturn.First().Id : Guid.Parse("00000000-0000-0000-0000-000000000000");
                return idSectors;
            }

            catch (Exception e)

            {
                throw new Exception("Error al consultar Sectores");
            }

        }
        /// <summary>
        /// Clase para guardar información que proviene de un archivo excel
        /// </summary>
        /// <param name="branchPerson"> El parametero tiene informacion de persona y local</param>
        /// <returns></returns>
//        public  bool SaveBranchMigrate(IList<CBranch> branchPerson, Guid idAccount, Guid idcampaing)
//        {
//            bool status = false;
//            Branch branch = null;
//            Person person = null;
//#pragma warning disable CS0219 // La variable 'task' está asignada pero su valor nunca se usa
//            TaskCampaign task = null;
//#pragma warning restore CS0219 // La variable 'task' está asignada pero su valor nunca se usa
//            try
//            {

            
//            foreach (var item in branchPerson.Where(x => x.Code != "NA"))
//            {

//                using (var transaction = Context.Database.BeginTransaction())
//                {

//                    try
//                    {
//                        var Getbrach = _brachDao.GetBranchByExternalCode(item.Code, idAccount);


//                        if (Getbrach == null)
//                        {
//                            var stateRegister = EntityState.Added;


//                            person = new Person();
//                            var ci = item.Document == null ? item.Code : item.Document;
//                            person.Code = item.Code;
//                            person.Name = item.PersonName;
//                            person.Phone = item.phone;
//                            person.Mobile = item.Mobil;
//                            person.Document = ci;
//                            person.SurName = "";
//                            person.TypeDocument = "CI";
//                            person.StatusRegister = "A";
//                            person.IdAccount = idAccount;
                     

            
                         

//                            //Context.SaveChanges();
//                            //Guid idperson = person.Id;
//                            branch = new Branch();
                               
//                            branch.Code = item.Code;
//                            branch.ExternalCode = item.Code;
//                            branch.IdCountry = Guid.Parse("BE7CF5FF-296B-464D-82FA-EF0B4F48721B");// Pais ecuador
//                            branch.IdProvince = item.IdProvice;
//                            branch.IdDistrict = item.IdDistrict;
//                            branch.IdParish = item.IdParish;
//                            branch.IdSector = item.IdSector;
//                            branch.Name = item.BranchName;
//                            branch.Label = item.BranchName;
//                            branch.LatitudeBranch = item.LatitudeBranch;
//                            branch.LenghtBranch = item.LenghtBranch;
//                            branch.MainStreet = item.BranchStreet;
//                            branch.Reference = item.BranchReference;
//                            branch.Zone = "";
//                            branch.TypeBusiness = item.BranchType;
//                            branch.Neighborhood = "-";
//                            branch.SecundaryStreet = "-";
//                            branch.NumberBranch = "-";
//                            branch.IdPersonAdministrator = person.Id;
//                            branch.IdPersonOwner = person.Id;
//                                branch.IsAdministratorOwner = "SI";
//                            branch.IdAccount = idAccount;
//                            branch.IMEI_ID = item.IMEI;
//                            branch.RUTAAGGREGATE = item.Rute;
//                            branch.ESTADOAGGREGATE = "S";
//                            branch.routeDate = DateTime.Now;
//                            //Context.Branches.Add(branch);
                    
//                             branch.PersonAdministration.IdAccount=
//                                Context.Persons.Add(person);
//                                Context.Entry(person).State = stateRegister;
//                                Context.SaveChanges();
//                                //task = new TaskCampaign();
//                                //task.Code = item.Code;
//                                //task.Description = "Agregada Para Gestión de Rutas";
//                                //task.ExternalCode = item.Code;
//                                //task.IdAccount = idAccount;
//                                //task.IdCampaign = idcampaing;
//                                //task.IdBranch = branch.Id;
//                                //task.IdMerchant = _userDao.GetMerchants(idAccount).First().Id;
//                                //task.IdStatusTask = Guid.Parse("7B0D0269-1AEF-4B73-9089-20E53698FF75");
//                                //task.StatusRegister = "A";
//                                //task.Route = item.Rute;

//                                //Context.TaskCampaigns.Add(task);
//                                //Context.Entry(task).State = stateRegister;
//                                //Context.SaveChanges();


//                                transaction.Commit();
//                        }
//                        else
//                        {
//                            var stateRegister = Guid.Empty == Getbrach.Id ? EntityState.Added : EntityState.Modified;


//                            Getbrach.IMEI_ID = item.IMEI;
//                            Getbrach.RUTAAGGREGATE = item.Rute;
//                            Getbrach.MainStreet = item.BranchStreet;
//                            Getbrach.Name = item.BranchName;
//                            Getbrach.Label = item.BranchName;
//                            Getbrach.Reference = item.BranchReference;
//                            Getbrach.LenghtBranch = item.LenghtBranch;
//                            Getbrach.LatitudeBranch = item.LatitudeBranch;
//                            Getbrach.ESTADOAGGREGATE = "S";
//                            Getbrach.routeDate = DateTime.Now;
//                            Context.Branches.Add(Getbrach);
//                            Context.Entry(Getbrach).State = stateRegister;
//                            Context.SaveChanges();

//                            //task = new TaskCampaign();
//                            //task.Code = item.Code;
//                            //task.Description = "Agregada Para Gestión de Rutas";
//                            //task.ExternalCode = item.Code;
//                            //task.IdAccount = idAccount;
//                            //task.IdCampaign = idcampaing;
//                            //task.IdBranch = Getbrach.Id;
//                            //task.IdMerchant = _userDao.GetMerchants(idAccount).First().Id;
//                            //task.IdStatusTask = Guid.Parse("7B0D0269-1AEF-4B73-9089-20E53698FF75");
//                            //task.StatusRegister = "A";
//                            //task.Route = item.Rute;

//                            //Context.TaskCampaigns.Add(task);

//                            //Context.Entry(task).State = EntityState.Modified;

//                            //Context.Entry(task).State = EntityState.Added;

//                            //Context.SaveChanges();



//                            transaction.Commit();
//                        }
                      
//                    }
                
//                    catch (Exception ex)

//                    {
//                        transaction.Rollback();
//                        return false;
//                    }
//                    finally{
//                        status = true;


//                    }
//                }
//            }
//            }
//            catch (Exception)
//            {
//                return false;
//                throw;
//            }
//            finally
//            {
//                status=true;
//            }
//            return status;

//        }
//    }
//}


public bool SaveBranchMigrate(IList<Branch> branchPerson, Guid idAccount, Guid idcampaing)
{
    bool status = false;
    Branch branch = null;
    Person person = null;
#pragma warning disable CS0219 // La variable 'task' está asignada pero su valor nunca se usa
    TaskCampaign task = null;
#pragma warning restore CS0219 // La variable 'task' está asignada pero su valor nunca se usa
    try
    {




                    var _dataUpdate = branchPerson.Where(x => x.Id != Guid.Parse("00000000-0000-0000-0000-000000000000"));
                    if (_dataUpdate.Count() > 0)
                    {
                        Context.Branches.UpdateRange(_dataUpdate);
                        Context.SaveChanges();
                    

                    }
                    var _datainsert = branchPerson.Where(x => x.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"));
                    if (_datainsert.Count() > 0)
                    {
                        Context.Branches.AddRange(_datainsert);
                        Context.SaveChanges();


                    }

                   

                    return true;
               
        
    }
    catch (Exception e)
    {
        return false;
        throw;
    }
    finally
    {
        status = true;
    }

}

        public Branch GetLocal(string code, Guid idAccount) {

            try
            {
                return Context.Branches.Include(x=>x.PersonOwner).Where(x => x.Code.Equals(code) && x.IdAccount.Equals(idAccount)).First();
            }
            catch (Exception)
            {

                return new Branch();
            }

        }
    }
}
