using System;

namespace Mardis.Engine.Web.ViewModel
{
    /// <summary>
    /// ViewModel de Servicios por campaña
    /// </summary>
    public class ListCampaignServicesViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public Guid IdService { get; set; }
    }
}
