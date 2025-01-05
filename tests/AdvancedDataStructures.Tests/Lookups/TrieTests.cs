using AdvancedDataStructures.Lookups;

namespace AdvancedDataStructures.Tests.Lookups;

public class TrieTests
{
    [Fact]
    public void Insert_SingleWord_ShouldStoreAndRetrieveSuccessfully()
    {
        var trie = new Trie();
        trie.Insert("apple");
        Assert.True(trie.Search("apple"), "The word 'apple' was not found.");
    }

    [Fact]
    public void Insert_DuplicateWords_ShouldStoreOnceAndRetrieveSuccessfully()
    {
        var trie = new Trie();
        trie.Insert("apple");
        trie.Insert("apple");
        Assert.True(trie.Search("apple"), "The word 'apple' was found.");
    }
    
    [Fact]
    public void ParameterizedConstructor_WordsInserted_ShouldStoreAndRetrieveThemSuccessfully()
    {
        var trie = new Trie(["apricot", "apple", "app"]);
        Assert.True(trie.Search("apricot"), "The word 'app' was not found.");
        Assert.True(trie.Search("apple"), "The word 'apple' was found.");
        Assert.True(trie.Search("app"), "The word 'app' was not found.");
    }

    [Fact]
    public void Insert_WordsWithSharedPrefix_ShouldStoreAndRetrieveThemSuccessfully()
    {
        var trie = new Trie();
        trie.Insert("apple");
        trie.Insert("app");
        Assert.True(trie.Search("app"), "The word 'app' was not found.");
        Assert.True(trie.Search("apple"), "The word 'apple' was found.");
    }

    [Fact]
    public void Search_ExistingWord_ShouldReturnTrue()
    {
        var trie = new Trie();
        trie.Insert("test");
        Assert.True(trie.Search("test"), "The word 'test' was not found.");
    }

    [Fact]
    public void Search_NonExistingWord_ShouldReturnFalse()
    {
        var trie = new Trie();
        trie.Insert("test");
        Assert.False(trie.Search("toast"), "The word 'toast' was not found.");
    }

    [Fact]
    public void Search_WordAsPrefixOnly_ShouldReturnFalse()
    {
        var trie = new Trie();
        trie.Insert("testing");
        Assert.False(trie.Search("test"), "The word 'testing' was not found.");
    }

    [Fact]
    public void Search_EmptyString_ShouldReturnFalse()
    {
        var trie = new Trie();
        Assert.False(trie.Search(""), "Empty string is not a valid word in the Trie.");
    }

    [Fact]
    public void QueryWords_ValidPrefix_ShouldReturnMatchingWords()
    {
        var trie = new Trie();
        trie.Insert("apple");
        trie.Insert("app");
        trie.Insert("application");
        var result = trie.QueryWords("app");
        Assert.Contains("app", result);
        Assert.Contains("apple", result);
        Assert.Contains("application", result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void QueryWords_InvalidPrefix_ShouldReturnEmptyList()
    {
        var trie = new Trie();
        trie.Insert("test");
        var result = trie.QueryWords("xyz");
        Assert.Empty(result);
    }

    [Fact]
    public void QueryWords_EmptyPrefix_ShouldReturnAllWords()
    {
        var trie = new Trie();
        trie.Insert("apple");
        trie.Insert("banana");
        trie.Insert("cherry");
        var result = trie.QueryWords("");
        Assert.Contains("apple", result);
        Assert.Contains("banana", result);
        Assert.Contains("cherry", result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void GetEnumerator_AllInsertedWords_ShouldReturnAllWords()
    {
        var trie = new Trie();
        trie.Insert("dog");
        trie.Insert("cat");
        trie.Insert("bat");
        var result = trie.ToList();
        Assert.Contains("dog", result);
        Assert.Contains("cat", result);
        Assert.Contains("bat", result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void GetEnumerator_NoWordsInserted_ShouldReturnEmptyList()
    {
        var trie = new Trie();
        var result = trie.ToList();
        Assert.Empty(result);
    }

    [Fact]
    public void Insert_NullWord_ShouldThrowArgumentNullException()
    {
        var trie = new Trie();
        Assert.Throws<ArgumentNullException>(() => trie.Insert(null!));
    }

    [Fact]
    public void Search_NullWord_ShouldThrowArgumentNullException()
    {
        var trie = new Trie();
        Assert.Throws<ArgumentNullException>(() => trie.Search(null!));
    }

    [Fact]
    public void QueryWords_NullWord_ShouldThrowArgumentNullException()
    {
        var trie = new Trie();
        Assert.Throws<ArgumentNullException>(() => trie.QueryWords(null!));
    }

    [Fact]
    public void Insert_And_Search_ShouldTrimLeadingAndTrailingSpaces()
    {
        var trie = new Trie();
        trie.Insert("  apple  ");
        Assert.True(trie.Search("apple"), "The word 'apple' was not found.");
        Assert.False(trie.Search("apple pie"), "Search should fail for a different word.");
    }

    [Fact]
    public void Insert_EmptyString_ShouldBeIgnored()
    {
        var trie = new Trie();
        trie.Insert("");
        Assert.False(trie.Search(""), "Search should return false for an empty string.");
    }

    [Fact]
    public void Insert_WhitespaceString_ShouldBeIgnored()
    {
        var trie = new Trie();
        trie.Insert("   ");
        Assert.False(trie.Search("   "), "Search should return false for a whitespace.");
    }

    [Fact]
    public void Insert_And_Search_ValidWord_ShouldSucceed()
    {
        var trie = new Trie();
        trie.Insert("apple");
        Assert.True(trie.Search("apple"), "The word 'apple' was not found.");
        Assert.False(trie.Search("ap"), "Search should return false for a different word.");
    }

    [Fact]
    public void CaseInsensitive_SearchWord_ShouldReturnTrueForMatchingCases()
    {
        var trie = new Trie();
        trie.Insert("apple");
        Assert.True(trie.Search("Apple"), "The word 'Apple' was not found.");
    }
    
    [Fact]
    public void CaseInsensitive_QueryWords_Prefix_ShouldReturnAllMatchingWords()
    {
        var trie = new Trie();
        trie.Insert("App");
        trie.Insert("Apple");
        trie.Insert("Application");
        var result = trie.QueryWords("APP");
        Assert.Contains("app", result);
        Assert.Contains("apple", result);
        Assert.Contains("application", result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void CaseSensitive_SearchWord_ShouldDifferentiateCases()
    {
        var trie = new Trie(isCaseSensitive: true);
        trie.Insert("Apple");
        Assert.True(trie.Search("Apple"), "The word 'Apple' was not found.");
        Assert.False(trie.Search("apple"), "Search should return false for the lowercase word 'apple'.");
    }
    
    [Fact]
    public void CaseSensitive_QueryWords_Prefix_ShouldReturnMatchingWords()
    {
        var trie = new Trie(isCaseSensitive: true);
        trie.Insert("App");
        trie.Insert("Apple");
        trie.Insert("Application");
        var result = trie.QueryWords("App");
        Assert.Contains("App", result);
        Assert.Contains("Apple", result);
        Assert.Contains("Application", result);
        Assert.Equal(3, result.Count);
    }
    
    [Fact]
    public void CaseSensitive_QueryWords_InvalidPrefix_ShouldReturnEmpty()
    {
        var trie = new Trie(isCaseSensitive: true);
        trie.Insert("App");
        trie.Insert("Apple");
        trie.Insert("Application");
        Assert.Empty(trie.QueryWords("app"));
    }
}
