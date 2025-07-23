using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.VerifyAvailableEmail
{
    public class VerifyAvailableEmailQuery(Guid? id, string email) : IRequestInput
    {
        public Guid? Id { get; set; } = id;
        public string Email { get; set; } = email;
    }
}
