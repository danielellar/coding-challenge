using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.

        }


        public SearchResults Search(SearchOptions options)
        {
            var shirts = _shirts.Where(shirt =>
                options.Colors.Contains(shirt.Color) &&
                options.Sizes.Contains(shirt.Size));

            var colorCounts = Color.All.Select(color => new ColorCount { Color = color, Count = shirts.Count(shirt => shirt.Color == color) });
            var sizeCounts = Size.All.Select(size => new SizeCount { Size = size, Count = shirts.Count(shirt => shirt.Size == size) });

            return new SearchResults
            {
                ColorCounts = colorCounts.ToList(),
                Shirts = shirts.ToList(),
                SizeCounts = sizeCounts.ToList()
            };

            //TODO: ensure counts calculate the latest, up to date data
        }
    }
}