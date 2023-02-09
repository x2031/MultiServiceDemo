using MultiServiceDemo.DI;
using MultiServiceDemo.Filters;
using MultiServiceDemo.Model;

namespace MultiServiceDemo.Service
{
    [ServiceFilter("service1")]
    public class UserService1 : IUserService, ITransientDependency
    {

        public User GetUser()
        {
            return new User
            {
                name = "UserService1",
                sex = "女",
                age = "28",
            };
        }
    }
}
