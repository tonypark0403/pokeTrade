using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeGoTradeModel.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }       
    }
}
