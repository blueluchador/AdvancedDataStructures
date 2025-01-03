namespace AdvancedDataStructures.Tests.Lookups.SkipLists;

public class MyObject : IComparable<MyObject>, IEquatable<MyObject>
{
    public int Id { get; set; }
    public string? Name { get; init; }

    public int CompareTo(MyObject? other) => other == null ? 1 : Id.CompareTo(other.Id);

    public bool Equals(MyObject? other) => other != null && Id == other.Id;

    public override bool Equals(object? obj) => obj is MyObject otherEntity && Equals(otherEntity);

    // ReSharper disable once NonReadonlyMemberInGetHashCode
    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => $"Id: {Id}, Name: {Name}";
}