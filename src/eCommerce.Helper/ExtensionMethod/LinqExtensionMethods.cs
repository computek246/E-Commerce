using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Helper.ExtensionMethod
{
    public static partial class LinqExtensionMethods
    {
        private static IEnumerable<HierarchyNode<TEntity>> CreateHierarchy<TEntity, TProperty>
        (IEnumerable<TEntity> allItems, TEntity parentItem,
            Func<TEntity, TProperty> idProperty, Func<TEntity, TProperty> parentIdProperty, int depth)
            where TEntity : class
        {
            var children = parentItem == null
                ? allItems.Where(i => parentIdProperty(i).Equals(default(TProperty)))
                : allItems.Where(i => parentIdProperty(i).Equals(idProperty(parentItem)));

            if (!children.Any()) yield break;

            depth++;

            foreach (var item in children)
                yield return new HierarchyNode<TEntity>
                {
                    Entity = item,
                    ChildNodes = CreateHierarchy(allItems, item, idProperty, parentIdProperty, depth),
                    Depth = depth
                };
        }

        /// <summary>
        /// LINQ IEnumerable AsHierarchy() extension method
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <typeparam name="TProperty">Property of entity class</typeparam>
        /// <param name="allItems">Flat collection of entities</param>
        /// <param name="idProperty">Reference to ID/Key of entity</param>
        /// <param name="parentIdProperty">Reference to parent ID/Key</param>
        /// <returns>Hierarchical structure of entities</returns>
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity, TProperty>
        (this IEnumerable<TEntity> allItems, Func<TEntity, TProperty> idProperty,
            Func<TEntity, TProperty> parentIdProperty)
            where TEntity : class
        {
            return CreateHierarchy(allItems, default, idProperty, parentIdProperty, 0);
        }


        /// <summary>
        /// LINQ IEnumerable Flatten()
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="nodes"></param>
        /// <param name="childrenSelector"></param>
        /// <returns>IEnumerable of entities</returns>
        public static IEnumerable<TNode> Flatten<TNode>(
            this IEnumerable<TNode> nodes,
            Func<TNode, IEnumerable<TNode>> childrenSelector
        )
        {
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));
            return nodes.SelectMany(c => childrenSelector(c).Flatten(childrenSelector)).Concat(nodes);
        }
    }
}