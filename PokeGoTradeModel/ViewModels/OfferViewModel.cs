
using System;

namespace PokeGoTradeModel.ViewModels
{
    public class OfferViewModel
    {
        public int PostingId { get; set; }
        public string CurrentUser { get; set; }
        public string SelectedUser { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PokemonOwned { get; set; }
        public string PokemonWanted { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Choice { get; set; }
        public DateTime DateTimeValue { get; set; }
    }
}
