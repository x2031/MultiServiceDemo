namespace MultiServiceDemo.Filters
{
    /// <summary>
    /// 服务过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ServiceFilterAttribute : Attribute
    {
        public string _serviceName { get; }
        /// <summary>
        /// 服务过滤器
        /// </summary>
        /// <param name="serviceName"></param>
        /// <exception cref="Exception"></exception>
        public ServiceFilterAttribute(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                throw new Exception("serviceName不能为空");

            _serviceName = serviceName;
        }
    }
}
