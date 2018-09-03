using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.BranchViewModels
{
    public class RouteBranchViewModel
    {
        public string route { get; set; }
        public bool status { get; set; } = false;
        public int numbreBranches { get; set; }
        public int linkbranch { get; set; }
        public int linkencuestador { get; set; }
    }
}
