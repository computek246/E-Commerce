using System.Collections.Generic;

namespace eCommerce.Helper.ExtensionMethod
{
    /// <summary>
    ///     Hierarchy node class which contains a nested collection of hierarchy nodes
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    public class HierarchyNode<TEntity> where TEntity : class
    {
        public TEntity Entity { get; set; }
        public IEnumerable<HierarchyNode<TEntity>> ChildNodes { get; set; }
        public int Depth { get; set; }
    }
}