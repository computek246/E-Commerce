using eCommerce.Common.Models.Interfaces;

namespace eCommerce.Common.Models
{
    public class BaseEntity : IEntity<int>
    {
        public int Id { get; set; } // Id (Primary key)
    }
}