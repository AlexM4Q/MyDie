using System.Collections.Generic;

namespace MyDie.App.Helpers
{
    public interface ICookieEater
    {
        IEnumerable<string> Eat(string cookie);
    }
}
