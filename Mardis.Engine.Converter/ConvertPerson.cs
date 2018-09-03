using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Web.ViewModel.TaskViewModels;

namespace Mardis.Engine.Converter
{
    public class ConvertPerson
    {
        public static Person FromMyTaskViewModel(MyTaskViewModel model, Person person)
        {
            person.Name = model.BranchOwnerName;
            person.SurName = string.Empty;
            person.Mobile = model.BranchOwnerMobile;
            person.Phone = model.BranchOwnerPhone;
            person.Document = model.BranchOwnerDocument;

            return person;
        }
    }
}
