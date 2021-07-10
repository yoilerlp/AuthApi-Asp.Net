using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApi.Models;
using AuthApi.Util;

namespace AuthApi.Data
{
    public class UserRepository : IUserRepository
    {

        private List<UserLogin> users = new List<UserLogin>();

        public UserRepository()
        {
            users.Add(new UserLogin() { Email = "test@gmail.com", Password = "shoulbeencryted" });
            users.Add(new UserLogin() { Email = "yoiler@gmail.com", Password = "shoulbeencryted2" });
        }

        public UserLogin GetUserByEmail(string email, string password)
        {
           return users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public UserResponse SignIn(UserLogin userLogin)
        {
             return  new UserResponse(){
                Email= userLogin.Email,
                Token = JwtUtil.GenerateJWT(userLogin)
            };
        }

        public void SignUp(UserLogin userLogin)
        {
            users.Add(userLogin);
        }

        public IEnumerable<UserLogin> GetAllUsers() {
            return  this.users;
        }
    }
}