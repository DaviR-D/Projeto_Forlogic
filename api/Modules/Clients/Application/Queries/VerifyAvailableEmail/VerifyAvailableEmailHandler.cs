using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.VerifyAvailableEmail
{
    public class VerifyAvailableEmailHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var query = (VerifyAvailableEmailQuery)input;
            Client? existingEmail = repository.GetAll().FirstOrDefault(client => client.Email == query.Email);
            if (existingEmail != null)
            {
                return new VerifyAvailableEmailResponse(existingEmail.Id.Equals(query.Id));
            }
            return new VerifyAvailableEmailResponse(true);
        }
    }
}
