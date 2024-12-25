using AutoMapper;
using TravelBookingPlatform.Application.Users.Login;
using TravelBookingPlatform.Application.Users.Register;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Mapping;

public class UsersProfile : Profile
{
  public UsersProfile()
  {
    CreateMap<JwtToken, LoginResponse>();
    CreateMap<RegisterCommand, User>();
  }
}