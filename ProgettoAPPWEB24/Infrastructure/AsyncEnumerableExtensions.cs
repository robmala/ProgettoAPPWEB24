﻿using System.Collections;

namespace ProgettoAPPWEB24.Infrastructure;

static class AsyncEnumerableExtensions
{
    public static async Task<IList<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
    {
        var results = new List<T>();
        await foreach (var item in source) results.Add(item);

        return results;
    }
}
