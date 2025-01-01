namespace AdvancedDataStructures.Lookups.SkipLists;

public interface IComparableSkipList<T> : ISkipList<T> where T : IComparable<T>, IEquatable<T>
{
    bool Update(T newValue);
}