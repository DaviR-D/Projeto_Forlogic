using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.VerifyAvailableEmail
{
    public class VerifyAvailableEmailResponse(bool isAvailable) : IRequestOutput
    {
        public bool IsAvailable { get; set; } = isAvailable;
    }
}
