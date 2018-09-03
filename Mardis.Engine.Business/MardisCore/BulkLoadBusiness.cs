using System;
using System.Collections.Generic;
using System.Threading;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.Business.MardisCore
{
    /// <summary>
    /// Clase de negocios de cargas masivas
    /// </summary>
    public class BulkLoadBusiness : ABusiness
    {
        private readonly BulkLoadDao _bulkLoadDao;
        private readonly BulkLoadCatalogDao _bulkLoadCatalogDao;
        private readonly BulkLoadStatusDao _bulkLoadStatusDao;
        private readonly string _connectionString;

        public BulkLoadBusiness(MardisContext mardisContext, string connectionString)
               : base(mardisContext)
        {
            _bulkLoadDao = new BulkLoadDao(mardisContext);
            _bulkLoadCatalogDao = new BulkLoadCatalogDao(mardisContext);
            _bulkLoadStatusDao = new BulkLoadStatusDao(mardisContext);
            _connectionString = connectionString;
        }

        /// <summary>
        /// Dame datos por cuenta
        /// </summary>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        public List<BulkLoad> GetDataByAccount(Guid idAccount)
        {
            var itemsReturn = _bulkLoadDao.GetDataByAccount(idAccount);

            foreach (var itemTemp in itemsReturn)
            {
                itemTemp.BulkLoadCatalog = _bulkLoadCatalogDao.GetOne(itemTemp.IdBulkLoadCatalog);
                itemTemp.BulkLoadStatus = _bulkLoadStatusDao.GetOne(itemTemp.IdBulkLoadStatus);
            }

            return itemsReturn;
        }

        /// <summary>
        /// Crear nuevo proceso
        /// </summary>
        /// <param name="idAccount"></param>
        /// <param name="idBulkCatalog"></param>
        /// <param name="fileName"></param>
        /// <param name="totalLines"></param>
        /// <returns></returns>
        public BulkLoad CreateNewProcess(Guid idAccount, Guid idBulkCatalog, string fileName, int totalLines)
        {
            var oneBulkLoad = new BulkLoad
            {
                BulkLoadStatus = _bulkLoadStatusDao.GetOneByCode(CBulkLoad.StateBulk.Pendiente)
            };

            oneBulkLoad.IdBulkLoadStatus = oneBulkLoad.BulkLoadStatus.Id;
            oneBulkLoad.BulkLoadCatalog = _bulkLoadCatalogDao.GetOne(idBulkCatalog);
            oneBulkLoad.IdBulkLoadCatalog = oneBulkLoad.BulkLoadCatalog.Id;
            oneBulkLoad.FileName = fileName;
            oneBulkLoad.ContainerName = string.Empty;
            oneBulkLoad.CreatedDate = DateTime.Now;
            oneBulkLoad.IdAccount = idAccount;
            oneBulkLoad.StatusRegister = CStatusRegister.Active;
            oneBulkLoad.TotalAdded = 0;
            oneBulkLoad.TotalFailed = 0;
            oneBulkLoad.TotalUpdated = 0;
            oneBulkLoad.TotalRegister = totalLines;

            _bulkLoadDao.InsertOrUpdate(oneBulkLoad);

            return oneBulkLoad;
        }

        /// <summary>
        /// Procesar archivo
        /// </summary>
        /// <param name="idAccount"></param>
        /// <param name="idBulkCatalog"></param>
        /// <param name="characteristicBulk"></param>
        /// <param name="fileName"></param>
        /// <param name="bufferArray"></param>
        public Guid ProcessFile(Guid idAccount, Guid idBulkCatalog,
                                string characteristicBulk,
                                string fileName,
                                byte[] bufferArray)
        {
            var oneBulkCatalog = _bulkLoadCatalogDao.GetOne(idBulkCatalog);
            var oneBulkLoad = CreateNewProcess(idAccount, idBulkCatalog, fileName, FileUtil.NumberFiles(bufferArray));
            var returnValue = oneBulkLoad.Id;

            try
            {

                switch (oneBulkCatalog.Code)
                {
                    case CBulkLoad.TypeBulk.BulkBranch:

                        var oneBulkBranch = new BulkLoadBranchBusiness(_connectionString, characteristicBulk, idAccount, oneBulkLoad.Id, bufferArray);
                        Thread oneThread = new Thread(oneBulkBranch.InitProcess);

                        oneThread.Start();
                        break;
                }
            }
            catch
            {
                // ignored
            }

            return returnValue;
        }

        public BulkLoad GetOne(Guid id)
        {
            var itemReturn = _bulkLoadDao.GetOne(id);

            itemReturn.BulkLoadCatalog = _bulkLoadCatalogDao.GetOne(itemReturn.IdBulkLoadCatalog);
            itemReturn.BulkLoadStatus = _bulkLoadStatusDao.GetOne(itemReturn.IdBulkLoadStatus);

            return itemReturn;
        }
    }
}
