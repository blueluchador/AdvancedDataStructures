using AdvancedDataStructures.Lookups.SkipLists;

namespace AdvancedDataStructures.Tests.Lookups.SkipLists;

public class ShardedSkipListTests
{
    private readonly Func<int, int> _shardFunction = value => value % 3;

    // [Fact]
    // public void Add_WithValidItem_ShouldAddToCorrectShard()
    // {
    //     var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction) { 3 };
    //     // Assert.True(skipList.GetShard(3).Contains(3));
    //     // Assert.Equal(1, skipList.GetShard(3).Count);
    // }
    
    [Fact]
    public void Constructor_WithNumShardsLessThanOne_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new ShardedSkipList<int, SkipList<int>>(0, null!));
    }

    [Fact]
    public void Constructor_WithNullShardFunction_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new ShardedSkipList<int, SkipList<int>>(3, null!));
    }

    [Fact]
    public void Clear_ShouldClearAllShards()
    {
        // Arrange
        var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction);
        skipList.AddRange([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);

        // Act
        skipList.Clear();

        // Assert
        Assert.Empty(skipList);
    }

    [Fact]
    public void Contains_WithExistingItem_ShouldReturnTrue()
    {
        // Arrange
        var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction);
        skipList.Add(3);

        // Act
        bool result = skipList.Contains(3);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Contains_WithNonExistingItem_ShouldReturnFalse()
    {
        // Arrange
        var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction);

        // Act
        bool result = skipList.Contains(4);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CopyTo_WithValidArray_ShouldCopyAllItems()
    {
        // Arrange
        var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction) { 3, 2, 1 };
        
        // Act
        int[] array = new int[5];
        skipList.CopyTo(array, 0);

        // Assert
        Assert.Equal([0, 0, 1, 2, 3], array);
    }

    [Fact]
    public void Remove_ExistingItem_ShouldReturnTrue()
    {
        // Arrange
        var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction);
        skipList.Add(3);

        // Act
        bool result = skipList.Remove(3);

        // Assert
        Assert.True(result);
        Assert.DoesNotContain(3, skipList);
    }

    [Fact]
    public void Remove_NonExistingItem_ShouldReturnFalse()
    {
        // Arrange
        var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction);

        // Act
        bool result = skipList.Remove(4);

        // Assert
        Assert.False(result);
    }

    // [Fact]
    // public void AddRange_WithNewItems_ShouldAddToCorrectShards()
    // {
    //     var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction);
    //
    //     skipList.AddRange(new[] { 1, 2, 3 });
    //
    //     // Assert.True(skipList.GetShard(1).Contains(1));
    //     // Assert.True(skipList.GetShard(2).Contains(2));
    //     // Assert.True(skipList.GetShard(3).Contains(3));
    // }

    [Fact]
    public void Find_ExistingItem_ShouldReturnCorrectItem()
    {
        // Arrange
        var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction) { 3, 1, 2, 7, 9, 10 };

        // Act
        int result = skipList.Find(3);

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void Find_NonExistingItem_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction) { 3, 1, 2, 7, 9, 10 };

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => skipList.Find(4));
    }

    [Fact]
    public void FindOrDefault_NonExistingItem_ShouldReturnDefault()
    {
        // Arrange
        var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction) { 3, 1, 2, 9, 7, 10 };

        // Act
        int result = skipList.FindOrDefault(4);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetEnumerator_ShouldReturnItemsFromAllShards()
    {
        // Arrange
        var skipList = new ShardedSkipList<int, SkipList<int>>(3, _shardFunction) { 2, 3, 1 };

        // Act
        var items = skipList.ToList();

        // Assert
        Assert.Equal(new[] { 1, 2, 3 }, items);
    }
}
