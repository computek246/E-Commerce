using System.Collections.Generic;

namespace eCommerce.Infrastructure.SingletonFactory
{
    /// <summary>
    ///     Provides a singleton list for a certain type.
    /// </summary>
    /// <typeparam name="T">The type of list to store.</typeparam>
    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        /// <summary>
        ///     The singleton instance for the specified type T. Only one instance (at the time) of this list for each type of T.
        /// </summary>
        public new static IList<T> Instance
        {
            get => Singleton<IList<T>>.Instance;
            set => Singleton<IList<T>>.Instance = value;
        }
    }
}