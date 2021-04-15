using System;
using System.Collections.Generic;
using Data.Models;

namespace Data.Repositories
{
    public interface ICatsRepository
    {
        IEnumerable<Cat> Get();
        Cat Get(Guid id);
        void Save(Cat cat);
    }
}
