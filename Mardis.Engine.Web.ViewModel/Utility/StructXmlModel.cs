using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.Utility
{
  public  class StructXmlModel
    {
        public string id { set; get; }
        public string valueText { set; get; }
        public string QuestionText { set; get; }
        public bool IsDynamic { set; get; }
        public List<StructXmlModelQuestion> Question { get; set; }
    }
}
