namespace AdvancedDataStructures.Lookups.SkipLists;

public interface ISkipList<T> : ICollection<T>
{
    void AddRange(IEnumerable<T> collection);
    T Find(T value);
    T? FindOrDefault(T value, T? defaultValue = default!);
}