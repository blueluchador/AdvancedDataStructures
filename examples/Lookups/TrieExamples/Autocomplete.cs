using AdvancedDataStructures.Lookups;

namespace Lookups.TrieExamples;

public static class Autocomplete
{
    public static List<string> QueryWords(this string[] words, string prefix)
    {
        var trie = new Trie(words);
        return trie.QueryWords(prefix);
    }
}