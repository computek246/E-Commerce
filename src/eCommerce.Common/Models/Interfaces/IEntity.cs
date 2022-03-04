using System;

namespace eCommerce.Common.Models.Interfaces
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; } // Id (Primary key)
    }
}