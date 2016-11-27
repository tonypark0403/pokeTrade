using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeGoTradeModel.Models
{
    public class Posting
    {
        public int PostingId { get; set; }
      
        public string Username { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }        
        public string Comment { get; set; }
        public DateTime DateTimeValue { get; set; }

        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int PokemonId { get; set; }
        
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }

        //겁나 중요. 한 클래스에 같은 모델이 한 개 이상 있을 시 (Ex: Pokemon) 
        //ForeignKey annotation을 써줘서 EF가 헤깔려하지 않게 지정해줘야 한다.        
        [ForeignKey("PokemonId")]
        public virtual Pokemon Pokemon { get; set; }
        public virtual ICollection<Pokemon> Pokemons { get; set; }
    }
}
