namespace AdvancedDataStructures.Lookups.SkipLists;

public class ComparableShardedSkipList<T, TShard>(int shardCount, Func<T, int> shardFunction)
    : ShardedSkipList<T, TShard>(shardCount, shardFunction), IComparableSkipList<T>
    where T : IComparable<T>, IEquatable<T>
    where TShard : IComparableSkipList<T>, new()
{
    public bool Update(T newValue) => GetShard(newValue).Update(newValue);
}