using System.Collections;

namespace AdvancedDataStructures.Trie;

public class Trie : IEnumerable<string>
{
    private class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new();
        public bool IsEndOfWord { get; set; }
    }

    private readonly TrieNode _root = new();

    public void Insert(string word)
    {
        ArgumentNullException.ThrowIfNull(word);
        
        word = word.Trim(); // Trim leading and trailing spaces
        if (word.Length == 0) return;

        var current = _root;
        foreach (char c in word)
        {
            if (!current.Children.ContainsKey(c))
                current.Children[c] = new TrieNode();
            current = current.Children[c];
        }
        current.IsEndOfWord = true;
    }

    public bool Search(string word)
    {
        var current = _root;
        return word.All(c => current.Children.TryGetValue(c, out current)) && current.IsEndOfWord;
    }
    
    public List<string> QueryWords(string prefix)
    {
        var current = _root;

        // Traverse to the end of the prefix
        if (prefix.Any(c => !current.Children.TryGetValue(c, out current)))
        {
            return []; // Prefix not found
        }

        // Collect all words from the prefix node
        var results = new List<string>();
        CollectWords(current, prefix, results);
        return results;
    }

    public IEnumerator<string> GetEnumerator()
    {
        return Traverse(_root, "").GetEnumerator();
    }
    
    private static void CollectWords(TrieNode node, string prefix, List<string> results)
    {
        if (node.IsEndOfWord)
            results.Add(prefix); // Add word to the result list

        foreach ((char key, var child) in node.Children)
        {
            CollectWords(child, prefix + key, results);
        }
    }

    private static IEnumerable<string> Traverse(TrieNode node, string prefix)
    {
        if (node.IsEndOfWord)
            yield return prefix;

        foreach ((char key, var child) in node.Children)
        {
            foreach (string word in Traverse(child, prefix + key))
                yield return word;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}