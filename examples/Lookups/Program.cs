using AdvancedDataStructures.Lookups;
using Lookups.TrieExamples;

// *** Trie Examples ***
Console.WriteLine("*** Trie examples ***");

// Autocomplete
Console.WriteLine("Running autocomplete example...");
var result = Autocomplete.Result(["apple", "app", "apricot", "banana", "bat", "batch"], "ap");
Console.WriteLine($"Autocomplete suggestions for 'ap': {String.Join(", ", result)}");