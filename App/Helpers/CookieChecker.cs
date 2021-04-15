using MyDie.Die.Lib;

namespace MyDie.App.Helpers
{
    [DieAddiction(DieLifecycle = DieLifecycle.Singleton)]
    internal sealed class CookieChecker : ICookieChecker
    {
        private int _checked = 0;

        public bool Check(string cookie)
        {
            try
            {
                return cookie == "🍪";
            }
            finally
            {
                _checked++;
            }
        }
    }
}
