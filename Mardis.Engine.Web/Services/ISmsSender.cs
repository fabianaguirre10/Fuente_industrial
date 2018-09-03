using System.Threading.Tasks;

namespace Mardis.Engine.Web.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
