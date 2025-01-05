using Lookups.TrieExamples;

// *** Trie Examples ***
Console.WriteLine("*** Trie examples ***");

// Autocomplete
Console.WriteLine("Running autocomplete example...");
var result = Autocomplete.Result(["apple", "app", "apricot", "banana", "bat", "batch"], "ap");

// Print the results
Console.WriteLine("Autocomplete suggestions for 'ap':");
foreach (string word in result)
{
    Console.WriteLine(word);
}
