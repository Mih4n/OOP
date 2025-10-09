using System.Text.RegularExpressions;

namespace ClassLib.Fourth;

public static class WordAnalyzer
{
    /// <summary>
    /// Finds all the words that start and end with the same character (case-insensitive).
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>A list of words that start and end with the same character.</returns>
    public static List<string> FindWordsWithSameStartAndEnd(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return [];

        var words = Regex.Matches(input, @"\b\w+\b")
            .Cast<Match>()
            .Select(m => m.Value)
            .ToList();

        var result = new List<string>();

        foreach (var word in words)
        {
            if (word.Length == 0)
                continue;

            char first = char.ToLower(word[0]);
            char last = char.ToLower(word[^1]);

            if (first == last)
            {
                result.Add(word);
            }
        }

        return result;
    }
}

