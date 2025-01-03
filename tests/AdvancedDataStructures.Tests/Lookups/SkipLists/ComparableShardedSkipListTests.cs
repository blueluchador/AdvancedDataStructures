using AdvancedDataStructures.Lookups.SkipLists;

namespace AdvancedDataStructures.Tests.Lookups.SkipLists;

public class ComparableShardedSkipListTests
{
    private readonly Func<MyObject, int> _shardFunction = obj => obj.Id % 3;

    [Fact]
    public void Constructor_WithNumShardsLessThanOne_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new ComparableShardedSkipList<MyObject>(0, null!));
    }

    [Fact]
    public void Constructor_WithNullShardFunction_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new ComparableShardedSkipList<MyObject>(3, null!));
    }

    [Fact]
    public void Clear_ShouldClearAllShards()
    {
        // Arrange
        var skipList = new ComparableShardedSkipList<MyObject>(3, _shardFunction);
        skipList.AddRange([
            new MyObject { Id = 1, Name = "One" },
            new MyObject { Id = 2, Name = "Two" },
            new MyObject { Id = 3, Name = "Three" },
            new MyObject { Id = 4, Name = "Four" },
            new MyObject { Id = 5, Name = "Five" }
        ]);

        // Act
        skipList.Clear();

        // Assert
        Assert.Empty(skipList);
    }

    [Fact]
    public void Contains_WithExistingItem_ShouldReturnTrue()
    {
        // Arrange
        var skipList = new ComparableShardedSkipList<MyObject>(3, _shardFunction);
        var obj = new MyObject { Id = 3, Name = "Three" };
        skipList.Add(obj);

        // Act
        bool result = skipList.Contains(obj);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Contains_WithNonExistingItem_ShouldReturnFalse()
    {
        // Arrange
        var skipList = new ComparableShardedSkipList<MyObject>(3, _shardFunction);

        // Act
        bool result = skipList.Contains(new MyObject { Id = 4, Name = "Four" });

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CopyTo_WithValidArray_ShouldCopyAllItems()
    {
        // Arrange
        var skipList = new ComparableShardedSkipList<MyObject>(3, _shardFunction)
        {
            new() { Id = 3, Name = "Three" },
            new() { Id = 2, Name = "Two" },
            new() { Id = 1, Name = "One" }
        };

        // Act
        var array = new MyObject[5];
        skipList.CopyTo(array, 2);

        // Assert
        Assert.Equal([
            null!,
            null!,
            new MyObject { Id = 1, Name = "One" },
            new MyObject { Id = 2, Name = "Two" },
            new MyObject { Id = 3, Name = "Three" }
        ], array);
    }

    [Fact]
    public void Remove_ExistingItem_ShouldReturnTrue()
    {
        // Arrange
        var skipList = new ComparableShardedSkipList<MyObject>(3, _shardFunction);
        var obj = new MyObject { Id = 3, Name = "Three" };
        skipList.Add(obj);

        // Act
        bool result = skipList.Remove(obj);

        // Assert
        Assert.True(result);
        Assert.DoesNotContain(obj, skipList);
    }

    [Fact]
    public void Remove_NonExistingItem_ShouldReturnFalse()
    {
        // Arrange
        var skipList = new ComparableShardedSkipList<MyObject>(3, _shardFunction);

        // Act
        bool result = skipList.Remove(new MyObject { Id = 4, Name = "Four" });

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Update_ExistingItem_ShouldReturnTrue()
    {
        // Arrange
        var skipList = new ComparableShardedSkipList<MyObject>(3, _shardFunction);
        var obj = new MyObject { Id = 3, Name = "Old" };
        skipList.Add(obj);

        var updatedObj = new MyObject { Id = 3, Name = "Updated" };

        // Act
        bool result = skipList.Update(updatedObj);

        // Assert
        Assert.True(result);
        var retrievedObj = skipList.Find(new MyObject { Id = 3 });
        Assert.Equal("Updated", retrievedObj.Name);
    }

    [Fact]
    public void Update_NonExistingItem_ShouldReturnFalse()
    {
        // Arrange
        var skipList = new ComparableShardedSkipList<MyObject>(3, _shardFunction);

        // Act
        bool result = skipList.Update(new MyObject { Id = 4, Name = "Four" });

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetEnumerator_ShouldReturnItemsFromAllShards()
    {
        // Arrange
        var skipList = new ComparableShardedSkipList<MyObject>(3, _shardFunction)
        {
            new() { Id = 2, Name = "Two" },
            new() { Id = 3, Name = "Three" },
            new() { Id = 1, Name = "One" }
        };

        // Act
        var items = skipList.ToList();

        // Assert
        Assert.Equal(new[]
        {
            new MyObject { Id = 1, Name = "One" },
            new MyObject { Id = 2, Name = "Two" },
            new MyObject { Id = 3, Name = "Three" }
        }, items);
    }
}