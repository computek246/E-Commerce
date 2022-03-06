using eCommerce.Common.Models;

namespace eCommerce.Entities.Catalog
{
    public class Manufacturer : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
    }
}
