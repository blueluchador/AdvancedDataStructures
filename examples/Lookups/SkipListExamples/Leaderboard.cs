using AdvancedDataStructures.Lookups.SkipLists;

namespace Lookups.SkipListExamples;

public class Leaderboard(IEnumerable<int>? initialScores = null)
{
    private readonly SkipList<int> _leaderboard = new(initialScores);
    
    public void AddScore(int score)
    {
        _leaderboard.Add(score);
    }
    
    public void DisplayTopScores(int topN)
    {
        Console.WriteLine($"Top {topN} Scores:");

        var topScores = _leaderboard.Skip(_leaderboard.Count - topN);

        int lastScore = 0;
        int rank = 1;
        int runningRank = 1;
        foreach (int score in topScores)
        {
            if (score != lastScore) rank = runningRank;
            lastScore = score;
            Console.WriteLine($"{rank}. {score}");
            runningRank++;
        }
    }
}