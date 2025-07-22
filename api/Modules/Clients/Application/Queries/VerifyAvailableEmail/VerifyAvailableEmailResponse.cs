using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.VerifyAvailableEmail
{
    public class VerifyAvailableEmailResponse(bool isAvailable, string? message = null) : IRequestOutput
    {
        public bool IsAvailable { get; set; } = isAvailable;
        public string? Message { get; set; } = message;
    }
}
