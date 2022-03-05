using System.Collections.Generic;
using eCommerce.Common.Models;

namespace eCommerce.Entities.Catalog
{
    public class Category : Auditable
    {
        private ICollection<Category> _categories;


        public string Name { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
        public string MetaKeywords { get; set; }
        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<Category> Categories
        {
            get => _categories ??= new HashSet<Category>();
            protected set => _categories = value;
        }
    }
}
