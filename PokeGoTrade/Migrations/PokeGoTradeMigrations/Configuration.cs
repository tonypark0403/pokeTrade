namespace PokeGoTrade.Migrations.PokeGoTradeMigrations
{
    using PokeGoTradeModel.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PokeGoTradeModel.Models.PGTContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\PokeGoTradeMigrations";
        }

        protected override void Seed(PokeGoTradeModel.Models.PGTContext context)
        {
            var pokemons = new List<Pokemon>
            {
                new Pokemon { PokemonId = 1, Name = "Bulbasaur" }, new Pokemon { PokemonId = 2, Name = "Ivysaur" }, new Pokemon { PokemonId = 3, Name = "Venusaur" },
                new Pokemon { PokemonId = 4, Name = "Charmander" }, new Pokemon { PokemonId = 5, Name = "Charmeleon" }, new Pokemon { PokemonId = 6, Name = "Charizard" },
                new Pokemon { PokemonId = 7, Name = "Squirtle" }, new Pokemon { PokemonId = 8, Name = "Wartortle" }, new Pokemon { PokemonId = 9, Name = "Blastoise" },
                new Pokemon { PokemonId = 10, Name = "Caterpie" }, new Pokemon { PokemonId = 11, Name = "Metapod" }, new Pokemon { PokemonId = 12, Name = "Butterfree" },
                new Pokemon { PokemonId = 13, Name = "Weedle" }, new Pokemon { PokemonId = 14, Name = "Kakuna" }, new Pokemon { PokemonId = 15, Name = "Beedrill" },
                new Pokemon { PokemonId = 16, Name = "Pidgey" }, new Pokemon { PokemonId = 17, Name = "Pidgeotto" }, new Pokemon { PokemonId = 18, Name = "Pidgeot" },
                new Pokemon { PokemonId = 19, Name = "Rattata" }, new Pokemon { PokemonId = 20, Name = "Raticate" }, new Pokemon { PokemonId = 21, Name = "Spearow" },
                new Pokemon { PokemonId = 22, Name = "Fearow" }, new Pokemon { PokemonId = 23, Name = "Ekans" }, new Pokemon { PokemonId = 24, Name = "Arbok" },
                new Pokemon { PokemonId = 25, Name = "Pikachu" }, new Pokemon { PokemonId = 26, Name = "Raichu" }, new Pokemon { PokemonId = 27, Name = "Sandshrew" },
                new Pokemon { PokemonId = 28, Name = "Sandslash" }, new Pokemon { PokemonId = 29, Name = "Nidoran F" }, new Pokemon { PokemonId = 30, Name = "Nidorina" },
                new Pokemon { PokemonId = 31, Name = "Nidoqueen" }, new Pokemon { PokemonId = 32, Name = "Nidoran M" }, new Pokemon { PokemonId = 33, Name = "Nidorino" },
                new Pokemon { PokemonId = 34, Name = "Nidoking" }, new Pokemon { PokemonId = 35, Name = "Clefairy" }, new Pokemon { PokemonId = 36, Name = "Clefable" },
                new Pokemon { PokemonId = 37, Name = "Vulpix" }, new Pokemon { PokemonId = 38, Name = "Ninetails" }, new Pokemon { PokemonId = 39, Name = "Jigglypuff" },
                new Pokemon { PokemonId = 40, Name = "Wigglytuff" }, new Pokemon { PokemonId = 41, Name = "Zubat" }, new Pokemon { PokemonId = 42, Name = "Golbat" },
                new Pokemon { PokemonId = 43, Name = "Oddish" }, new Pokemon { PokemonId = 44, Name = "Gloom" }, new Pokemon { PokemonId = 45, Name = "Vileplume" },
                new Pokemon { PokemonId = 46, Name = "Paras" }, new Pokemon { PokemonId = 47, Name = "Parasect" }, new Pokemon { PokemonId = 48, Name = "Venonat" },
                new Pokemon { PokemonId = 49, Name = "Venomoth" }, new Pokemon { PokemonId = 50, Name = "Diglett" }, new Pokemon { PokemonId = 51, Name = "Dugtrio" },
                new Pokemon { PokemonId = 52, Name = "Meowth" }, new Pokemon { PokemonId = 53, Name = "Persian" }, new Pokemon { PokemonId = 54, Name = "Psyduck" },
                new Pokemon { PokemonId = 55, Name = "Golduck" }, new Pokemon { PokemonId = 56, Name = "Mankey" }, new Pokemon { PokemonId = 57, Name = "Primeape" },
                new Pokemon { PokemonId = 58, Name = "Growlithe" }, new Pokemon { PokemonId = 59, Name = "Arcanine" }, new Pokemon { PokemonId = 60, Name = "Poliwag" },
                new Pokemon { PokemonId = 61, Name = "Poliwhirl" }, new Pokemon { PokemonId = 62, Name = "Poliwrath" }, new Pokemon { PokemonId = 63, Name = "Abra" },
                new Pokemon { PokemonId = 64, Name = "Kadabra" }, new Pokemon { PokemonId = 65, Name = "Alakazam" }, new Pokemon { PokemonId = 66, Name = "Machop" },
                new Pokemon { PokemonId = 67, Name = "Machoke" }, new Pokemon { PokemonId = 68, Name = "Machamp" }, new Pokemon { PokemonId = 69, Name = "Bellsprout" },
                new Pokemon { PokemonId = 70, Name = "Weepinbell" }, new Pokemon { PokemonId = 71, Name = "Victreebel" }, new Pokemon { PokemonId = 72, Name = "Tentacool" },
                new Pokemon { PokemonId = 73, Name = "Tentacruel" }, new Pokemon { PokemonId = 74, Name = "Geodude" }, new Pokemon { PokemonId = 75, Name = "Graveler" },
                new Pokemon { PokemonId = 76, Name = "Golem" }, new Pokemon { PokemonId = 77, Name = "Ponyta" }, new Pokemon { PokemonId = 78, Name = "Rapidash" },
                new Pokemon { PokemonId = 79, Name = "Slowpoke" }, new Pokemon { PokemonId = 80, Name = "Slowbro" }, new Pokemon { PokemonId = 81, Name = "Magnemite" },
                new Pokemon { PokemonId = 82, Name = "Magneton" }, new Pokemon { PokemonId = 83, Name = "Farfetch'd" }, new Pokemon { PokemonId = 84, Name = "Doduo" },
                new Pokemon { PokemonId = 85, Name = "Dodrio" }, new Pokemon { PokemonId = 86, Name = "Seel" }, new Pokemon { PokemonId = 87, Name = "Dewgong" },
                new Pokemon { PokemonId = 88, Name = "Grimer" }, new Pokemon { PokemonId = 89, Name = "Muk" }, new Pokemon { PokemonId = 90, Name = "Shellder" },
                new Pokemon { PokemonId = 91, Name = "Cloyster" }, new Pokemon { PokemonId = 92, Name = "Gastly" }, new Pokemon { PokemonId = 93, Name = "Haunter" },
                new Pokemon { PokemonId = 94, Name = "Gengar" }, new Pokemon { PokemonId = 95, Name = "Onix" }, new Pokemon { PokemonId = 96, Name = "Drowzee" },
                new Pokemon { PokemonId = 97, Name = "Hypno" }, new Pokemon { PokemonId = 98, Name = "Krabby" }, new Pokemon { PokemonId = 99, Name = "Kingler" },
                new Pokemon { PokemonId = 100, Name = "Voltorb" }, new Pokemon { PokemonId = 101, Name = "Electrode" }, new Pokemon { PokemonId = 102, Name = "Exeggcute" },
                new Pokemon { PokemonId = 103, Name = "Exeggutor" }, new Pokemon { PokemonId = 104, Name = "Cubone" }, new Pokemon { PokemonId = 105, Name = "Marowak" },
                new Pokemon { PokemonId = 106, Name = "Hitmonlee" }, new Pokemon { PokemonId = 107, Name = "Hitmonchan" }, new Pokemon { PokemonId = 108, Name = "Lickitung" },
                new Pokemon { PokemonId = 109, Name = "Koffing" }, new Pokemon { PokemonId = 110, Name = "Weezing" }, new Pokemon { PokemonId = 111, Name = "Rhyhorn" },
                new Pokemon { PokemonId = 112, Name = "Rhydon" }, new Pokemon { PokemonId = 113, Name = "Chansey" }, new Pokemon { PokemonId = 114, Name = "Tangela" },
                new Pokemon { PokemonId = 115, Name = "Kangaskhan" }, new Pokemon { PokemonId = 116, Name = "Horsea" }, new Pokemon { PokemonId = 117, Name = "Seadra" },
                new Pokemon { PokemonId = 118, Name = "Goldeen" }, new Pokemon { PokemonId = 119, Name = "Seaking" }, new Pokemon { PokemonId = 120, Name = "Staryu" },
                new Pokemon { PokemonId = 121, Name = "Starmie" }, new Pokemon { PokemonId = 122, Name = "Mr. Mime" }, new Pokemon { PokemonId = 123, Name = "Scyther" },
                new Pokemon { PokemonId = 124, Name = "Jynx" }, new Pokemon { PokemonId = 125, Name = "Electabuzz" }, new Pokemon { PokemonId = 126, Name = "Magmar" },
                new Pokemon { PokemonId = 127, Name = "Pinsir" }, new Pokemon { PokemonId = 128, Name = "Tauros" }, new Pokemon { PokemonId = 129, Name = "Magikarp" },
                new Pokemon { PokemonId = 130, Name = "Gyarados" }, new Pokemon { PokemonId = 131, Name = "Lapras" }, new Pokemon { PokemonId = 132, Name = "Ditto" },
                new Pokemon { PokemonId = 133, Name = "Eevee" }, new Pokemon { PokemonId = 134, Name = "Vaporeon" }, new Pokemon { PokemonId = 135, Name = "Jolteon" },
                new Pokemon { PokemonId = 136, Name = "Flareon" }
            };

            pokemons.ForEach(s => context.Pokemons.AddOrUpdate(p => p.PokemonId, s));
            context.SaveChanges();

            var country = new List<Country>
            {
                new Country { CountryId = 1, Name = "Canada" },
                new Country { CountryId = 2, Name = "South Korea" }
            };

            country.ForEach(s => context.Countrys.AddOrUpdate(p => p.CountryId, s));
            context.SaveChanges();

            var state = new List<State>
            {
                new State { StateId = 1, Name = "Ontario", CountryId = country.Single(x => x.Name == "Canada").CountryId },
                new State { StateId = 2, Name = "British Columbia", CountryId = country.Single(x => x.Name == "Canada").CountryId },
                new State { StateId = 3, Name = "Edmonton", CountryId = country.Single(x => x.Name == "Canada").CountryId },
            };

            state.ForEach(s => context.States.AddOrUpdate(p => p.StateId, s));
            context.SaveChanges();

            var city = new List<City>
            {
                new City { CityId = 1, Name = "Vancouver", StateId = state.Single(x => x.Name == "British Columbia").StateId },
                new City { CityId = 2, Name = "Toronto", StateId = state.Single(x => x.Name == "Ontario").StateId },
            };

            city.ForEach(s => context.Citys.AddOrUpdate(p => p.CityId, s));
            context.SaveChanges();
        }
    }
}