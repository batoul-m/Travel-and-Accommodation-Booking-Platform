using AutoMapper;
using TravelBookingPlatform.Api.Dtos.Auth;
using TravelBookingPlatform.Application.Users.Login;
using TravelBookingPlatform.Application.Users.Register;

namespace TravelBookingPlatform.Api.Mapping;

public class AuthProfile : Profile
{
  public AuthProfile()
  {
    CreateMap<LoginRequest, LoginCommand>();
    CreateMap<RegisterRequest, RegisterCommand>();
  }
}