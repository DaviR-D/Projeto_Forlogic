﻿namespace Api.Modules.Clients
{
    public class DtoMapper
    {
        public static ClientDto ToDto(Client client)
        {
            return new ClientDto(
                id: client.Id,
                name: client.Name,
                email: client.Email,
                status: client.Status,
                pending: client.Pending,
                date: client.Date,
                age: client.Age,
                address: client.Address,
                other: client.Other,
                interests: client.Interests,
                feelings: client.Feelings,
                values: client.Values
            );
        }

        public static Client ToEntity(ClientDto client)
        {
            return new Client(
                id: client.Id,
                name: client.Name,
                email: client.Email,
                status: client.Status,
                pending: client.Pending,
                date: client.Date,
                age: client.Age,
                address: client.Address,
                other: client.Other,
                interests: client.Interests,
                feelings: client.Feelings,
                values: client.Values
            );
        }

        public static List<ClientPreviewDto> ToPreviewDto(List<Client> clients)
        {
            return [.. clients
                .Select(registration =>
                new ClientPreviewDto(
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
