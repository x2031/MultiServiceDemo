using MultiServiceDemo.DI;
using MultiServiceDemo.Filters;
using MultiServiceDemo.Model;

namespace MultiServiceDemo.Service
{
    [ServiceFilter("service3")]
    public class UserService3 : IUserService, ITransientDependency
    {
        public User GetUser()
        {
            return new User
            {
                name = "UserService3",
                sex = "男",
                age = "30"
            };
        }
    }
}
