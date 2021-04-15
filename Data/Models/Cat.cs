using System;

namespace Data.Models
{
    public sealed class Cat
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
