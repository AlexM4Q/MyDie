using Data.Models;
using Data.Repositories;
using MyDie.App.Extensions;
using MyDie.Die;
using Netb;
using Netb.Lib;

namespace MyDie.App
{
    internal static class Program
    {
        private static readonly IDieContainer Container = new DieContainer();

        private static void Main()
        {
            Container.RegisterApp();

            var catsRepository = Container.Get<ICatsRepository>();
            catsRepository.Save(new Cat {Name = "Коржик"});
            catsRepository.Save(new Cat {Name = "Компот"});
            catsRepository.Save(new Cat {Name = "Карамелька"});

            var server = new NetbServer(x => Container.Get(x) as NetbController);
            var env = new Environment(server);

            var response = env.Request("home/index", "🍪");
        }
    }

    public sealed class Environment
    {
        private readonly INetbServer _server;

        public Environment(INetbServer server)
        {
            _server = server;
        }

        public NetbResponse Request(string path, object content = null) => _server.Handle(new NetbRequest
        {
            Path = path,
            Content = content
        });
    }
}
