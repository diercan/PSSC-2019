using System;

namespace FrBaschet.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}