using Core.DTOs;
using Core.Generator;
using Core.Security;
using Data.Model;
using Data.MyDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.UserSer
{
    public class UserService : IUserService
    {
        private readonly MyDb _db;
        public UserService(MyDb db)
        {
            _db = db;
        }

        public void Add(User user)
        {
            _db.Users.Add(user);
            Save();
        }

        public User FindUserByEmailOrUsername(ForgotPasswordViewModel forgotPassword)
        {
            User user = _db.Users.SingleOrDefault(u => u.Username == forgotPassword.EmailOrUsername || u.Email == forgotPassword.EmailOrUsername);
            return user;
        }

        public User GetUserByActiveCode(string code)
        {
            return _db.Users.SingleOrDefault(u => u.ActiveCode == code);
        }

        public bool IsEmail(string email)
        {
            return _db.Users.Any(u => u.Email == email);
        }

        public bool IsUsername(string username)
        {
            return _db.Users.Any(u => u.Username == username);
        }

        public bool RegisterUser(string id)
        {
            User user = _db.Users.SingleOrDefault(u => u.ActiveCode == id);
            if(user == null)
            {
                return false;
            }

            user.IsActive = true;
            user.ActiveCode = ActiveCodeGen.GenerateCode();
            Update(user);

            return true;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public User SignInUser(SignInViewModel signIn)
        {
            string MyHashPassword = PasswordHashC.EncodePasswordMd5(signIn.Password);
            User user = _db.Users.SingleOrDefault( u => u.Email == signIn.UsernameOrEmail || u.Username == signIn.UsernameOrEmail);
            if((user == null) || (user.Password != MyHashPassword))
            {
                return null;
            }

            return user;
            
        }

        public void Update(User user)
        {
            _db.Update(user);
            Save();
        }
    }
}
