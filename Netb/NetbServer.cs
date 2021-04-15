using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Netb.Lib;

namespace Netb
{
    public sealed class NetbServer : INetbServer
    {
        private readonly IDictionary<string, Type> _controllers = new Dictionary<string, Type>();
        private readonly Func<Type, NetbController> _creator;

        public NetbServer(Func<Type, NetbController> creator)
        {
            _creator = creator;

            foreach (var type in Assembly.GetCallingAssembly().GetTypes())
            {
                var configs = type.GetCustomAttribute<NetbControllerAttribute>();
                if (configs == null)
                    continue;

                _controllers.Add(configs.Prefix, type);
            }
        }

        public NetbResponse Handle(NetbRequest request)
        {
            var path = request.Path.Split('/');
            var prefix = path[0];

            if (!_controllers.TryGetValue(prefix, out var type))
                return new NetbResponse {StatusCode = NetbStatusCode.NotFound};

            var route = path[1];
            var handler = type
                .GetMethods()
                .FirstOrDefault(x => x.GetCustomAttribute<NetbRouteAttribute>()?.Route == route);

            if (handler == null)
                return new NetbResponse {StatusCode = NetbStatusCode.NotFound};

            var controller = _creator.Invoke(type);
            controller.Request = request;

            return handler.Invoke(controller, new[] {request.Content}) as NetbResponse;
        }
    }
}
