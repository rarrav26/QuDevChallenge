using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuWordFinder;

namespace QuWordFinderTests
{
    [TestClass]
    public class WordFinderTests
    {
        [TestMethod]
        public void Find_WordsFound()
        {
            // Arrange
            var matrix = new List<string> { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            var wordFinder = new WordFinder(matrix);
            var wordstream = new List<string> { "chill", "cold", "wind" };

            // Act
            var result = wordFinder.Find(wordstream);

            // Assert
            CollectionAssert.AreEqual(wordstream, result.ToList());
        }

        [TestMethod]
        public void Find_NoWordsFound()
        {
            // Arrange
            var matrix = new List<string> { "abcde", "fghij", "klmno", "pqrst", "uvwxy" };
            var wordFinder = new WordFinder(matrix);
            var wordstream = new List<string> { "chill", "cold", "wind" };

            // Act
            var result = wordFinder.Find(wordstream);

            // Assert
            CollectionAssert.AreEqual(new List<string>(), result.ToList());
        }

        [TestMethod]
        public void Find_WordsFound_RepeatedWordsInWordstream()
        {
            // Arrange
            var matrix = new List<string> { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            var wordFinder = new WordFinder(matrix);
            var wordstream = new List<string> { "chill", "cold", "wind", "chill", "cold" };

            // Act
            var result = wordFinder.Find(wordstream);

            // Assert
            CollectionAssert.AreEqual(wordstream.Distinct().ToList(), result.ToList());
        }
    }
}