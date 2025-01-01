using System.Collections;
using System.Collections.Concurrent;

namespace AdvancedDataStructures.Lookups.SkipLists;

public class SkipList<T> : ISkipList<T>
{
    private const double Probability = 0.5;
    private static readonly Random Random = new(); 

    protected class Node(T value)
    {
        public T Value { get; set; } = value;
        public readonly Dictionary<int, Node> Forward = new();
    }
    
    private int _count;

    protected Node Head = new(default!);
    protected int MaxLevel;
    
    // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
    public int Count => _count;
    public bool IsReadOnly => false;
    
    private readonly object _lock = new();

    public SkipList() {}
    
    public SkipList(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
    
        var items = collection.AsParallel().OrderBy(x => x).ToList();
        items.Sort();
    
        BulkAdd(items);
    }
    // public SkipList(IEnumerable<T> collection)
    // {
    //     const int maxBatchSize = 10_000;
    //     
    //     ArgumentNullException.ThrowIfNull(collection);
    //
    //     // Sort the collection and convert it to a list
    //     var items = collection.AsParallel().OrderBy(x => x).ToList();
    //
    //     int totalItems = items.Count;
    //     int calculatedBatchSize = (int)Math.Ceiling((double)totalItems / Environment.ProcessorCount);
    //
    //     // Ensure batch size does not exceed the maximum allowed size
    //     int batchSize = Math.Min(calculatedBatchSize, maxBatchSize);
    //
    //     // Determine chunk size
    //     int chunkSize = batchSize * Environment.ProcessorCount;
    //
    //     // Process in chunks
    //     for (int i = 0; i < totalItems; i += chunkSize)
    //     {
    //         int end = Math.Min(i + chunkSize, totalItems);
    //         BulkAdd(items, i, end);
    //     }
    // }
    
    private void BulkAdd(List<T> items)
    {
        int count = items.Count;
        int batchSize = (int)Math.Ceiling((double)count / Environment.ProcessorCount);
        if (batchSize > 10_000) batchSize = 10_000;
    
        var partitioner = Partitioner.Create(0, count, batchSize);
    
        Parallel.ForEach(partitioner, range =>
        {
            for (int i = range.Item1; i < range.Item2; i++)
            {
                Add(items[i]);
            }
        });
    }

    private int RandomLevel()
    {
        int level = 1;
        while (Random.NextDouble() < Probability)
        {
            level++;
        }
        return Math.Min(level, MaxLevel); 
    }

    public virtual void Add(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var update = new Node[MaxLevel + 1];
        var current = Head;
        
        // Find the insertion point at each level
        for (int i = MaxLevel; i >= 0; i--)
        {
            while (current.Forward.TryGetValue(i, out var next) && Comparer<T>.Default.Compare(next.Value, value) < 0)
            {
                current = next;
            }
            update[i] = current;
        }
        
        // Generate a random level for the new node
        int level = RandomLevel();

        lock (_lock)
        {
            if (level > MaxLevel)
            {
                for (int i = MaxLevel + 1; i <= level; i++)
                {
                    update[i] = Head;
                }
                MaxLevel = level;
            }
            
            var newNode = new Node(value);
            for (int i = 0; i <= level; i++)
            {
                if (update[i].Forward.TryGetValue(i, out var next))
                {
                    newNode.Forward[i] = next;
                }
                update[i].Forward[i] = newNode;
            }
            
            _count++;
        }
    }

    public void Clear()
    {
        Head = new Node(default!);
        MaxLevel = 0;
        _count = 0;
    }

    public bool Contains(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        var current = Head;

        for (int i = MaxLevel; i >= 0; i--)
        {
            while (current.Forward.ContainsKey(i) && Comparer<T>.Default.Compare(current.Forward[i].Value, value) < 0)
            {
                current = current.Forward[i];
            }
        }

        // Move to the next node horizontally
        current = current.Forward.GetValueOrDefault(0);

        return current != null && current.Value!.Equals(value);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        
        if (arrayIndex < 0 || arrayIndex >= array.Length)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < _count)
            throw new ArgumentException("Destination array is not long enough.");

        var current = Head.Forward.GetValueOrDefault(0);
        int i = arrayIndex;
        while (current != null && i < array.Length)
        {
            array[i++] = current.Value;
            current = current.Forward.GetValueOrDefault(0);
        }
    }

    public T Find(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        var current = Head;

        for (int i = MaxLevel; i >= 0; i--)
        {
            while (current.Forward.ContainsKey(i) && Comparer<T>.Default.Compare(current.Forward[i].Value, value) < 0)
            {
                current = current.Forward[i];
            }
        }

        // Move to the next node horizontally
        current = current.Forward.GetValueOrDefault(0);
        
        if (current == null) throw new KeyNotFoundException();
        return current.Value;
    }

    public T? FindOrDefault(T value, T? defaultValue = default)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        try
        {
            return Find(value);
        }
        catch (KeyNotFoundException)
        {
            return defaultValue;
        }
    }

    public bool Remove(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        if (_count == 0) return false;
    
        var current = Head;
        var update = new Node[MaxLevel + 1]; // Create an array with a size of MaxLevel + 1

        // Find the node to be removed at each level
        for (int i = MaxLevel; i >= 0; i--)
        {
            while (current.Forward.ContainsKey(i) && Comparer<T>.Default.Compare(current.Forward[i].Value, value) < 0)
            {
                current = current.Forward[i];
            }
            update[i] = current; // Track the node at this level
        }

        // Find the node to be removed
        current = current.Forward.GetValueOrDefault(0);

        if (current == null || !current.Value!.Equals(value)) return false;

        // Remove the node from each level
        for (int i = 0; i <= MaxLevel; i++)
        {
            if (update[i].Forward.ContainsKey(i) && update[i].Forward[i] == current)
            {
                update[i].Forward[i] = current.Forward.GetValueOrDefault(i)!;
            }
        }

        // Decrease MaxLevel if the highest level is empty
        while (MaxLevel > 0 && Head.Forward.GetValueOrDefault(MaxLevel) == null)
        {
            MaxLevel--;
        }

        _count--;
        if (_count == 0) Clear(); // Ensure the skip list is reset
        return true;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        var current = Head.Forward.GetValueOrDefault(0); 
        while (current != null)
        {
            yield return current.Value;
            current = current.Forward.GetValueOrDefault(0);
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}