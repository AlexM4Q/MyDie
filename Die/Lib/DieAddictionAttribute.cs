using System;

namespace MyDie.Die.Lib
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DieAddictionAttribute : Attribute
    {
        public DieLifecycle DieLifecycle { get; set; } = DieLifecycle.Singleton;
    }
}
