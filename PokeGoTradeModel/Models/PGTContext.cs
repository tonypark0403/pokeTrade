using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PokeGoTradeModel.Models
{
    public class PGTContext : DbContext
    {
        public PGTContext() : base("PokeGoTradeDB")
        { }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Posting> Postings { get; set; }
        public DbSet<FindPokemon> FindPokemons { get; set; }
        public DbSet<Country> Countrys { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Citys { get; set; }        
        public DbSet<OfferMade> OfferMades { get; set; }
        public DbSet<OfferReceived> OfferReceiveds { get; set; }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Pokemon>()
                .HasMany(c => c.Postings).WithMany(i => i.Pokemons)
                .Map(t => t.MapLeftKey("PokemonId")
                    .MapRightKey("PostingId")
                    .ToTable("PostingPokemon"));
        }
    }
}
