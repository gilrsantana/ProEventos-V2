using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class AccountService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IMapper mapper,
    IUserPersist userPersist,
    ITokenService tokenService)
    : IAccountService
{
    public async Task<SignInResult?> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
    {
        try
        {
            var user = await userManager
                .Users
                .SingleOrDefaultAsync(u => u.UserName == userUpdateDto.Username.ToLower());
            if (user == null) return null;
            return await signInManager.CheckPasswordSignInAsync(user, password, false);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao verificar password. Erro: {ex.Message}");
        }
    }

    public async Task<UserCreatedDto?> CreateAccountAsync(UserDto userDto)
    {
        try
        {
            var user = mapper.Map<User>(userDto);
            var result = await userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded) return null;
            
            var userCreated = await userManager.FindByNameAsync(user.UserName);
            if (userCreated == null) return null;
            var userToReturn = mapper.Map<UserCreatedDto>(userCreated);
            userToReturn.PrimeiroNome = userCreated.PrimeiroNome;   
            userToReturn.UltimoNome = userCreated.UltimoNome;
            userToReturn.Token = await tokenService.CreateToken(
                new UserUpdateDto
                {
                    Id = userCreated.Id, 
                    Username = userCreated.UserName
                });
            
            return userToReturn;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao criar conta. Erro: {ex.Message}");
        }
    }

    public async Task<UserUpdateDto?> GetUserByUserNameAsync(string? username)
    {
        try
        {
            var user = await userPersist.GetUserByUserNameAsync(username);
            return user == null ? null : mapper.Map<UserUpdateDto>(user);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao buscar usuário pelo nome. Erro: {ex.Message}");
        }
    }

    public async Task<UserUpdateDto?> UpdateAccountAsync(UserUpdateDto userUpdateDto)
    {
        try
        {
            var user = await userPersist.GetUserByUserNameAsync(userUpdateDto.Username);
            if (user == null) return null;
            mapper.Map(userUpdateDto, user);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

            userPersist.Update(user);
            if (await userPersist.SaveChangesAsync())
            {
                if (user.UserName == null) return null;
                var userToReturn = await userPersist.GetUserByUserNameAsync(user.UserName);
                return mapper.Map<UserUpdateDto>(userToReturn);
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
            return await userManager
                .Users
                .AnyAsync(u => u.UserName == username.ToLower());
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao verificar se usuário existe. Erro: {ex.Message}");
        }
    }
}