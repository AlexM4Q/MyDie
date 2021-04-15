using System;

namespace Netb.Lib
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class NetbRouteAttribute : Attribute
    {
        public string Route { get; set; }
    }
}
