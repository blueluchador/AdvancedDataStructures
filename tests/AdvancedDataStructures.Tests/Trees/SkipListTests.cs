using AdvancedDataStructures.Lookups;

namespace AdvancedDataStructures.Tests.Trees;

public class MyObject : IComparable<MyObject>, IEquatable<MyObject>
{
    public int Id { get; init;  }
    public string? Name { get; init; }

    public int CompareTo(MyObject? other)
    {
        return other == null ? 1 : Id.CompareTo(other.Id);
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}";
    }

    public bool Equals(MyObject? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((MyObject)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}

public class SkipListTests
{
    [Fact]
    public void Test_Add_And_Contains_Int()
    {
        var skipList = new SkipList<int> { 10, 20, 15 };
        Assert.Contains(10, skipList);
        Assert.Contains(15, skipList);
        Assert.Contains(20, skipList);
        Assert.DoesNotContain(30, skipList);
    }

    [Fact]
    public void Test_Remove_Int()
    {
        var skipList = new SkipList<int> { 10, 20 };
        Assert.True(skipList.Remove(10));
        Assert.DoesNotContain(10, skipList);
        Assert.Contains(20, skipList);
        Assert.False(skipList.Remove(30));
    }

    [Fact]
    public void Test_Find_Int()
    {
        var skipList = new SkipList<int> { 10, 20 };
        Assert.Equal(10, skipList.Find(10));
        Assert.Equal(20, skipList.Find(20));
        Assert.Equal(-1, skipList.FindOrDefault(30, -1));
    }

    [Fact]
    public void Test_Find_Int_Throws_Exception()
    {
        var skipList = new SkipList<int> { 10, 15, 20 };
        Assert.Throws<KeyNotFoundException>(() => skipList.Find(30));
    }

    [Fact]
    public void Test_Add_And_Contains_MyObject()
    {
        var skipList = new SkipList<MyObject>
        {
            new() { Id = 1, Name = "Some name" },
            new() { Id = 2, Name = "Some other name" },
            new() { Id = 3, Name = "Some ordinary name" }
        };
        Assert.Contains(new MyObject { Id = 1 }, skipList);
        Assert.DoesNotContain(new MyObject { Id = 4 }, skipList);
    }
    
    [Fact]
    public void Test_Find_MyObject()
    {
        var skipList = new SkipList<MyObject>
        {
            new() { Id = 1, Name = "Some name" },
            new() { Id = 2, Name = "Some other name" }
        };
        var result = skipList.FindOrDefault(new MyObject { Id = 2 });
        Assert.NotNull(result);
        Assert.Equal("Some other name", result.Name);
    
        var notFound = skipList.FindOrDefault(new MyObject { Id = 3 });
        Assert.Null(notFound);
    }
    
    [Fact]
    public void Test_Remove_MyObject()
    {
        var skipList = new SkipList<MyObject>
        {
            new() { Id = 1, Name = "Some name" },
            new() { Id = 2, Name = "Some other name" }
        };
        Assert.True(skipList.Remove(new MyObject { Id = 1 }));
        Assert.DoesNotContain(new MyObject { Id = 1 }, skipList);
        Assert.Contains(new MyObject { Id = 2 }, skipList);
        Assert.False(skipList.Remove(new MyObject { Id = 3 }));
    }

    [Fact]
    public void Test_Enumeration()
    {
        var skipList = new SkipList<int> { 10, 20, 15 };
        var sorted = skipList.ToList();
        Assert.Equal(new[] { 10, 15, 20 }, sorted);
    }

    [Fact]
    public void Test_Empty_SkipList()
    {
        var skipList = new SkipList<int>();
        Assert.DoesNotContain(10, skipList);
        Assert.Equal(-1, skipList.FindOrDefault(10, -1));
        Assert.False(skipList.Remove(10));
    }

    [Fact]
    public void Test_Add_Duplicate_Int()
    {
        var skipList = new SkipList<int> { 10, 10 };
        int count = skipList.Count;
        Assert.Equal(2, count);
    }

    [Fact]
    public void Test_Add_Duplicate_MyObject()
    {
        var skipList = new SkipList<MyObject>();
    
        var myObject = new MyObject { Id = 1, Name = "Some name" };
        skipList.Add(myObject);
        skipList.Add(myObject);
    
        int count = skipList.Count;
        Assert.Equal(2, count);
    }

    [Fact]
    public void Test_Boundary_Int()
    {
        var skipList = new SkipList<int>();
        skipList.Add(Int32.MinValue);
        skipList.Add(Int32.MaxValue);
        Assert.Contains(Int32.MinValue, skipList);
        Assert.Contains(Int32.MaxValue, skipList);
    }
}
