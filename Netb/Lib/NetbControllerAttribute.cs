using System;

namespace Netb.Lib
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class NetbControllerAttribute : Attribute
    {
        public string Prefix { get; set; }
    }
}
