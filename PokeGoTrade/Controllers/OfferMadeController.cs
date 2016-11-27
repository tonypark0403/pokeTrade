using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PokeGoTradeModel.Models;
using PagedList;

namespace PokeGoTrade.Controllers
{   
    public class OfferMadeController : Controller
    {
        private PGTContext db = new PGTContext();

        // GET: OfferMade
        [Authorize(Roles = "Admin, User")]
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            var offerMadeModel = db.OfferMades
                                   .Where(x => x.CurrentUser == User.Identity.Name)
                                   .ToList();

            //In order to convert it to a List in the View (ex: var postingId = (List<int>)ViewBag.postingId;),
            //.ToList() is required.
            var postingId = db.Postings.Select(x => x.PostingId).ToList();

            ViewBag.postingId = postingId;

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
                var searchModels = db.OfferMades             
                .Where(x => x.CurrentUser == User.Identity.Name)
                .Where(s => s.Country.Contains(searchString) || s.State.Contains(searchString) || s.City.Contains(searchString) || s.SelectedUser.Contains(searchString) || s.PokemonOwned.Contains(searchString) || s.PokemonWanted.Contains(searchString) || s.Status.Contains(searchString))
                .ToList();

                int _pageSize = 5;
                int _pageNumber = (page ?? 1);

                return View(searchModels.ToPagedList(_pageNumber, _pageSize));
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(offerMadeModel.ToPagedList(pageNumber, pageSize));           
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Confirmed(string username, int postingId)
        {
            var user = db.Postings.Where(x => x.Username == username)
                                  .Where(s=>s.PostingId == postingId)
                                  .FirstOrDefault();

            ViewBag.userName = user.Username;
            ViewBag.email = user.Email;
            ViewBag.telephone = user.Telephone;
            ViewBag.comment = user.Comment;

            return View(user);
        }


        // GET: OfferMade/Delete/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferMade offerMade = db.OfferMades.Find(id);
            if (offerMade == null)
            {
                return HttpNotFound();
            }
            return View(offerMade);
        }

        // POST: OfferMade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferMade offerMade = db.OfferMades.Find(id);
            db.OfferMades.Remove(offerMade);
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
