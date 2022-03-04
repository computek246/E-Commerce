using eCommerce.Common.Models;

namespace eCommerce.Entities.Localization
{
    public class LocaleStringResource : Auditable
    {
        public int LanguageId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        
        public Language Language { get; set; }
    }
}
