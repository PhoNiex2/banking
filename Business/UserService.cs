using banking.Data;

namespace banking.Business
{
    public class UserService
    {
        private readonly UserData userData = new UserData();

        public bool Login(string username, string password)
        {
            return userData.Login(username, password);
        }
    }
}