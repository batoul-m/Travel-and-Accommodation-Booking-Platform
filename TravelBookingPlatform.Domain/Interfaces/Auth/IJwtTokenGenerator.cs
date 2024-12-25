using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Domain.Interfaces.Auth;

public interface IJwtTokenGenerator
{
    JwtToken Generate(User user);
}