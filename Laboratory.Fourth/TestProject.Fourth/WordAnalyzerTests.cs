using ClassLib.Fourth;

namespace TestProject.Fourth;

[TestFixture]
public class WordAnalyzerTests
{
    [Test]
    public void EmptyStringReturnsEmptyList()
    {
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd("");
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void NullStringReturnsEmptyList()
    {
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void SingleMatchingWordIsReturned()
    {
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd("Anna");
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0], Is.EqualTo("Anna"));
    }

    [Test]
    public void SingleNonMatchingWordReturnsEmpty()
    {
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd("Tests");
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void MultipleWordsReturnOnlyMatchingOnes()
    {
        var input = "Anna test pop house dad";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);

        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result, Does.Contain("Anna"));
        Assert.That(result, Does.Contain("test"));
        Assert.That(result, Does.Contain("pop"));
        Assert.That(result, Does.Contain("dad"));
    }

    [Test]
    public void WordsWithPunctuationAreHandledCorrectly()
    {
        var input = "Anna, pop! dad?";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);
        Assert.That(result.Count, Is.EqualTo(3));
    }

    [Test]
    public void MixedCaseLettersAreIgnored()
    {
        var input = "aAa Pop pOp";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);
        Assert.That(result.Count, Is.EqualTo(3));
    }

    [Test]
    public void WordWithOneLetterIsIncluded()
    {
        var input = "a";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0], Is.EqualTo("a"));
    }

    [Test]
    public void MultipleSingleLetterWordsAreAllIncluded()
    {
        var input = "a b c d";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);
        Assert.That(result.Count, Is.EqualTo(4));
    }

    [Test]
    public void WordsWithNumbersAreHandled()
    {
        var input = "1a1 22b2b 3c3";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);

        Assert.That(result, Does.Contain("1a1"));
        Assert.That(result, Does.Contain("3c3"));
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public void LeadingAndTrailingSpacesAreIgnored()
    {
        var input = "   Anna pop dad   ";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);
        Assert.That(result.Count, Is.EqualTo(3));
    }

    [Test]
    public void MultipleSpacesBetweenWordsAreHandled()
    {
        var input = "Anna   pop   dad";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);
        Assert.That(result.Count, Is.EqualTo(3));
    }

    [Test]
    public void NoMatchingWordsReturnsEmptyList()
    {
        var input = "one two three four";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void WordsWithNonLetterCharactersAtEdgesAreParsedCorrectly()
    {
        var input = "(Anna) [pop] {dad}";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);
        Assert.That(result.Count, Is.EqualTo(3));
    }

    [Test]
    public void LongWordMatchingIsFound()
    {
        var input = "abca test dad";
        var result = WordAnalyzer.FindWordsWithSameStartAndEnd(input);

        Assert.That(result, Does.Contain("abca"));
        Assert.That(result, Does.Contain("dad"));
        Assert.That(result.Count, Is.EqualTo(3));
    }
}