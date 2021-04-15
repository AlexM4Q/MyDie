using System.Collections.Generic;
using Data.Repositories;
using MyDie.Die.Lib;

namespace MyDie.App.Helpers
{
    [DieAddiction(DieLifecycle = DieLifecycle.Prototype)]
    internal sealed class CookieEater : ICookieEater
    {
        private readonly ICatsRepository _catsRepository;

        public CookieEater(ICatsRepository catsRepository)
        {
            _catsRepository = catsRepository;
        }

        public IEnumerable<string> Eat(string cookie)
        {
            foreach (var cat in _catsRepository.Get())
            {
                yield return $"{cat.Name} {cookie}";
            }
        }
    }
}
