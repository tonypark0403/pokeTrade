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
using System.Data.Entity.Infrastructure;
using PagedList;

namespace PokeGoTrade.Controllers
{    
    public class PostingController : Controller
    {
        private PGTContext db = new PGTContext();

        // GET: Posting
        [Authorize(Roles = "Admin, User")]
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {

            //var viewModel = new PostingIndexData();
            //viewModel.Postings = db.Postings
            //    .Include(i => i.Pokemon)
            //    .Include(i => i.Pokemons);  

            var model = db.Postings
                .Include(i => i.Pokemon)
                .Include(i => i.Pokemons)
                .Where(x => x.Username == User.Identity.Name)
                .ToList();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                var searchModels = db.Postings
                .Include(i => i.Pokemon)
                .Include(i => i.Pokemons)
                .Where(x => x.Username == User.Identity.Name)
                .Where(s => s.Pokemon.Name.Contains(searchString) || 
                s.Pokemons.Select(x => x.Name).Contains(searchString) || 
                s.Country.Name.Contains(searchString) || 
                s.City.Name.Contains(searchString) || 
                s.State.Name.Contains(searchString) ||
                s.Comment.Contains(searchString))                               
                .ToList();

                int _pageSize = 5;
                int _pageNumber = (page ?? 1);

                return View(searchModels.ToPagedList(_pageNumber, _pageSize));
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(model.ToPagedList(pageNumber, pageSize));           
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult CreateViewIndex()
        {
            return View();
        }

        
        // GET: Posting/Create
        [Authorize(Roles = "Admin, User")]
        public ActionResult Create()
        {
            var posting = new Posting();            
            posting.Pokemons = new List<Pokemon>();
            PopulateAssignedPokemonData(posting);
            PopulateCountryDropDownList();          
            PopulatePokemonDropDownList();

            return View();
        }

        // POST: Posting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public ActionResult Create([Bind(Include = "Email,Telephone,Comment,CountryId,PokemonId")] Posting posting, string[] selectedPokemons, int State, int City)
        {
            posting.Username = User.Identity.Name;

            if (selectedPokemons != null)
            {
                posting.Pokemons = new List<Pokemon>();
                foreach (var pokemon in selectedPokemons)
                {
                    var pokemonToAdd = db.Pokemons.Find(int.Parse(pokemon));
                    posting.Pokemons.Add(pokemonToAdd);
                }   
            }

            //City,State 드롭다운이 정상적이지않아서 이렇게해야함. ㅈㅈ
            posting.StateId = State;
            posting.CityId = City;
            posting.DateTimeValue = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                db.Postings.Add(posting);
                db.SaveChanges();

                TempData["Message"] = "Created Successfully";

                return RedirectToAction("Index");
            }

            //sets the selected item when they redisplay the page after an error
            PopulateAssignedPokemonData(posting);
            PopulateCountryDropDownList(posting.CountryId);
            PopulateStateDropDownList(posting.StateId);
            PopulateCityDropDownList(posting.CityId);
            PopulatePokemonDropDownList(posting.PokemonId);

            TempData["Message"] = "Failed Creating a post";

            return View(posting);
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult FillState(int country)
        {
            var states = db.States.Where(c => c.CountryId == country);
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult FillCity(int state)
        {
            var cities = db.Citys.Where(c => c.StateId == state);
            return Json(cities, JsonRequestBehavior.AllowGet);
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

        private void PopulateAssignedPokemonData(Posting posting)
        {
            var allPokemons = db.Pokemons.OrderBy(x=>x.Name);
            var postingPokemons = new HashSet<int>(posting.Pokemons.Select(c => c.PokemonId));
            var viewModel = new List<AssignedPokemonData>();

            foreach (var pokemon in allPokemons)
            {
                viewModel.Add(new AssignedPokemonData
                {
                    PokemonID = pokemon.PokemonId,
                    Name = pokemon.Name,
                    Assigned = postingPokemons.Contains(pokemon.PokemonId)
                });
            }
            ViewBag.Pokemons = viewModel;
        }

        // GET: Posting/Edit/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Posting posting = db.Postings           
            .Include(i => i.Pokemons)
            .Where(i => i.PostingId == id)
            .Single();

            PopulateAssignedPokemonData(posting);

            //Posting posting = db.Postings.Find(id);
            if (posting == null)
            {
                return HttpNotFound();
            }
            return View(posting);
        }

        // POST: Posting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public ActionResult Edit(int? id, string[] selectedPokemons)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }           

            var postingToUpdate = db.Postings
            .Include(i => i.Pokemons)
            .Where(i => i.PostingId == id)
            .Single();

            if (TryUpdateModel(postingToUpdate, "",
               new string[] { "Country", "State", "City", "Email", "Telephone", "IWant", "Comment" }))
            {
                try
                {
                    UpdatePostingPokemon(selectedPokemons, postingToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            PopulateAssignedPokemonData(postingToUpdate);

            return View(postingToUpdate);
        }

        private void UpdatePostingPokemon(string[] selectedPokemons, Posting postingToUpdate)
        {
            if (selectedPokemons == null)
            {
                postingToUpdate.Pokemons = new List<Pokemon>();
                return;
            }

            var selectedPokemonHS = new HashSet<string>(selectedPokemons);
            var postingPokemon = new HashSet<int>
                (postingToUpdate.Pokemons.Select(c => c.PokemonId));

            foreach (var pokemon in db.Pokemons)
            {
                if (selectedPokemonHS.Contains(pokemon.PokemonId.ToString()))
                {
                    if (!postingPokemon.Contains(pokemon.PokemonId))
                    {
                        postingToUpdate.Pokemons.Add(pokemon);
                    }
                }
                else
                {
                    if (postingPokemon.Contains(pokemon.PokemonId))
                    {
                        postingToUpdate.Pokemons.Remove(pokemon);
                    }
                }
            }
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Comment(int postingId)
        {
            var comment = db.Postings.Where(s => s.PostingId == postingId)
                                  .FirstOrDefault();
           
            ViewBag.comment = comment.Comment;

            return View(comment);
        }

        // GET: Posting/Delete/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posting posting = db.Postings.Find(id);
            if (posting == null)
            {
                return HttpNotFound();
            }
            return View(posting);
        }

        // POST: Posting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public ActionResult DeleteConfirmed(int id)
        {
            Posting posting = db.Postings.Find(id);
            db.Postings.Remove(posting);
            db.SaveChanges();
            return RedirectToAction("Index");
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
