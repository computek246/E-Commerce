using System.Collections.Generic;
using eCommerce.Common.Models;

namespace eCommerce.Entities.Localization
{
    public class Language : Auditable
    {
        private ICollection<LocaleStringResource> _stringResources;


        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public bool Rtl { get; set; }
        public int DisplayOrder { get; set; }

        public ICollection<LocaleStringResource> StringResources
        {
            get => _stringResources ??= new HashSet<LocaleStringResource>();
            protected set => _stringResources = value;
        }
    }
}
