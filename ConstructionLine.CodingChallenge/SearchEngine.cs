using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {
            Validate(options);

            var predicate = CreatePredicate(options);
            var shirts = _shirts.Where(shirt => predicate(shirt)).ToList();

            var colorCounts = Color.All.Select(color => new ColorCount 
            {
                Color = color, Count = shirts.Count(shirt => shirt.Color == color) 
            });

            var sizeCounts = Size.All.Select(size => new SizeCount
            {
                Size = size, Count = shirts.Count(shirt => shirt.Size == size)
            });

            return new SearchResults
            {
                ColorCounts = colorCounts.ToList(),
                Shirts = shirts,
                SizeCounts = sizeCounts.ToList()
            };
        }

        private void Validate(SearchOptions options)
        {
            if (options == null) throw new ArgumentException("Please provide SearchOptions.", nameof(options));
            if (options.Colors == null) throw new ArgumentException("Please provide SearchOptions Colors.");
            if (options.Sizes == null) throw new ArgumentException("Please provide SearchOptions Sizes.");
        }

        private Func<Shirt, bool> CreatePredicate(SearchOptions options)
        {
            Func<Shirt, bool> emptyStrategy = shirt => false;
            Func<Shirt, bool> sizesStrategy=shirt => options.Sizes.Contains(shirt.Size);
            Func<Shirt, bool> colorsStrategy =  shirt => options.Colors.Contains(shirt.Color);
            Func<Shirt, bool> combinedStrategy =  shirt => options.Colors.Contains(shirt.Color) && options.Sizes.Contains(shirt.Size);

            if (!options.Colors.Any() && !options.Sizes.Any())
            {
                return emptyStrategy;
            }

            if (options.Colors.Any() && options.Sizes.Any())
            {
                return combinedStrategy;
            }

            if (options.Sizes.Any() && !options.Colors.Any())
            {
                return sizesStrategy; 
            }

            if (options.Colors.Any() && !options.Sizes.Any())
            {
                return colorsStrategy;
            }

            throw new InvalidOperationException();
        }
    }
}