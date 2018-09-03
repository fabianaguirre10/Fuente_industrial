using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Mardis.Engine.Web.Libraries.Services
{

    /// <summary>
    /// Controlador de Servicios de Filtros
    /// </summary>
    [Authorize]
    public class ServicesFilterController : AController<ServicesFilterController>
    {
        readonly FilterExecutionBusiness _filterExecutionBusiness;
        readonly FilterExecutionDetailBusiness _filterExecutionDetailBusiness;
        readonly FilterControllerBusiness _filterControllerBusiness;
        readonly FilterTableBusiness _filterTableBusiness;
        readonly FilterFieldsBusiness _filterFieldsBusiness;
        readonly TypeFilterBusiness _typeFilterBusiness;
        readonly FilterCriteriaBusiness _filterCriteriaBusiness;
        readonly ILogger<ServicesFilterController> _logger;

        public ServicesFilterController(UserManager<ApplicationUser> userManager,
                                       IHttpContextAccessor httpContextAccessor,
                                       MardisContext mardisContext,
                                       ILogger<ServicesFilterController> logger)
                                      : base(userManager, httpContextAccessor, mardisContext, logger)
        {

            _filterExecutionBusiness = new FilterExecutionBusiness(mardisContext);
            _filterExecutionDetailBusiness = new FilterExecutionDetailBusiness(mardisContext);
            _filterControllerBusiness = new FilterControllerBusiness(mardisContext);
            _filterTableBusiness = new FilterTableBusiness(mardisContext);
            _filterFieldsBusiness = new FilterFieldsBusiness(mardisContext);
            _typeFilterBusiness = new TypeFilterBusiness(mardisContext);
            _filterCriteriaBusiness = new FilterCriteriaBusiness(mardisContext);
            _logger = logger;

        }

        /// <summary>
        /// Dame Ejecución Filtro o Grabalo
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        [HttpPost]
        public FilterExecution GetOrInsertFilterExecution(string controller)
        {
            FilterExecution filterExecution = null;
            Guid idUser = new Guid(base.ApplicationUserCurrent.UserId);

            filterExecution = _filterExecutionBusiness.GetFilterExecution(controller, idUser);


            if (null == filterExecution)
            {

                var filterController = _filterControllerBusiness.GetControllerById(controller);

                if (null != filterController)
                {
                    try
                    {
                        filterExecution = new FilterExecution();

                        filterExecution.IdUser = idUser;

                        filterExecution.FilterControllers = filterController;
                        filterExecution.IdFilterController = filterController.Id;

                        _filterExecutionBusiness.InsertOrUpdate(filterExecution);
                    }
                    catch (ExceptionMardis ex)
                    {
                        //logg de aplicacion
                        _logger.LogError(ex.Message);
                        filterExecution = null;
                    }
                }
                else
                {
                    //logg de aplicacion
                }

            }
            else
            {
                filterExecution.FilterExecutionDetails = _filterExecutionDetailBusiness.GetExecutionDetailByExecution(filterExecution.Id);
            }

            return filterExecution;
        }



        /// <summary>
        /// Dame Filtros de Tablas
        /// </summary>
        /// <param name="idFilterController"></param>
        /// <returns></returns>
        [HttpGet]
        public List<FilterTable> GetFilterTable(Guid idFilterController)
        {
            List<FilterTable> itemFilterTable = new List<FilterTable>();

            itemFilterTable = _filterTableBusiness.GetVisiblesFilterTable(idFilterController);

            return itemFilterTable;
        }

        /// <summary>
        /// Dame Campos de la Tablas
        /// </summary>
        /// <param name="IdFilterTable"></param>
        /// <returns></returns>
        [HttpPost]
        public List<FilterField> GetFilterFieldByTable(Guid IdFilterTable)
        {

            var itemFilterField = _filterFieldsBusiness.GetFilterVisibleFieldByTable(IdFilterTable);

            return itemFilterField;
        }


        /// <summary>
        /// Dame Tipos de Filtros por Campo
        /// </summary>
        /// <param name="IdFilterField"></param>
        /// <returns></returns>
        [HttpPost]
        public List<TypeFilter> GetTypeFilterByField(Guid IdFilterField)
        {
            var itemReturn = _typeFilterBusiness.GetTypeFilterByField(IdFilterField);

            return itemReturn;
        }


        /// <summary>
        /// Guardar Detalle de búsqueda
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<FilterExecutionDetail> SaveFilterDetail(FilterViewModel filter)
        {

            var filterExecutionDetail = new FilterExecutionDetail();
            var dateNow = DateTime.Now;

            try
            {
                var filterCriteria = _filterCriteriaBusiness
                                                          .GetFilterCriteria(new Guid(filter.IdFilterField),
                                                                             new Guid(filter.IdFilterCriteria));

                filterExecutionDetail.IdFilterExecution = new Guid(filter.IdFilterExecution);
                filterExecutionDetail.FilterCriteria = filterCriteria;
                filterExecutionDetail.IdFilterCriteria = filterCriteria.Id;
                filterExecutionDetail.Value = filter.FilterValue;
                filterExecutionDetail.CreationFilter = dateNow;

                _filterExecutionDetailBusiness.InsertOrUpdate(filterExecutionDetail);
            }
            catch (ExceptionMardis ex)
            {
                _logger.LogError(ex.Message);
            }

            return await Task.FromResult(filterExecutionDetail);
        }

        /// <summary>
        /// Borrar Filtro de Detalle
        /// </summary>
        /// <param name="idFilterExecutionDetail"></param>
        [HttpPost]
        public void DeleteFilterDetail(string idFilterExecutionDetail)
        {

            FilterExecutionDetail filterExecutionDetail =
                                  _filterExecutionDetailBusiness.GetOne(new Guid(idFilterExecutionDetail));

            if (null != filterExecutionDetail)
            {

                _filterExecutionDetailBusiness.PhysicalDelete(filterExecutionDetail);
            }

        }


        /// <summary>
        /// IsDynamic Builder for Filters
        /// </summary>
        /// <param name="idFilterExecution"></param>
        [HttpGet]
        public List<dynamic> GetResults(string idFilterExecution, string propertyName, string propertyValue)
        {

            Guid idAccount = base.ApplicationUserCurrent.AccountId;
            FilterExecution filterExecution = _filterExecutionBusiness.GetFilterExecution(new Guid(idFilterExecution));
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlReference = new StringBuilder();
            string referencesTablesSelect = "";
            var retObject = new List<dynamic>();


            if (null != filterExecution)
            {
                filterExecution.FilterExecutionDetails =
                            _filterExecutionDetailBusiness.GetExecutionDetailByExecution(filterExecution.Id);

                filterExecution.DateInit = DateTime.Now;

                string nameTableBase = filterExecution.FilterControllers.NameTable;
                int numberParameter = 0;



                List<FilterTable> itemTables = _filterTableBusiness.GetFilterTable(filterExecution.FilterControllers.Id);

                foreach (var itemReferences in itemTables)
                {
                    if (CFilter.AffirmationHasRelation.Equals(itemReferences.HasRelation))
                    {
                        string tableReference = itemReferences.TableInitial;

                        List<FilterField> lstItemFilterField =
                                                  _filterFieldsBusiness.GetFilterFieldByTable(itemReferences.Id);

                        foreach (var tempField in lstItemFilterField)
                        {
                            referencesTablesSelect += "," + itemReferences.TableInitial + "." + tempField.Field + " as " + itemReferences.TableInitial + tempField.Field;
                        }

                        var aliasTable = "";
                        if (itemReferences.FieldMainTable.IndexOf(".", StringComparison.Ordinal) < 0)
                        {
                            aliasTable = "tb.";
                        }

                        sqlReference.Append(" INNER JOIN ").Append(itemReferences.TableRelation).Append(" as ").Append(tableReference);
                        sqlReference.Append(" ON ").Append(aliasTable).Append(itemReferences.FieldMainTable)
                                          .Append(" = ").Append(tableReference).Append(".")
                                          .Append(itemReferences.FieldRelationTable);

                    }

                }

                sql.Append(" SELECT tb.*")
                    .Append(referencesTablesSelect)
                    .Append(" ")
                    .Append(" FROM ").Append(nameTableBase)
                    .Append(" as tb ");


                sql.Append(sqlReference);
                sql.Append(" WHERE 1=1 ");

                if (!(string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(propertyValue)))
                {
                    sql.Append(" and tb.")
                        .Append(propertyName)
                        .Append(" = '")
                        .Append(propertyValue)
                        .Append("' ");
                }

                using (var cmd = Context.Database.GetDbConnection().CreateCommand())
                {

                    if (CFilter.AffirmationHasRelation.Equals(filterExecution.FilterControllers.HasStatus))
                    {
                        sql.Append(UtilSqlBuilder.ConditionEqual("tb", "StatusRegister", CStatusRegister.Active, numberParameter++, cmd));
                    }

                    if (CFilter.AffirmationHasRelation.Equals(filterExecution.FilterControllers.HasAccount))
                    {
                        sql.Append(UtilSqlBuilder.ConditionEqual("tb", "IdAccount", idAccount, numberParameter++, cmd));
                    }

                    foreach (var itemReferences in itemTables)
                    {
                        if (CFilter.AffirmationHasRelation.Equals(itemReferences.HasRelation))
                        {
                            if (CFilter.AffirmationHasRelation.Equals(itemReferences.HasStatus))
                            {
                                sql.Append(UtilSqlBuilder.ConditionEqual(itemReferences.TableInitial, "StatusRegister", CStatusRegister.Active, numberParameter++, cmd));
                            }

                            if (CFilter.AffirmationHasRelation.Equals(itemReferences.HasAccount))
                            {
                                sql.Append(UtilSqlBuilder.ConditionEqual(itemReferences.TableInitial, "IdAccount", idAccount, numberParameter++, cmd));
                            }

                        }
                    }


                    foreach (var fieldsTemp in filterExecution.FilterExecutionDetails)
                    {


                        if (CFilter.AffirmationHasRelation.Equals(fieldsTemp.FilterCriteria.FilterField.FilterTable.HasRelation))
                        {

                            sql.Append(
                                  UtilSqlBuilder.AddCondition(fieldsTemp.FilterCriteria.FilterField.FilterTable.TableInitial,
                                                 fieldsTemp.FilterCriteria.FilterField.Field,
                                                 fieldsTemp.FilterCriteria.TypeFilter.SignFilter,
                                                 fieldsTemp.Value,
                                                 numberParameter,
                                                 cmd)
                                                 );
                        }
                        else if (CFilter.NegationHasRelation.Equals(fieldsTemp.FilterCriteria.FilterField.FilterTable.HasRelation))
                        {
                            sql.Append(
                                   UtilSqlBuilder.AddCondition("tb",
                                                  fieldsTemp.FilterCriteria.FilterField.Field,
                                                  fieldsTemp.FilterCriteria.TypeFilter.SignFilter,
                                                  fieldsTemp.Value,
                                                  numberParameter,
                                                  cmd)
                                                  );
                        }

                        numberParameter++;
                    }

                    cmd.CommandText = sql.ToString();

                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var dataRow = new ExpandoObject() as IDictionary<string, object>;
                            for (var iFiled = 0; iFiled < dataReader.FieldCount; iFiled++)
                                dataRow.Add(
                                    dataReader.GetName(iFiled),
                                    dataReader.IsDBNull(iFiled) ? null : dataReader[iFiled] // use null instead of {}
                                );

                            retObject.Add((ExpandoObject)dataRow);
                        }
                    }

                }

            }

            return retObject;
        }


    }
}
