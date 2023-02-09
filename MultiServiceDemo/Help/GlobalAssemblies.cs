using System.Reflection;

namespace MultiServiceDemo.Help
{

    public static class GlobalAssemblies
    {
        /// <summary>
        /// 解决方案所有程序集
        /// </summary>
        public static readonly Assembly[] AllAssemblies = new Assembly[]
        {
            Assembly.Load("MultiServiceDemo"),
        };

        /// <summary>
        /// 解决方案所有自定义类
        /// </summary>
        public static readonly Type[] AllTypes = AllAssemblies.SelectMany(x => x.GetTypes()).ToArray();

    }
}
