using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IUserPersist _userPersist;
    public AccountService(UserManager<User> userManager, 
                          SignInManager<User> signInManager, 
                          IMapper mapper, 
                          IUserPersist userPersist)
    {
            _userPersist = userPersist;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        
    }

    public async Task<SignInResult?> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
    {
        try
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == userUpdateDto.Username.ToLower());
            if (user == null) return null;
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao verificar password. Erro: {ex.Message}");
        }
    }

    public async Task<UserDto?> CreateAccountAsync(UserDto userDto)
    {
        try
        {
            var user = _mapper.Map<User>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserDto>(user);
                return userToReturn;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao criar conta. Erro: {ex.Message}");
        }
    }

    public async Task<UserUpdateDto?> GetUserByUserNameAsync(string username)
    {
        try
        {
            var user = await _userPersist.GetUserByUserNameAsync(username);
            if (user == null) return null;
            return _mapper.Map<UserUpdateDto>(user);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao pegar usuário pelo nome. Erro: {ex.Message}");
        }
    }

    public async Task<UserUpdateDto?> UpdateAccountAsync(UserUpdateDto userUpdateDto)
    {
        try
        {
            var user = await _userPersist.GetUserByUserNameAsync(userUpdateDto.Username);
            if (user == null) return null;
            _mapper.Map(userUpdateDto, user);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

            _userPersist.Update(user);
            if (await _userPersist.SaveChangesAsync())
            {
                if (user.UserName == null) return null;
                var userToReturn = await _userPersist.GetUserByUserNameAsync(user.UserName);
                return _mapper.Map<UserUpdateDto>(userToReturn);
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao atualizar usuário. Erro: {ex.Message}");
        }
    }

    public async Task<bool> UserExists(string username)
    {
        try
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao verificar se usuário existe. Erro: {ex.Message}");
        }
    }
}