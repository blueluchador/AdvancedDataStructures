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
        public readonly object NodeLock = new();
    }
    
    private int _count;

    protected Node Head = new(default!);
    protected int MaxLevel;
    
    // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
    public int Count => _count;
    public bool IsReadOnly => false;
    
    private readonly object _lock = new();

    public SkipList() {}
    
    public SkipList(IEnumerable<T>? collection)
    {
        AddRange(collection);
    }

    public void AddRange(IEnumerable<T>? collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        var items = collection.AsParallel().OrderBy(x => x).ToList();
        BulkAdd(items);
    }
    
    private void BulkAdd(List<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        // Sort the input items
        items = items.AsParallel().OrderBy(x => x).ToList();

        var current = Head;
        var newHead = new Node(default!); // Temporary head for the new skip list
        var tempCurrent = newHead;

        int maxLevel = MaxLevel;

        // Merge the existing skip list with the new sorted items
        foreach (var item in items)
        {
            while (current.Forward.TryGetValue(0, out var next) && Comparer<T>.Default.Compare(next.Value, item) < 0)
            {
                tempCurrent.Forward[0] = next;
                tempCurrent = next;
                current = next;
            }

            var newNode = new Node(item);
            tempCurrent.Forward[0] = newNode;
            tempCurrent = newNode;
        }

        // Add remaining nodes from the original skip list
        while (current.Forward.TryGetValue(0, out var next))
        {
            tempCurrent.Forward[0] = next;
            tempCurrent = next;
            current = next;
        }

        lock (_lock)
        {
            // Replace the original head with the new merged list
            Head = newHead;
            MaxLevel = maxLevel;
            _count += items.Count;
        }
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

        lock (Head.NodeLock) // Lock only during level adjustment
        {
            if (level > MaxLevel)
            {
                for (int i = MaxLevel + 1; i <= level; i++)
                {
                    update[i] = Head;
                }
                MaxLevel = level;
            }
        }

        var newNode = new Node(value);
        for (int i = 0; i <= level; i++)
        {
            lock (update[i].NodeLock) // Lock only the affected nodes
            {
                if (update[i].Forward.TryGetValue(i, out var next))
                {
                    newNode.Forward[i] = next;
                }
                update[i].Forward[i] = newNode;
            }
        }

        Interlocked.Increment(ref _count);
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

        // Verify the found node matches the search value
        if (current == null || Comparer<T>.Default.Compare(current.Value, value) != 0)
        {
            throw new KeyNotFoundException();
        }

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