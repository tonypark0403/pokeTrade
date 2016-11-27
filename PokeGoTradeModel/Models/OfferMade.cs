using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeGoTradeModel.Models
{
    public class OfferMade
    {
        public int OfferMadeId { get; set; }
      
        public int PostingId { get; set; }
        public string CurrentUser { get; set; }
        public string SelectedUser { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PokemonOwned { get; set; }
        public string PokemonWanted { get; set; }        
        public string Status { get; set; }
        public DateTime DateTimeValue { get; set; }

    }
}
