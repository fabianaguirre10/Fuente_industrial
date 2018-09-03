using System.Threading.Tasks;

namespace Mardis.Engine.Web.Security.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
