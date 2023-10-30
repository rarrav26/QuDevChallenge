using System;
using System.Collections.Generic;
using System.Linq;

namespace QuWordFinder
{
    public class WordFinder
    {
        private readonly char[][] matrix;

        public WordFinder(IEnumerable<string> matrix)
        {
            // Validate matrix
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            if (matrix.All(row => row.Length == 0))
                throw new ArgumentException("Matrix is empty.");

            // Initialize the character matrix from the input strings
            this.matrix = matrix.Select(row => row.ToCharArray()).ToArray();
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            // Validate wordstream
            if (wordstream == null || wordstream.Count() == 0)
                return Enumerable.Empty<string>();

            // Flatten the matrix into a single string for easier searching
            var matrixString = new string(matrix.SelectMany(row => row).ToArray());

            // Create a dictionary to store the word frequencies
            var wordFrequencies = new Dictionary<string, int>();

            foreach (var word in wordstream)
            {
                // Check for the word horizontally (left to right)
                var count = CountWordOccurrences(matrixString, word);

                // Check for the word vertically (top to bottom)
                for (int col = 0; col < matrix[0].Length; col++)
                {
                    var column = new string(matrix.Select(row => row[col]).ToArray());
                    count += CountWordOccurrences(column, word);
                }

                // Update the word frequency in the dictionary
                wordFrequencies[word] = count;
            }

            // Get the top 10 most repeated words
            var topWords = wordFrequencies.Where(pair => pair.Value > 0)
                .OrderByDescending(pair => pair.Value)
                .ThenBy(pair => pair.Key)
                .Take(10)
                .Select(pair => pair.Key);

            return topWords;
        }

        private int CountWordOccurrences(string text, string word)
        {
            int count = 0;
            int index = text.IndexOf(word);
            while (index != -1)
            {
                count++;
                index = text.IndexOf(word, index + word.Length);
            }
            return count;
        }
    }
}