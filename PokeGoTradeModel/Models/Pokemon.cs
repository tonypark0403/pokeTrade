using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeGoTradeModel.Models
{
    public class Pokemon
    {
        public int PokemonId { get; set; }
        public string Name { get; set; }
      
        public virtual ICollection<Posting> Postings { get; set; }   
             
    }
}
