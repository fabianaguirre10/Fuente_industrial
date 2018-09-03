using System.Threading.Tasks;

namespace Mardis.Engine.Web.Security.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
