using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PokeGoTradeModel.Models;
using PokeGoTradeModel.ViewModels;
using PagedList;

namespace PokeGoTrade.Controllers
{  
    public class FindPokemonController : Controller
    {
        private PGTContext db = new PGTContext();

        // GET: FindPokemon
        [Authorize(Roles = "Admin, User")]
        public ActionResult Index()
        {
            PopulateCountryDropDownList();
            PopulatePokemonDropDownList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public ActionResult GetPokemonPosting(int? CountryId, int? state, int? city, int? PokemonId, int? page)
        {
            //must include navigation properties to get rid of lazy loading for ajax because it doesn't load model without it
            var model = db.Postings
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Pokemon)
                .Include(x => x.Pokemons)
                .Where(x => x.Country.CountryId == CountryId && x.State.StateId == state &&x.City.CityId == city)
                //loop through ICollection Pokemons to search for PokemonId
                .Where(x => x.Pokemons.Any(c => c.PokemonId == PokemonId))
                .ToList();
                
            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            //ViewBag.CurrentFilter = searchString;
            
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    var searchModels = db.Postings
            //    .Include(x => x.Country)
            //    .Include(x => x.City)
            //    .Include(x => x.Pokemon)
            //    .Include(x => x.Pokemons)
            //    .Where(s => s.Country.CountryName.Contains(searchString) || s.City.CityName.Contains(searchString) ||
            //           s.Pokemon.Name.Contains(searchString) || s.Pokemons.Select(x => x.Name).Contains(searchString))
            //    .ToList();

            //    int _pageSize = 10;
            //    int _pageNumber = (page ?? 1);

            //    return PartialView("PartialPosting", model.ToPagedList(_pageNumber, _pageSize));
            //}

            ViewBag.PokemonOwned = db.Pokemons
                .Where(x => x.PokemonId == PokemonId)
                .Select(x => x.Name);

            //ViewBags are used so it can be passed to the PartialPosting View.
            ViewBag.CountryId = CountryId;
            ViewBag.StateId = state;
            ViewBag.CityId = city;
            ViewBag.PokemonId = PokemonId;

            //viewbag to prohibit the current user to make the same offer more than once.
            ViewBag.DuplicateOffer = db.OfferMades
                                      .Where(x => x.CurrentUser == User.Identity.Name)
                                      .ToList();

            int pageSize = 5;
            int pageNumber = (page ?? 1);
                       
            return PartialView("PartialPosting", model.ToPagedList(pageNumber, pageSize));            
        }

        private void PopulateCountryDropDownList(object selectedCountry = null)
        {
            var countryQuery = from d in db.Countrys
                               orderby d.Name
                               select d;
            ViewBag.CountryId = new SelectList(countryQuery, "CountryId", "Name", selectedCountry);
        }

        private void PopulateStateDropDownList(object selectedState = null)
        {
            var stateQuery = from d in db.States
                             orderby d.Name
                             select d;
            ViewBag.StateId = new SelectList(stateQuery, "StateId", "Name", selectedState);
        }

        private void PopulateCityDropDownList(object selectedCity = null)
        {
            var cityQuery = from d in db.Citys
                            orderby d.Name
                            select d;
            ViewBag.CityId = new SelectList(cityQuery, "CityId", "Name", selectedCity);
        }

        private void PopulatePokemonDropDownList(object selectedPokemon = null)
        {
            var pokemonQuery = from d in db.Pokemons
                               orderby d.Name
                               select d;
            ViewBag.PokemonId = new SelectList(pokemonQuery, "PokemonId", "Name", selectedPokemon);
        }
          
        [Authorize(Roles = "Admin, User")]
        public ActionResult Offer(int postingId, string country, string state, string city, string currentUser, string selectedUser, string pokemonOwned, string pokemonWanted)
        {
            var viewModel = new OfferViewModel()
            {
                PostingId = postingId,
                Country = country,
                State = state,
                City = city,
                CurrentUser = currentUser,
                SelectedUser = selectedUser,
                PokemonOwned = pokemonOwned,
                PokemonWanted = pokemonWanted,
                DateTimeValue = DateTime.UtcNow
            };          

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public ActionResult PostOffer(OfferViewModel model)
        {           
            try
            {
                var offerMade = new OfferMade
                {
                    PostingId = model.PostingId,
                    CurrentUser = model.CurrentUser,
                    SelectedUser = model.SelectedUser,
                    Country = model.Country,
                    State = model.State,
                    City = model.City,
                    PokemonOwned = model.PokemonOwned,
                    PokemonWanted = model.PokemonWanted,
                    //initialization
                    Status = "Pending",
                    DateTimeValue = DateTime.UtcNow
                };
                       
                var offerReceived = new OfferReceived
                {
                    CurrentUser = model.CurrentUser,
                    SelectedUser = model.SelectedUser,
                    Country = model.Country,
                    State = model.State,
                    City = model.City,
                    PokemonOwned = model.PokemonOwned,
                    PokemonWanted = model.PokemonWanted,
                    Comments = model.Comments,
                    Choice = model.Choice,
                    DateTimeValue = DateTime.UtcNow
                };

                db.OfferMades.Add(offerMade);
                db.OfferReceiveds.Add(offerReceived);
                db.SaveChanges();

                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }            
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Comment(int postingId)
        {
            var comment = db.Postings
                            .Where(s => s.PostingId == postingId)
                            .FirstOrDefault();

            ViewBag.comment = comment.Comment;

            return View(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
