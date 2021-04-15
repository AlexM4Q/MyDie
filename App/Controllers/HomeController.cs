using System.Linq;
using MyDie.App.Helpers;
using MyDie.Die.Lib;
using Netb.Lib;

namespace MyDie.App.Controllers
{
    [NetbController(Prefix = "home")]
    [DieAddiction(DieLifecycle = DieLifecycle.Prototype)]
    public sealed class HomeController : NetbController
    {
        private readonly ICookieChecker _cookieChecker;
        private readonly ICookieEater _cookieEater;

        public HomeController(ICookieChecker cookieChecker,
                              ICookieEater cookieEater)
        {
            _cookieChecker = cookieChecker;
            _cookieEater = cookieEater;
        }

        [NetbRoute(Route = "index")]
        public NetbResponse Handle(object content)
        {
            var cookie = content as string;

            if (_cookieChecker.Check(cookie))
            {
                var eaters = _cookieEater.Eat(cookie).ToArray();

                return new NetbResponse
                {
                    StatusCode = NetbStatusCode.Ok,
                    Content = eaters
                };
            }

            return new NetbResponse
            {
                StatusCode = NetbStatusCode.Error,
                Content = "Ну не оч"
            };
        }
    }
}
