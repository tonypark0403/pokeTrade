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
    public class OfferReceivedController : Controller
    {
        private PGTContext db = new PGTContext();

        // GET: OfferReceived
        [Authorize(Roles = "Admin, User")]
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            var model = db.OfferReceiveds
                          .Where(x => x.SelectedUser == User.Identity.Name)
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
                var searchModels = db.OfferReceiveds
                .Where(x => x.SelectedUser == User.Identity.Name)
                .Where(s => s.Country.Contains(searchString) || s.State.Contains(searchString) || s.City.Contains(searchString) || s.CurrentUser.Contains(searchString) || s.PokemonOwned.Contains(searchString) || s.PokemonWanted.Contains(searchString) || s.Comments.Contains(searchString) || s.Choice.Contains(searchString))
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
        public ActionResult Choice(int id, string choice)
        {
            try
            {
                //Find method can take null value
                OfferMade offerMade = db.OfferMades.Find(id);
                OfferReceived offerReceived = db.OfferReceiveds.Find(id);

                if (offerMade == null)
                {
                    //var userName = offerReceived.CurrentUser;
                    offerReceived.Choice = "Deleted";
                    db.SaveChanges();
                    return Json(new { status = "null" }, JsonRequestBehavior.AllowGet);                    
                }

                var offerMadeModel = db.OfferMades
                              .Where(x => x.OfferMadeId == id)
                              .Single();
                
                if (choice.Equals("Accepted"))
                {
                    offerMadeModel.Status = "Confirmed";
                }
                if (choice.Equals("Declined"))
                {
                    offerMadeModel.Status = "Declined";
                }

                var offerReceivedModel = db.OfferReceiveds
                                           .Where(x => x.OfferReceivedId == id)
                                           .Single();

                offerReceivedModel.Choice = choice;
                                            
                db.SaveChanges();
              
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);           
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Comment(int offerReceivedId)
        {
            var comment = db.OfferReceiveds
                            .Where(s => s.OfferReceivedId == offerReceivedId)
                            .FirstOrDefault();

            ViewBag.comment = comment.Comments;

            return View(comment);
        }


        // GET: OfferReceived/Delete/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferReceived offerReceived = db.OfferReceiveds.Find(id);
            if (offerReceived == null)
            {
                return HttpNotFound();
            }
            return View(offerReceived);
        }

        // POST: OfferReceived/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferReceived offerReceived = db.OfferReceiveds.Find(id);
            db.OfferReceiveds.Remove(offerReceived);
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
