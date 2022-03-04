using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Helper.ExtensionMethod
{
    public static partial class LinqExtensionMethods
    {
        public static IEnumerable<TResult> FullOuterGroupJoin<TA, TB, TKey, TResult>(
            this IEnumerable<TA> a,
            IEnumerable<TB> b,
            Func<TA, TKey> selectKeyA,
            Func<TB, TKey> selectKeyB,
            Func<IEnumerable<TA>, IEnumerable<TB>, TKey, TResult> projection,
            IEqualityComparer<TKey> cmp = null)
        {
            cmp ??= EqualityComparer<TKey>.Default;
            var lookup1 = a.ToLookup(selectKeyA, cmp);
            var lookup2 = b.ToLookup(selectKeyB, cmp);

            var keys = new HashSet<TKey>(lookup1.Select(p => p.Key), cmp);
            keys.UnionWith(lookup2.Select(p => p.Key));

            var join = from key in keys
                let xa = lookup1[key]
                let xb = lookup2[key]
                select projection(xa, xb, key);

            return join;
        }

        public static IEnumerable<TResult> FullOuterJoin<TA, TB, TKey, TResult>(
            this IEnumerable<TA> a,
            IEnumerable<TB> b,
            Func<TA, TKey> selectKeyA,
            Func<TB, TKey> selectKeyB,
            Func<TA, TB, TKey, TResult> projection,
            TA defaultA = default,
            TB defaultB = default,
            IEqualityComparer<TKey> cmp = null)
        {
            cmp ??= EqualityComparer<TKey>.Default;
            var lookup1 = a.ToLookup(selectKeyA, cmp);
            var lookup2 = b.ToLookup(selectKeyB, cmp);

            var keys = new HashSet<TKey>(lookup1.Select(p => p.Key), cmp);
            keys.UnionWith(lookup2.Select(p => p.Key));

            var join = from key in keys
                from xa in lookup1[key].DefaultIfEmpty(defaultA)
                from xb in lookup2[key].DefaultIfEmpty(defaultB)
                select projection(xa, xb, key);

            return join;
        }
    }
}
