using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Services.AuthServices;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredDto>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
    {
        private readonly AuthRules _authRules;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public RegisterCommandHandler(AuthRules authRules, IUserRepository userRepository, IAuthService authService)
        {
            _authRules = authRules;
            _userRepository = userRepository;
            _authService = authService;
        }


        public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authRules.EmailCanNotBeDuplicatedWhenRegistering(request.UserForRegisterDto.Email);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = request.UserForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = request.UserForRegisterDto.FirstName,
                LastName = request.UserForRegisterDto.LastName,
                Status = true
            };
            User createdUser = await _userRepository.AddAsync(user);
            AccessToken createdAccessToken = await _authService.CreateAccessTokenAsync(createdUser);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            RegisteredDto registeredDto = new RegisteredDto
            {
                AccessToken = createdAccessToken,
                RefreshToken = addedRefreshToken
            };
            return registeredDto;
        }
    }
}