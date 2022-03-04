using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eCommerce.Helper.ExtensionMethod
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            return modelState.Values.Where(s => s.Errors.Any())
                .SelectMany(s => s.Errors.Select(x => x.ErrorMessage)).ToList();
        }
    }
}
