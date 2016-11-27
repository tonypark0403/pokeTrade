namespace PokeGoTrade.Migrations.PokeGoTradeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        SortName = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.FindPokemon",
                c => new
                    {
                        FindPokemonId = c.Int(nullable: false, identity: true),
                        Country = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        City = c.Int(nullable: false),
                        IWant = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FindPokemonId);
            
            CreateTable(
                "dbo.OfferMade",
                c => new
                    {
                        OfferMadeId = c.Int(nullable: false, identity: true),
                        PostingId = c.Int(nullable: false),
                        CurrentUser = c.String(),
                        SelectedUser = c.String(),
                        Country = c.String(),
                        State = c.String(),
                        City = c.String(),
                        PokemonOwned = c.String(),
                        PokemonWanted = c.String(),
                        Status = c.String(),
                        DateTimeValue = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OfferMadeId);
            
            CreateTable(
                "dbo.OfferReceived",
                c => new
                    {
                        OfferReceivedId = c.Int(nullable: false, identity: true),
                        CurrentUser = c.String(),
                        SelectedUser = c.String(),
                        Country = c.String(),
                        State = c.String(),
                        City = c.String(),
                        PokemonOwned = c.String(),
                        PokemonWanted = c.String(),
                        Comments = c.String(),
                        Choice = c.String(),
                        DateTimeValue = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OfferReceivedId);
            
            CreateTable(
                "dbo.Pokemon",
                c => new
                    {
                        PokemonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PokemonId);
            
            CreateTable(
                "dbo.Posting",
                c => new
                    {
                        PostingId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Telephone = c.String(),
                        Comment = c.String(),
                        DateTimeValue = c.DateTime(nullable: false),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        PokemonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostingId)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Pokemon", t => t.PokemonId, cascadeDelete: true)
                .ForeignKey("dbo.State", t => t.StateId, cascadeDelete: true)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.PokemonId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.PostingPokemon",
                c => new
                    {
                        PokemonId = c.Int(nullable: false),
                        PostingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PokemonId, t.PostingId })
                .ForeignKey("dbo.Pokemon", t => t.PokemonId, cascadeDelete: true)
                .ForeignKey("dbo.Posting", t => t.PostingId, cascadeDelete: false)
                .Index(t => t.PokemonId)
                .Index(t => t.PostingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostingPokemon", "PostingId", "dbo.Posting");
            DropForeignKey("dbo.PostingPokemon", "PokemonId", "dbo.Pokemon");
            DropForeignKey("dbo.Posting", "StateId", "dbo.State");
            DropForeignKey("dbo.Posting", "PokemonId", "dbo.Pokemon");
            DropForeignKey("dbo.Posting", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Posting", "CityId", "dbo.City");
            DropIndex("dbo.PostingPokemon", new[] { "PostingId" });
            DropIndex("dbo.PostingPokemon", new[] { "PokemonId" });
            DropIndex("dbo.Posting", new[] { "PokemonId" });
            DropIndex("dbo.Posting", new[] { "CityId" });
            DropIndex("dbo.Posting", new[] { "StateId" });
            DropIndex("dbo.Posting", new[] { "CountryId" });
            DropTable("dbo.PostingPokemon");
            DropTable("dbo.State");
            DropTable("dbo.Posting");
            DropTable("dbo.Pokemon");
            DropTable("dbo.OfferReceived");
            DropTable("dbo.OfferMade");
            DropTable("dbo.FindPokemon");
            DropTable("dbo.Country");
            DropTable("dbo.City");
        }
    }
}
