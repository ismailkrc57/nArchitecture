using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;

namespace Application.Features.Auth.Rules;

public class AuthRules
{
    private readonly IUserRepository _userRepository;

    public AuthRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task EmailCanNotBeDuplicatedWhenRegistering(string email)
    {
        User? user = await _userRepository.GetAsync(u => u.Email == email);
        if (user != null) throw new BusinessException("Email is already in use");
    }
}