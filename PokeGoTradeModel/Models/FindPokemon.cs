using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeGoTradeModel.Models
{
    public class FindPokemon
    {
        public int FindPokemonId { get; set; }

        public int Country { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public int IWant { get; set; }
    }
}
