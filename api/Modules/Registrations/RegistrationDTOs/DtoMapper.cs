namespace Api.Modules.Registrations
{
    public class DtoMapper
    {
        public List<RegistrationPreviewDto> MapPreview(List<RegistrationDto> registrations)
        {
            return [.. registrations
                .Select(registration =>
                new RegistrationPreviewDto(
                    registration.Id,
                    registration.Name,
                    registration.Email,
                    registration.Status,
                    registration.Date
                    )
                )];
        }
    }
}
