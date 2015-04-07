// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extend.cs">
//   Copyright (c) 2015. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Spritely.Cqrs.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Extend
    {
        public static Func<T, object> AsFunc<T>(this Action<T> a)
        {
            return item =>
            {
                a(item);
                return null;
            };
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> a)
        {
            enumerable.Select(a.AsFunc()).ToList();
        }
    }
}
