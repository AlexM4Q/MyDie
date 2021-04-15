using System;
using System.Collections.Generic;
using System.Linq;
using Data.Models;
using MyDie.Die.Lib;

namespace Data.Repositories
{
    [DieAddiction(DieLifecycle = DieLifecycle.Singleton)]
    internal sealed class CatsRepository : ICatsRepository
    {
        private readonly IList<Cat> _cats = new List<Cat>();

        public IEnumerable<Cat> Get() =>
            _cats;

        public Cat Get(Guid id) =>
            _cats.FirstOrDefault(x => x.Id == id);

        public void Save(Cat cat) =>
            _cats.Add(cat);
    }
}
