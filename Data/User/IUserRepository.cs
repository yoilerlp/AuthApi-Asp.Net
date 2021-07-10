using System.Collections.Generic;
using AuthApi.Models;

namespace AuthApi.Data
{
    public interface IUserRepository
    {
        UserResponse SignIn(UserLogin userLogin);
        void SignUp(UserLogin userLogin);
        UserLogin GetUserByEmail(string email, string password);

        IEnumerable<UserLogin> GetAllUsers();
    }
}