using System.Reflection;
using MyDie.Die;

namespace Data.Extensions
{
    public static class DieContainerExtension
    {
        public static void RegisterData(this IDieContainer container)
        {
            container.Register(Assembly.GetExecutingAssembly());
        }
    }
}
