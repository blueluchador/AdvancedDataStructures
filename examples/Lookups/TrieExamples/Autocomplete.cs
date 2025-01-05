using AdvancedDataStructures.Lookups;

namespace Lookups.TrieExamples;

public static class Autocomplete
{
    public static List<string> Result(string[] words, string prefix)
    {
        var trie = new Trie(words);
        return trie.QueryWords(prefix);
        // // Insert words into the Trie
        // trie.Insert("apple");
        // trie.Insert("app");
        // trie.Insert("apricot");
        // trie.Insert("banana");
        // trie.Insert("bat");
        // trie.Insert("batch");

        // Find all words with the prefix "ap"
        
        //var autocompleteResults = trie.QueryWords(prefix);

        // // Print the results
        // Console.WriteLine($"Autocomplete suggestions for '{prefix}':");
        // foreach (var word in autocompleteResults)
        // {
        //     Console.WriteLine(word);
        // }
    }
}