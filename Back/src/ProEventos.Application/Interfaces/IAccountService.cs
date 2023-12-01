using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> UserExists(string username);
        Task<UserUpdateDto?> GetUserByUserNameAsync(string username);
        Task<SignInResult?> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserDto?> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto?> UpdateAccountAsync(UserUpdateDto userUpdateDto);
    }
}