namespace RedacteurPortaal.Helpers;

public static class AsyncLinq
{
    public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(
        this IEnumerable<TSource> source, Func<TSource, Task<TResult>> method)
    {
        if (source != null && source.Any())
        {
            return await Task.WhenAll(source.Select(async s => await method(s)));

        }
        else
        {
            return new List<TResult>();
        }
    }

    public static IEnumerable<T> DiscardNullValues<T>(this IEnumerable<T?> nullable)
    {
        foreach (var item in nullable)
        {
            if (item is not null) yield return item;
        }
    }
}
