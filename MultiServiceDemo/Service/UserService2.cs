using MultiServiceDemo.DI;
using MultiServiceDemo.Filters;
using MultiServiceDemo.Model;

namespace MultiServiceDemo.Service
{
    [ServiceFilter("service2")]
    public class UserService2 : IUserService, ITransientDependency
    {
        public User GetUser()
        {
            return new User
            {
                name = "UserService2",
                sex = "男",
                age = "18"
            };
        }
    }
}
