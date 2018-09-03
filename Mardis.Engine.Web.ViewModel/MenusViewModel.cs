using System;

namespace Mardis.Engine.Web.ViewModel
{
    public class MenusViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string UrlMenu { get; set; }
        public Guid IdParent { get; set; }
        public string StatusRegister { get; set; }
    }
}
