namespace AdvancedDataStructures.Lookups.SkipLists;

public class ComparableSkipList<T> : SkipList<T>, IComparableSkipList<T> where T : IComparable<T>, IEquatable<T>
{
    public ComparableSkipList() {}
    
    public ComparableSkipList(IEnumerable<T> collection) : base(collection) {}
    
    public override void Add(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        if (Contains(value))
        {
            throw new InvalidOperationException("Duplicate values are not allowed in ComparableSkipList.");
        }

        base.Add(value);
    }
    
    public bool Update(T newValue)
    {
        ArgumentNullException.ThrowIfNull(newValue);
        
        // Start from the head node
        var current = Head;
        bool found = false;

        // Traverse the skip list to find the first node to replace
        for (int i = MaxLevel; i >= 0; i--)
        {
            while (current.Forward.ContainsKey(i) && current.Forward[i].Value.CompareTo(newValue) < 0)
            {
                current = current.Forward[i];
            }
        }

        // Move to the first matching node
        current = current.Forward.GetValueOrDefault(0);

        // Update all matching nodes
        while (current != null && current.Value.Equals(newValue))
        {
            current.Value = newValue;
            found = true;
            current = current.Forward.GetValueOrDefault(0); // Move to the next node
        }

        return found;
    }
}