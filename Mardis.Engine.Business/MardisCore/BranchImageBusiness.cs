using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class BranchImageBusiness : ABusiness
    {

        private readonly BranchImageDao _branchImageDao;

        public BranchImageBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _branchImageDao = new BranchImageDao(mardisContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchImages"></param>
        /// <returns></returns>
        public BranchImages SaveBranchImages(BranchImages branchImages)
        {
            branchImages.Branch = null;
            return _branchImageDao.SaveBranchImages(branchImages);
        }

        public List<BranchImages> GetBranchesImagesList(Guid idBranch, Guid idAccount, Guid idCampaign)
        {
            return _branchImageDao.GetBranchImagesList(idBranch, idAccount, idCampaign);
        }

        public List<BranchImages> GetBranchesImagesList(Guid idBranch, Guid idAccount)
        {
            return _branchImageDao.GetBranchImagesList(idBranch, idAccount);
        }

        public BranchImages GetBranchImageById(Guid idImageBranch, Guid idAccount)
        {
            return _branchImageDao.GetBranchImageById(idImageBranch, idAccount);
        }

        public void DeleteBranchImage(Guid idImageBranch, Guid idAccount)
        {
            var branchImage = _branchImageDao.GetBranchImageById(idImageBranch, idAccount);
            _branchImageDao.DeleteBranchImage(branchImage);
        }
    }
}
