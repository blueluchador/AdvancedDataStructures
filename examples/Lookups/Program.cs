using AdvancedDataStructures.Lookups;
using Lookups.SkipListExamples;
using Lookups.TrieExamples;

// *** Trie Examples ***
Console.WriteLine("*** Trie examples ***");

// Autocomplete
Console.WriteLine("Running autocomplete...");
string[] words = ["apple", "app", "apricot", "banana", "bat", "batch"];
var result = words.QueryWords("ap");
Console.WriteLine($"Autocomplete suggestions for 'ap': {String.Join(", ", result)}");

// *** Skip List Examples ***
Console.WriteLine("*** Skip List examples ***");

// Leaderboard
Console.WriteLine("Running leaderboard...");
var leaderboard = new Leaderboard(
    [5000, 100, 7000, 200, 400, 3000, 8900, 8000, 9000, 10000, 1000, 1000, 1000, 500, 5500, 6900, 700, 900, 3100, 900]);
leaderboard.DisplayTopScores(5);

Console.WriteLine("Add new score of 9500");
leaderboard.AddScore(9500);
leaderboard.DisplayTopScores(5);