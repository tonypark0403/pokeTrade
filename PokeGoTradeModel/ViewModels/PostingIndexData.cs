
using PokeGoTradeModel.Models;
using System.Collections.Generic;

namespace PokeGoTradeModel.ViewModels
{
    public class PostingIndexData
    {
        public IEnumerable<Posting> Postings { get; set; }
        public IEnumerable<Pokemon> Pokemons { get; set; }
    }
}
