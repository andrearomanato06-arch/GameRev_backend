using GameRev.DTOs.Requests;
using GameRev.Models.Utils;

namespace GameRev.DTOs.Mappers;

public static class RequestToRequest
{
    public static UserRequest RegistrationRequestToUserRequest (RegistrationRequest request)
    {
        return new UserRequest
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            RegistrationDate = DateTime.Now,
            Role = UserRole.BASIC
        };   
    }

    public static UserRequest RegistrationRequestToAdminRequest (RegistrationRequest request)
    {
        return new UserRequest
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            RegistrationDate = DateTime.Now,
            Role = UserRole.ADMIN
        };   
    }
}