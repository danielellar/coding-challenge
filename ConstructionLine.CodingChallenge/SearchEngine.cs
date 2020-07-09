using System.Collections.Generic;

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
            // TODO: search logic goes here.

            return new SearchResults
            {
                ColorCounts = new List<ColorCount>(),
                Shirts = new List<Shirt>(),
                SizeCounts = new List<SizeCount>()
            };
        }
    }
}