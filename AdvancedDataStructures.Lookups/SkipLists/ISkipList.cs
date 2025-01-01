namespace AdvancedDataStructures.Lookups.SkipLists;

public interface ISkipList<T> : ICollection<T>
{
    T Find(T value);
    T? FindOrDefault(T value, T? defaultValue = default!);
}