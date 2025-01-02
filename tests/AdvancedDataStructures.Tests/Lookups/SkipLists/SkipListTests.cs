using AdvancedDataStructures.Lookups.SkipLists;

namespace AdvancedDataStructures.Tests.Lookups.SkipLists;

public class SkipListTests//(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ParameterizedConstructor_WithInitialItems_ShouldInitializeCorrectly()
    {
        // Arrange
        int[] initialItems = [3, 7, 2, 5, 8, 1, 6, 4, 9, 10];

        // Act
        var skipList = new SkipList<int>(initialItems);

        // Assert
        foreach (int item in initialItems)
        {
            Assert.Contains(item, skipList);
        }

        Assert.Equal(initialItems.Length, skipList.Count);
    }

    [Fact]
    public void Add_WithDuplicateItems_ShouldIncreaseCount()
    {
        // Arrange
        var skipList = new SkipList<int> { 5, 3, 8 };

        // Act (add a duplicate)
        skipList.Add(5);

        // Assert
        Assert.Contains(5, skipList);
        Assert.Equal(4, skipList.Count);
    }

    [Fact]
    public void Add_WithNullValue_ShouldThrowArgumentNullException()
    {
        // Arrange
        var skipList = new SkipList<string>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => skipList.Add(null!));
    }
    
    [Fact]
    public void AddRange_WithNewItems_ShouldAddCorrectly()
    {
        // Arrange
        int[] initialItems = [3, 7, 2, 5, 8, 1, 4, 9, 10, 12, 13];
        int[] newItems = [ 6, 14, 11 ];

        // Act
        var skipList = new SkipList<int>(initialItems);
        skipList.AddRange(newItems);

        // Assert
        foreach (int item in newItems)
        {
            Assert.Contains(item, skipList);
        }

        Assert.Equal(initialItems.Length + newItems.Length, skipList.Count);
    }

    [Fact]
    public void Remove_ExistingItem_ShouldRemoveCorrectly()
    {
        // Arrange
        var skipList = new SkipList<int> { 1, 2, 3, 4, 5 };

        // Act
        bool result = skipList.Remove(3);

        // Assert
        Assert.True(result);
        Assert.DoesNotContain(3, skipList);
        Assert.Equal(4, skipList.Count);
    }

    [Fact]
    public void Remove_FromEmptyList_ShouldReturnFalse()
    {
        // Arrange
        var skipList = new SkipList<int>();

        // Act
        bool result = skipList.Remove(5);

        // Assert
        Assert.False(result);
        Assert.Empty(skipList);
    }

    [Fact]
    public void Contains_WithNonExistentItem_ShouldReturnFalse()
    {
        // Arrange
        var skipList = new SkipList<int> { 1, 3, 5 };

        // Act
        bool result = skipList.Contains(2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Find_WithExistingItem_ShouldReturnCorrectValue()
    {
        // Arrange
        var skipList = new SkipList<int> { 1, 2, 3, 7, 5 };

        // Act
        int result = skipList.Find(3);

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void Find_WithNonExistentItem_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var skipList = new SkipList<int> { 1, 2, 3, 4, 5 };

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => skipList.Find(10));
    }

    [Fact]
    public void FindOrDefault_WithNonExistentItem_ShouldReturnDefaultValue()
    {
        // Arrange
        var skipList = new SkipList<int> { 1, 2, 3 };

        // Act
        int result = skipList.FindOrDefault(10, -1);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public void CopyTo_WithValidParameters_ShouldCopyItemsCorrectly()
    {
        // Arrange
        var skipList = new SkipList<int> { 1, 3, 2 };
        int[] array = new int[5];

        // Act
        skipList.CopyTo(array, 2);

        // Assert
        Assert.Equal([0, 0, 1, 2, 3], array);
    }

    [Fact]
    public void CopyTo_WithInvalidParameters_ShouldThrowException()
    {
        // Arrange
        var skipList = new SkipList<int> { 1, 2, 3 };
        int[] array = new int[2];

        // Act & Assert
        Assert.Throws<ArgumentException>(() => skipList.CopyTo(array, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => skipList.CopyTo(array, -1));
    }

    [Fact]
    public void Clear_WhenCalled_ShouldEmptyTheList()
    {
        // Arrange
        var skipList = new SkipList<int> { 1, 2, 3 };

        // Act
        skipList.Clear();

        // Assert
        Assert.Empty(skipList);
        Assert.DoesNotContain(1, skipList);
    }

    [Fact]
    public void SkipList_WithDuplicates_ShouldHandleCorrectly()
    {
        // Arrange
        var skipList = new SkipList<int> { 1, 2, 2, 3 };

        // Assert
        Assert.Equal(4, skipList.Count);
        Assert.Contains(2, skipList);
    }

    [Fact]
    public void Enumerator_WithItems_ShouldIterateCorrectly()
    {
        // Arrange
        int[] items = [4, 1, 7, 3, 2, 6, 5, 8, 9, 10];
        var skipList = new SkipList<int>(items);

        // Act
        var enumeratedItems = skipList.ToList();

        // Assert
        Assert.Equal(items.OrderBy(x => x).ToList(), enumeratedItems);
    }
    
//     [Fact]
//     public void ContainsAndFind_PerformanceTest_ForMillionRecords()
//     {
//         // Arrange
//         const int rangePerShard = 100_000;
//         const int numShards = 10;
//
//         var shardedSkipList = new ShardedSkipList<int, SkipList<int>>(
//             numShards,
//             value => (value - 1) / rangePerShard);
//         
//         shardedSkipList.AddRange(Enumerable.Range(1, 1_000_000));
//
//         var stopwatch = new Stopwatch();
//         
//         // Act
//         stopwatch.Start();
//         bool containsResult = shardedSkipList.Contains(500_000);
//         stopwatch.Stop();
//         long containsTime = stopwatch.ElapsedMilliseconds;
//         
//         stopwatch.Reset();
//         stopwatch.Start();
//         int findResult = shardedSkipList.Find(500_000);
//         stopwatch.Stop();
//         long findTime = stopwatch.ElapsedMilliseconds;
//         
//         // Assert
//         Assert.True(containsResult);
//         Assert.Equal(500_000, findResult);
//         Assert.Equal(1_000_000, shardedSkipList.Count);
//         
//         // Log performance (optional, for analysis)
//         testOutputHelper.WriteLine($"Contains time: {containsTime} ms");
//         testOutputHelper.WriteLine($"Find time: {findTime} ms");
//     }
}
