using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void Test()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_HasCorrectShirt_Color()
        {
            var redShirt = new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red);
            var blackShirt = new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Black);
            var shirts = new List<Shirt> { redShirt, blackShirt };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts, Is.Not.Null);
            Assert.That(results.Shirts.Contains(redShirt));
            Assert.False(results.Shirts.Contains(blackShirt));
        }

        [Test]
        public void Search_HasCorrectShirt_Size()
        {
            var smallShirt = new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red);
            var largeShirt = new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red);
            var shirts = new List<Shirt> { smallShirt, largeShirt };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts, Is.Not.Null);
            Assert.That(results.Shirts.Contains(smallShirt));
            Assert.False(results.Shirts.Contains(largeShirt));
        }

        [Test]
        public void Search_HasCorrect_ColorCount()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var redShirts = new List<Shirt> {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red)
            };

            shirts.AddRange(redShirts);

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts, Is.Not.Null);

            Assert.AreEqual(
                redShirts.Count,
                results.Shirts.Where(shirt => shirts.Contains(shirt)).Count());
        }
    }
}
