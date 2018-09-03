using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.Utility
{
  public  class StructXmlModelQuestion
    {
        public string id { set; get; }
        public string valueText { set; get; }
        public string QuestionText { set; get; }
        public string QuestionTipo { set; get; }
        public string tipo;

        public List<StructXmlModelQuestionDetail> Detail{ get; set; }
    }
}
