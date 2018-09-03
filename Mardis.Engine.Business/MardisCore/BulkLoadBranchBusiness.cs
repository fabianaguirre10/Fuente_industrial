using System;
using System.Collections.Generic;
using System.IO;
using Mardis.Engine.Business.MardisCommon;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Framework.Resources.TypesBulkLoad;
using Mardis.Engine.Web.ViewModel.BranchViewModels;

namespace Mardis.Engine.Business.MardisCore
{
    public class BulkLoadBranchBusiness : ABulkBusiness
    {
        private BulkLoadBusiness _bulkLoadBusiness;
        private BranchDao _branchDao;
        private ProvinceDao _provinceDao;
        private DistrictDao _districtDao;
        private ParishDao _parishDao;
        private PersonDao _personaDao;
        private SectorDao _sectorDao;
        private BulkLoadStatusDao _bulkLoadStatusDao;

        private readonly List<string> _lstItemsSuccess = new List<string>();
        private readonly List<string> _lstItemsError = new List<string>();

        public string CharacteristicBulk { get; set; }

        public Guid IdProcess { get; set; }

        public byte[] BufferFile { get; set; }

        public Guid IdAccount { get; set; }

        public BranchLoadViewModel ConvertViewModel(string line, char separator)
        {
            var returnValue = new BranchLoadViewModel();

            try
            {
                var lineSeparator = line.Split(separator);

                returnValue.ExternalCode = lineSeparator[CBulkLoadBranch.PositionExternalCode]?.Trim();
                returnValue.Name = lineSeparator[CBulkLoadBranch.PositionName]?.Trim();
                returnValue.Label = lineSeparator[CBulkLoadBranch.PositionLabel]?.Trim();
                returnValue.Contry = lineSeparator[CBulkLoadBranch.PositionCountry]?.Trim();
                returnValue.Province = lineSeparator[CBulkLoadBranch.PositionProvince]?.Trim();
                returnValue.District = lineSeparator[CBulkLoadBranch.PositionDistrict]?.Trim();
                returnValue.Parish = lineSeparator[CBulkLoadBranch.PositionParish]?.Trim();
                returnValue.Sector = lineSeparator[CBulkLoadBranch.PositionSector]?.Trim();
                returnValue.Zone = lineSeparator[CBulkLoadBranch.PositionZone]?.Trim();
                returnValue.Neighborhood = lineSeparator[CBulkLoadBranch.PositionNeighborhood]?.Trim();
                returnValue.MainStreet = lineSeparator[CBulkLoadBranch.PositionMainStreet]?.Trim();
                returnValue.SecundaryStreet = lineSeparator[CBulkLoadBranch.PositionSecundaryStreet]?.Trim();
                returnValue.NumberBranch = lineSeparator[CBulkLoadBranch.PositionNumberBranch]?.Trim();
                returnValue.Latitude = lineSeparator[CBulkLoadBranch.PositionLatitud]?.Trim();
                returnValue.Lenght = lineSeparator[CBulkLoadBranch.PositionLenght]?.Trim();
                returnValue.Reference = lineSeparator[CBulkLoadBranch.PositionReference]?.Trim();
                returnValue.PersonOwnerCode = lineSeparator[CBulkLoadBranch.PositionPersonOwnerCode]?.Trim();
                returnValue.PersonOwnerName = lineSeparator[CBulkLoadBranch.PositionPersonOwnerName]?.Trim();
                returnValue.PersonOwnerSurname = lineSeparator[CBulkLoadBranch.PositionPersonOwnerSurname]?.Trim();
                returnValue.PersonOwnerType = lineSeparator[CBulkLoadBranch.PositionPersonOwnerType]?.Trim();
                returnValue.PersonOwnerDocument = lineSeparator[CBulkLoadBranch.PositionPersonOwnerDocument]?.Trim();
                returnValue.PersonOwnerPhone = lineSeparator[CBulkLoadBranch.PositionPersonOwnerPhone]?.Trim();
                returnValue.PersonOwnerMobile = lineSeparator[CBulkLoadBranch.PositionPersonOwnerMobile]?.Trim();
                returnValue.IsPersonAdministrator = lineSeparator[CBulkLoadBranch.PositionIsPersonAdministrator]?.Trim();
                returnValue.PersonAdminCode = lineSeparator[CBulkLoadBranch.PositionPersonAdminCode]?.Trim();
                returnValue.PersonAdminName = lineSeparator[CBulkLoadBranch.PositionPersonAdminName]?.Trim();
                returnValue.PersonAdminSurname = lineSeparator[CBulkLoadBranch.PositionPersonAdminSurname]?.Trim();
                returnValue.PersonAdminType = lineSeparator[CBulkLoadBranch.PositionPersonAdminType]?.Trim();
                returnValue.PersonAdminDocument = lineSeparator[CBulkLoadBranch.PositionPersonAdminDocument]?.Trim();
                returnValue.PersonAdminPhone = lineSeparator[CBulkLoadBranch.PositionPersonAdminPhone]?.Trim();
                returnValue.PersonAdminMobile = lineSeparator[CBulkLoadBranch.PositionPersonAdminMobile]?.Trim();

            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }

        public BulkLoadBranchBusiness(
                                      string connectionString,
                                      string characteristicBulk,
                                      Guid idAccount,
                                      Guid idProcess, 
                                      byte[] bufferFile) 
            : base(connectionString)
        {


            CharacteristicBulk = characteristicBulk;
            IdProcess = idProcess;
            BufferFile = bufferFile;
            IdAccount = idAccount;
        }

        public void InitProcess()
        {
            var numberLine = 0;

            var fileContent = new StreamReader(new MemoryStream(BufferFile));

            using (var mardisContext = MardisContext)
            {
                _bulkLoadBusiness = new BulkLoadBusiness(mardisContext, null);
                _branchDao = new BranchDao(mardisContext);
                _provinceDao = new ProvinceDao(mardisContext);
                _districtDao = new DistrictDao(mardisContext);
                _parishDao = new ParishDao(mardisContext);
                _personaDao = new PersonDao(mardisContext);
                _sectorDao = new SectorDao(mardisContext);
                _bulkLoadStatusDao = new BulkLoadStatusDao(mardisContext);

                var oneProcess = _bulkLoadBusiness.GetOne(IdProcess);
                var separatorChar = Convert.ToChar(oneProcess.BulkLoadCatalog.Separator);

                string line;
                while ((line = fileContent.ReadLine()) != null)
                {

                    if (0 == numberLine)
                    {
                        //primera fila es cabecera
                        numberLine++;
                        continue;
                    }


                    //comienza transacción
                    oneProcess.BulkLoadStatus = _bulkLoadStatusDao.GetOneByCode(CBulkLoad.StateBulk.EnProceso);
                    oneProcess.IdBulkLoadStatus = oneProcess.BulkLoadStatus.Id;

                    mardisContext.SaveChanges();

                    var addCurrent = 0;
                    var updateCurrent = 0;
                    var deleteCurrent = 0;
                    var lineResult = line;
                    var errorLine = string.Empty;

                    using (var transaction = mardisContext.Database.BeginTransaction())
                    {

                        try
                        {
                            var valuesLine = line.Split(separatorChar);
                            var branchViewModel = ConvertViewModel(line, separatorChar);

                            var branchTemp = _branchDao.GetBranchByExternalCode(branchViewModel.ExternalCode,
                                                                               IdAccount);
                            var updateFields = false;

                            if (null == branchTemp)
                            {
                                //significa que es nuevo
                                updateFields = true;

                                branchTemp = new Branch
                                {
                                    Code = branchViewModel.ExternalCode,
                                    ExternalCode = branchViewModel.ExternalCode,
                                    IdAccount = IdAccount
                                };


                                addCurrent++;
                            }
                            else
                            {

                                if (CBulkLoad.CharacteristicBulk.ConsevarYAdicionar.Equals(CharacteristicBulk))
                                {
                                    lineResult += separatorChar + "Local conservado por configuración";
                                    _lstItemsSuccess.Add(lineResult);
                                }
                                else if (CBulkLoad.CharacteristicBulk.ActualizarYAdicionar.Equals(CharacteristicBulk))
                                {
                                    updateFields = true;
                                }

                                updateCurrent++;
                            }

                            if (updateFields)
                            {
                                branchTemp.Name = branchViewModel.Name;
                                branchTemp.Label = branchViewModel.Label;

                                var oneCountry = new Country();// _countryDao.GetCountryByCode(branchViewModel.Contry);
                                var oneProvince = _provinceDao.GetProvinceByCode(branchViewModel.Province);
                                var oneDistrinct = _districtDao.GetDistrinctByCode(branchViewModel.District);
                                var oneParish = _parishDao.GetParishByCode(branchViewModel.Parish);
                                var onePersonOwner = _personaDao.GetPersonByCode(branchViewModel.PersonOwnerCode);
                                var oneSector = _sectorDao.GetByCode(branchViewModel.Sector);

                                if (null != oneCountry)
                                {
                                    branchTemp.Country = oneCountry;
                                    branchTemp.IdCountry = oneCountry.Id;
                                }
                                else
                                {
                                    errorLine += "Ciudad:" + branchViewModel.Contry + " no existe,";
                                }

                                if (null != oneProvince)
                                {
                                    branchTemp.Province = oneProvince;
                                    branchTemp.IdProvince = oneProvince.Id;
                                }
                                else
                                {
                                    errorLine += "Provincia:" + branchViewModel.Province + " no existe,";
                                }

                                if (null != oneDistrinct)
                                {
                                    branchTemp.District = oneDistrinct;
                                    branchTemp.IdDistrict = oneDistrinct.Id;
                                }
                                else
                                {
                                    errorLine += "Distrito:" + branchViewModel.District + " no existe,";
                                }

                                if (null != oneSector)
                                {
                                    branchTemp.Sector = oneSector;
                                    branchTemp.IdSector = oneSector.Id;
                                }
                                else
                                {
                                    errorLine += "Sector:" + branchViewModel.Sector + " no existe,";
                                }

                                if (null != oneParish)
                                {
                                    branchTemp.Parish = oneParish;
                                    branchTemp.IdParish = oneParish.Id;
                                }
                                else
                                {
                                    errorLine += "Parroquia:" + branchViewModel.Parish + " no existe,";
                                }

                                branchTemp.Zone = branchViewModel.Zone;
                                branchTemp.Neighborhood = branchViewModel.Neighborhood;
                                branchTemp.MainStreet = branchViewModel.MainStreet;
                                branchTemp.SecundaryStreet = branchViewModel.SecundaryStreet;
                                branchTemp.NumberBranch = branchViewModel.NumberBranch;
                                branchTemp.LatitudeBranch = branchViewModel.Latitude;
                                branchTemp.LenghtBranch = branchViewModel.Lenght;
                                branchTemp.Reference = branchViewModel.Reference;

                                if (null == onePersonOwner)
                                {
                                    onePersonOwner = new Person { IdAccount = IdAccount };

                                }

                                onePersonOwner.Code = branchViewModel.PersonOwnerCode;
                                onePersonOwner.Name = branchViewModel.PersonOwnerName;
                                onePersonOwner.SurName = branchViewModel.PersonOwnerSurname;
                                onePersonOwner.TypeDocument = branchViewModel.PersonOwnerType;
                                onePersonOwner.Document = branchViewModel.PersonOwnerDocument;
                                onePersonOwner.Phone = branchViewModel.PersonOwnerPhone;
                                onePersonOwner.Mobile = branchViewModel.PersonOwnerMobile;

                                if (onePersonOwner.Id == Guid.Empty)
                                {
                                    mardisContext.Persons.Add(onePersonOwner);
                                }

                                mardisContext.SaveChanges();

                                branchTemp.PersonOwner = onePersonOwner;
                                branchTemp.IdPersonOwner = onePersonOwner.Id;

                                branchTemp.IsAdministratorOwner = branchViewModel.IsPersonAdministrator.ToUpper();

                                if (CBranch.Yes.Equals(branchViewModel.IsPersonAdministrator.ToUpper()))
                                {
                                    branchTemp.IdPersonAdministrator = null;
                                    branchTemp.PersonAdministration = null;
                                }
                                else if (CBranch.No.Equals(branchViewModel.IsPersonAdministrator.ToUpper()))
                                {
                                    var onePersonAdmin = _personaDao.GetPersonByCode(branchViewModel.PersonAdminCode);

                                    if (null == onePersonAdmin)
                                    {
                                        onePersonAdmin = new Person();

                                        mardisContext.Persons.Add(onePersonAdmin);
                                    }

                                    onePersonAdmin.Code = branchViewModel.PersonAdminCode;
                                    onePersonAdmin.Name = branchViewModel.PersonAdminName;
                                    onePersonAdmin.SurName = branchViewModel.PersonAdminSurname;
                                    onePersonAdmin.TypeDocument = branchViewModel.PersonAdminType;
                                    onePersonAdmin.Document = branchViewModel.PersonAdminDocument;
                                    onePersonAdmin.Phone = branchViewModel.PersonAdminPhone;
                                    onePersonAdmin.Mobile = branchViewModel.PersonAdminMobile;
                                    onePersonAdmin.IdAccount = IdAccount;

                                    if (onePersonAdmin.Id == Guid.Empty)
                                    {
                                        mardisContext.Persons.Add(onePersonAdmin);
                                    }

                                    mardisContext.SaveChanges();

                                    branchTemp.IdPersonAdministrator = onePersonAdmin.Id;
                                    branchTemp.PersonAdministration = onePersonAdmin;
                                }
                                else
                                {
                                    errorLine += "No se entiende si es o no Adminitrador use SI o NO,";
                                }

                            }

                            if (!string.IsNullOrEmpty(errorLine))
                            {
                                throw new Exception(errorLine);
                            }


                            if (branchTemp.Id == Guid.Empty)
                            {
                                mardisContext.Branches.Add(branchTemp);
                            }

                            mardisContext.SaveChanges();

                            transaction.Commit();

                            _lstItemsSuccess.Add(lineResult);

                        }
                        catch (Exception ex)
                        {
                            addCurrent = 0;
                            updateCurrent = 0;
                            deleteCurrent = 1;

                            transaction.Rollback();

                            lineResult += separatorChar + "Error linea " + numberLine + " , mensaje: " + ex.Message;
                            _lstItemsError.Add(lineResult);
                        }

                        numberLine++;
                    }

                    oneProcess.CurrentFile = numberLine;
                    oneProcess.TotalAdded += addCurrent;
                    oneProcess.TotalUpdated += updateCurrent;
                    oneProcess.TotalFailed += deleteCurrent;

                    mardisContext.SaveChanges();
                }

                oneProcess.BulkLoadStatus = _bulkLoadStatusDao.GetOneByCode(CBulkLoad.StateBulk.Aceptado);
                oneProcess.IdBulkLoadStatus = oneProcess.BulkLoadStatus.Id;

                mardisContext.SaveChanges();
            }
        }

    }
}
