using System.Collections;

namespace AdvancedDataStructures.Lookups.SkipLists;

public class ShardedSkipList<T>(int shardCount, Func<T, int> shardFunction)
    : ShardedSkipList<T, SkipList<T>>(shardCount, shardFunction);

public abstract class ShardedSkipList<T, TShard> : ISkipList<T> where TShard : ISkipList<T>, new()
{
    private readonly Func<T, int> _shardFunction;
    private readonly List<TShard> _shards;

    public int Count => _shards.Sum(s => s.Count);
    public bool IsReadOnly => false;

    protected ShardedSkipList(int shardCount, Func<T, int> shardFunction)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(shardCount, 1);

        _shardFunction = shardFunction ?? throw new ArgumentNullException(nameof(shardFunction));
        _shards = Enumerable.Range(0, shardCount).Select(_ => new TShard()).ToList();
    }

    protected TShard GetShard(T value) => _shards[_shardFunction(value)];

    public virtual void Add(T item) => GetShard(item).Add(item);

    public void Clear()
    {
        foreach (var shard in _shards)
        {
            shard.Clear();
        }
    }

    public virtual bool Contains(T item) => GetShard(item).Contains(item);
    
    public virtual void CopyTo(T[] array, int arrayIndex)
    {
        var allItems = new List<T>();
        foreach (var shard in _shards)
        {
            allItems.AddRange(shard.ToArray());
        }
        allItems.CopyTo(array, arrayIndex);
        Array.Sort(array);
    }

    public virtual bool Remove(T item) => GetShard(item).Remove(item);

    public void AddRange(IEnumerable<T> collection)
    {
        var shards = collection
            .GroupBy(_shardFunction)
            .OrderBy(group => group.Key)
            .Select(group => group.ToArray())
            .ToList();

        foreach (var shard in shards)
        {
            GetShard(shard[0]).AddRange(shard);
        }
    }

    public virtual T Find(T value) => GetShard(value).Find(value);

    public virtual T? FindOrDefault(T value, T? defaultValue = default) =>
        GetShard(value).FindOrDefault(value, defaultValue);

    public virtual IEnumerator<T> GetEnumerator() => _shards.SelectMany(x => x).GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
