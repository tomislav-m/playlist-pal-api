namespace Domain.Helpers;

public static class RandomizeCollectionHelper
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
    {
        return list.OrderBy(_ => Guid.NewGuid());
    }
}