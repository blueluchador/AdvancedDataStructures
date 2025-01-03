using AdvancedDataStructures.Lookups.SkipLists;

namespace AdvancedDataStructures.Tests.Lookups.SkipLists;

public class ComparableSkipListTests
{
    [Fact]
    public void ParameterizedConstructor_WithInitialObjects_ShouldInitializeCorrectly()
    {
        // Arrange
        var initialObjects = new[]
        {
            new MyObject { Id = 1, Name = "Object1" },
            new MyObject { Id = 2, Name = "Object2" },
            new MyObject { Id = 3, Name = "Object3" }
        };

        // Act
        var skipList = new ComparableSkipList<MyObject>(initialObjects);

        // Assert
        foreach (var obj in initialObjects)
        {
            Assert.Contains(obj, skipList);
        }

        Assert.Equal(initialObjects.Length, skipList.Count);
    }

    [Fact]
    public void Add_UniqueValue_ShouldAddSuccessfully()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        var obj = new MyObject { Id = 1, Name = "Object1" };

        // Act
        skipList.Add(obj);

        // Assert
        Assert.True(skipList.Contains(obj), "The object was not added to the skip list.");
    }

    [Fact]
    public void Add_DuplicateValue_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        var obj = new MyObject { Id = 1, Name = "Object1" };
        skipList.Add(obj);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => skipList.Add(obj));
        Assert.Equal("Duplicate values are not allowed in ComparableSkipList.", exception.Message);
    }

    [Fact]
    public void Add_NullValue_ShouldThrowArgumentNullException()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => skipList.Add(null!));
    }
    
    [Fact]
    public void AddRange_WithUniqueObjects_ShouldAddCorrectly()
    {
        // Arrange
        var initialObjects = new[]
        {
            new MyObject { Id = 1, Name = "Object1" },
            new MyObject { Id = 2, Name = "Object2" },
            new MyObject { Id = 3, Name = "Object3" }
        };
        var skipList = new ComparableSkipList<MyObject>(initialObjects);

        // Act
        var newObjects = new[]
        {
            new MyObject { Id = 4, Name = "Object4" },
            new MyObject { Id = 5, Name = "Object5" }
        };
        skipList.AddRange(newObjects);

        // Assert
        foreach (var obj in newObjects)
        {
            Assert.True(skipList.Contains(obj), $"The object {obj} was not added to the skip list.");
        }
        Assert.Equal(initialObjects.Length + newObjects.Length, skipList.Count);
    }
    
    [Fact]
    public void Update_WithExistingValue_ShouldUpdateSuccessfully()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        var oldObj = new MyObject { Id = 1, Name = "OldObject" };
        var updatedObj = new MyObject { Id = 1, Name = "UpdatedObject" };
        skipList.Add(oldObj);

        // Act
        bool result = skipList.Update(updatedObj);

        // Assert
        Assert.True(result, "The update operation failed for an existing value.");
        Assert.True(skipList.Contains(updatedObj), "The updated object was not found in the skip list.");
    }

    [Fact]
    public void Update_NonExistingValue_ShouldReturnFalse()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        var obj = new MyObject { Id = 1, Name = "NonExistingObject" };

        // Act
        bool result = skipList.Update(obj);

        // Assert
        Assert.False(result, "Update returned true for a non-existing value.");
    }

    [Fact]
    public void Remove_ExistingValue_ShouldRemoveSuccessfully()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        var obj = new MyObject { Id = 1, Name = "ObjectToRemove" };
        skipList.Add(obj);

        // Act
        bool result = skipList.Remove(new MyObject { Id = 1 });

        // Assert
        Assert.True(result, "The value was not removed from the skip list.");
        Assert.False(skipList.Contains(obj), "The removed value is still in the skip list.");
    }

    [Fact]
    public void Remove_NonExistingValue_ShouldReturnFalse()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        var obj = new MyObject { Id = 1, Name = "NonExistingObject" };

        // Act
        bool result = skipList.Remove(obj);

        // Assert
        Assert.False(result, "Remove returned true for a non-existing value.");
    }

    [Fact]
    public void Remove_NullValue_ShouldThrowArgumentNullException()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => skipList.Remove(null!));
    }

    [Fact]
    public void Contains_ExistingValue_ShouldReturnTrue()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        var obj = new MyObject { Id = 1, Name = "ExistingObject" };
        skipList.Add(obj);

        // Act
        bool result = skipList.Contains(obj);

        // Assert
        Assert.True(result, "Contains returned false for an existing value.");
    }

    [Fact]
    public void Contains_NonExistingValue_ShouldReturnFalse()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        var obj = new MyObject { Id = 1, Name = "NonExistingObject" };

        // Act
        bool result = skipList.Contains(obj);

        // Assert
        Assert.False(result, "Contains returned true for a value not in the skip list.");
    }

    [Fact]
    public void Count_EmptyList_ShouldBeZero()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();

        // Act
        int count = skipList.Count;

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void Count_NonEmptyList_ShouldReturnCorrectCount()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        skipList.Add(new MyObject { Id = 1, Name = "Object1" });
        skipList.Add(new MyObject { Id = 2, Name = "Object2" });

        // Act
        var count = skipList.Count;

        // Assert
        Assert.Equal(2, count);
    }

    [Fact]
    public void Add_ValuesOutOfOrder_ShouldMaintainSortedOrder()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        var obj1 = new MyObject { Id = 3, Name = "Object3" };
        var obj2 = new MyObject { Id = 1, Name = "Object1" };
        var obj3 = new MyObject { Id = 2, Name = "Object2" };
    
        // Act
        skipList.Add(obj1);
        skipList.Add(obj2);
        skipList.Add(obj3);
        var allValues = skipList.ToList();
        
        // Assert
        Assert.Collection(allValues,
            item => Assert.Equal(obj2, item),
            item => Assert.Equal(obj3, item),
            item => Assert.Equal(obj1, item)
        );
    }

    [Fact]
    public void AddAndRemove_MultipleOperations_ShouldWorkAsExpected()
    {
        // Arrange
        var skipList = new ComparableSkipList<MyObject>();
        var obj1 = new MyObject { Id = 1, Name = "Object1" };
        var obj2 = new MyObject { Id = 2, Name = "Object2" };

        // Act
        skipList.Add(obj1);
        skipList.Add(obj2);
        skipList.Remove(obj1);

        // Assert
        Assert.False(skipList.Contains(obj1), "Removed object was still found in the skip list.");
        Assert.True(skipList.Contains(obj2), "Valid object was not found in the skip list.");
    }
}
