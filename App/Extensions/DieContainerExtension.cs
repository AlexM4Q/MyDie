using System.Reflection;
using Data.Extensions;
using MyDie.Die;

namespace MyDie.App.Extensions
{
    public static class DieContainerExtension
    {
        public static void RegisterApp(this IDieContainer container)
        {
            container.RegisterData();
            container.Register(Assembly.GetExecutingAssembly());
        }
    }
}
