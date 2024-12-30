using System.Collections;

namespace AdvancedDataStructures.Lookups;

public class SkipList<T> : ICollection<T?>
{
    private const double Probability = 0.5;
    private static readonly Random Random = new(); 

    private class Node(T? value)
    {
        public T? Value { get; } = value;
        public readonly Dictionary<int, Node> Forward = new();
    }

    private Node _head = new(default);
    private int _maxLevel;
    private int _count;
    
    public int Count => _count;
    public bool IsReadOnly { get; }

    private int RandomLevel()
    {
        int level = 1;
        while (Random.NextDouble() < Probability)
        {
            level++;
        }
        return Math.Min(level, _maxLevel); 
    }

    public void Add(T? value)
    {
        var current = _head;
        var update = new List<Node>();

        // Find the insertion point at each level
        for (int i = _maxLevel; i >= 0; i--)
        {
            while (current.Forward.ContainsKey(i) && Comparer<T>.Default.Compare(current.Forward[i].Value, value) < 0)
            {
                current = current.Forward[i];
            }
            update.Add(current); 
        }

        // Generate a random level for the new node
        int level = RandomLevel();

        // If the new level is greater than the current max level, increase the max level
        if (level > _maxLevel)
        {
            _maxLevel = level;
        }

        // Create the new node
        Node newNode = new Node(value);

        // Connect the new node to the forward pointers of the update nodes
        for (int i = 0; i <= level; i++)
        {
            if (update[i].Forward.ContainsKey(i))
            {
                newNode.Forward[i] = update[i].Forward[i];
            }
            update[i].Forward[i] = newNode;
        }

        _count++;
    }

    public void Clear()
    {
        _head = new Node(default);
        _maxLevel = 0;
        _count = 0;
    }

    public bool Contains(T? value)
    {
        var current = _head;

        for (int i = _maxLevel; i >= 0; i--)
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

    public void CopyTo(T?[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        
        if (arrayIndex < 0 || arrayIndex >= array.Length)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < _count)
            throw new ArgumentException("Destination array is not long enough.");

        var current = _head.Forward.ContainsKey(0) ? _head.Forward[0] : null;
        int i = arrayIndex;
        while (current != null && i < array.Length)
        {
            array[i++] = current.Value;
            current = current.Forward.GetValueOrDefault(0);
        }
    }

    public T? Find(T value)
    {
        var current = _head;

        for (int i = _maxLevel; i >= 0; i--)
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
        try
        {
            return Find(value);
        }
        catch (KeyNotFoundException)
        {
            return defaultValue;
        }
    }

    public bool Remove(T? value)
    {
        var current = _head;
        var update = new List<Node>();

        // Find the node to be removed at each level
        for (int i = _maxLevel; i >= 0; i--)
        {
            while (current.Forward.ContainsKey(i) && Comparer<T>.Default.Compare(current.Forward[i].Value, value) < 0)
            {
                current = current.Forward[i];
            }
            update.Add(current);
        }

        // Find the node to be removed
        current = current.Forward.GetValueOrDefault(0);

        if (current == null || !current.Value!.Equals(value)) return false;
        
        // Remove the node from each level
        for (int i = 0;
             i <= _maxLevel && update[i].Forward.ContainsKey(i) && update[i].Forward[i] == current;
             i++)
        {
            update[i].Forward[i] = current.Forward[i];
        }
        return true;
    }
    
    public IEnumerator<T?> GetEnumerator()
    {
        var current = _head.Forward.GetValueOrDefault(0); 
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