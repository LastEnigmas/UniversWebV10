using Core.DTOs;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.UserSer
{
    public interface IUserService
    {
        bool IsEmail(string email);
        bool IsUsername(string username);
        void Add(User user);
        bool RegisterUser(string id);
        void Update(User user);
        User SignInUser(SignInViewModel signIn);
        User GetUserByActiveCode(string code);
        User FindUserByEmailOrUsername(ForgotPasswordViewModel forgotPassword);
        void Save();
    }
}
