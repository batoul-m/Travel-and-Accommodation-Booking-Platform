using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Users.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
  private readonly IMapper _mapper;
  private readonly IRoleRepository _roleRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;

  public RegisterCommandHandler(
    IUserRepository userRepository,
    IMapper mapper,
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _mapper = mapper;
    _roleRepository = roleRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(
    RegisterCommand request,
    CancellationToken cancellationToken = default)
  {
    var role = await _roleRepository.GetByNameAsync(request.Role, cancellationToken)
               ?? throw new InvalidRoleException(UserMessages.InvalidRole);

    if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
    {
      throw new UserEmailAlreadyExistsException(UserMessages.EmailAlreadyExists);
    }

    var userToAdd = _mapper.Map<User>(request);

    userToAdd.Roles.Add(role);

    await _userRepository.CreateAsync(userToAdd, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}