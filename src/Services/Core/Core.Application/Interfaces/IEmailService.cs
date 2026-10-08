using Core.Application.DTOs.Email;

namespace Core.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto request);
    }
}
