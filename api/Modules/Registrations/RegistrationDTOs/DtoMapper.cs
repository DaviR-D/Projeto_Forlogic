namespace Api.Modules.Registrations
{
    public class DtoMapper
    {
        public static RegistrationDto ToDto(Registration registration)
        {
            return new RegistrationDto(
                id: registration.Id,
                name: registration.Name,
                email: registration.Email,
                status: registration.Status,
                pending: registration.Pending,
                date: registration.Date,
                age: registration.Age,
                address: registration.Address,
                other: registration.Other,
                interests: registration.Interests,
                feelings: registration.Feelings,
                values: registration.Values
            );
        }

        public static Registration ToEntity(RegistrationDto registrationDto)
        {
            return new Registration(
                id: registrationDto.Id,
                name: registrationDto.Name,
                email: registrationDto.Email,
                status: registrationDto.Status,
                pending: registrationDto.Pending,
                date: registrationDto.Date,
                age: registrationDto.Age,
                address: registrationDto.Address,
                other: registrationDto.Other,
                interests: registrationDto.Interests,
                feelings: registrationDto.Feelings,
                values: registrationDto.Values
            );
        }

        public static List<RegistrationPreviewDto> ToPreviewDto(List<Registration> registrations)
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
