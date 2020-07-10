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
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var redShirts = new List<Shirt> {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
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

        [Test]
        public void Search_HasCorrect_SizeCount()
        {
            var repoShirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
            };

            var smallShirts = new List<Shirt> {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
            };

            repoShirts.AddRange(smallShirts);

            var searchEngine = new SearchEngine(repoShirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts, Is.Not.Null);

            var shirtCount = results.Shirts.Where(resultShirt => repoShirts.Contains(resultShirt)).Count();
            Assert.AreEqual(smallShirts.Count, shirtCount);

            var smallSizeCount = results.SizeCounts.Where(sc => sc.Size.Name == Size.Small.Name.ToString()).FirstOrDefault().Count;
            Assert.That(smallSizeCount, Is.Not.Null);
            Assert.AreEqual(smallShirts.Count, smallSizeCount);
        }

        [Test]
        public void Search_HasCorrectShirt_OnlyColorSearchOption()
        {
            var redShirt = new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red);
            var blackShirt = new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black);
            var shirts = new List<Shirt> { redShirt, blackShirt };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts, Is.Not.Null);
            Assert.That(results.Shirts.Contains(redShirt));
            Assert.False(results.Shirts.Contains(blackShirt));
        }

        [Test]
        public void Search_HasCorrectShirt_OnlySizeSearchOption()
        {
            var smallShirt = new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red);
            var mediumShirt = new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Black);
            var shirts = new List<Shirt> { smallShirt, mediumShirt };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Small },
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts, Is.Not.Null);
            Assert.That(results.Shirts.Contains(smallShirt));
            Assert.False(results.Shirts.Contains(mediumShirt));
        }

        [Test]
        public void Search_EmptyResult_WhenNoData()
        {
            var shirts = new List<Shirt> { };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Sizes = Size.All,
                Colors = Color.All
            };

            var results = searchEngine.Search(searchOptions);
            Assert.That(results.Shirts, Is.Not.Null);
            Assert.That(results.Shirts.Count, Is.Zero);
        }

        [Test]
        public void Search_Throws_ForNullSearchOptions()
        {
            var shirts = new List<Shirt> { new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red) };

            var searchEngine = new SearchEngine(shirts);

            Assert.Throws(typeof(ArgumentException), () => searchEngine.Search(null));
        }

        [Test]
        public void Search_Throws_ForNullColors()
        {
            var shirts = new List<Shirt> { };

            var searchEngine = new SearchEngine(shirts);

            Assert.Throws(typeof(ArgumentException), () => searchEngine.Search(new SearchOptions { Colors = null }));
        }

        [Test]
        public void Search_Throws_ForNullSizes()
        {
            var shirts = new List<Shirt> { };

            var searchEngine = new SearchEngine(shirts);

            Assert.Throws(typeof(ArgumentException), () => searchEngine.Search(new SearchOptions { Sizes = null }));
        }

        [Test]
        public void Search_HasCorrectShirts_EmptySearchOptions()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
            };

            var searchEngine = new SearchEngine(shirts);
            var results = searchEngine.Search(new SearchOptions());

            Assert.That(results.Shirts, Is.Not.Null);
            Assert.That(results.Shirts.Count, Is.Zero);
        }
    }
}
