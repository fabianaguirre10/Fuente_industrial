using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class BranchImageDao : ADao
    {
        public BranchImageDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public BranchImages SaveBranchImages(BranchImages branchImages)
        {
            return InsertOrUpdate(branchImages);
        }

        public List<BranchImages> GetBranchImagesList(Guid idBranch, Guid idAccount, Guid idCampaign)
        {
            return Context.BranchImageses
                .Where(bi => bi.IdBranch == idBranch &&
                        bi.IdCampaign == idCampaign &&
                        bi.Branch.IdAccount == idAccount)
                .ToList();
        }

        public List<BranchImages> GetBranchImagesList(Guid idBranch, Guid idAccount)
        {
            return Context.BranchImageses
                .Where(bi => bi.IdBranch == idBranch &&
                        bi.Branch.IdAccount == idAccount)
                .ToList();
        }

        public BranchImages GetBranchImageById(Guid idImageBranch, Guid idAccount)
        {
            return Context.BranchImageses
                .FirstOrDefault(bi => bi.Id == idImageBranch && bi.Branch.IdAccount == idAccount);
        }

        public void DeleteBranchImage(BranchImages branchImage)
        {
            Context.BranchImageses.Remove(branchImage);
            Context.SaveChanges();
        }
    }
}
