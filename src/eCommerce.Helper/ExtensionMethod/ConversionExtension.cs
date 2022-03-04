using System;

namespace eCommerce.Helper.ExtensionMethod
{
    public static class ConversionExtension
    {
        public static TResult To<TResult>(this object entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return ConvertValue<TResult>(entity);
        }

        public static TResult ConvertValue<TResult>(object value)
        {
            try
            {
                return (TResult) Convert.ChangeType(value, typeof(TResult));
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}