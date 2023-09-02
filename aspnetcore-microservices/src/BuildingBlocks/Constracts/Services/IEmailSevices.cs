using Shared.Services.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.Services
{
    public interface IEmailSevices<T> where T : class
    {
        Task SendEmailRequest(T request, CancellationToken cancellationToken = new CancellationToken());
    }
}
