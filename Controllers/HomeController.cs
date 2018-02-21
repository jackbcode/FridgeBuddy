using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FridgeBuddy.Models;

namespace FridgeBuddy.Controllers
{
    public class HomeController : Controller
    {
        private FridgeBuddyEntities db = new FridgeBuddyEntities();

        // GET: Main
        public ActionResult Index(string foodCategory, string searchString, string sortOrder)
        {
           
            //Food Category Search
            //retrieve data from database cast into a list and use as dropdownList on html markup

            var CategoryList = new List<string>();
            var CategoryQuery = from f in db.Fridges
                                orderby f.Category
                                select f.Category;
            CategoryList.AddRange(CategoryQuery.Distinct());
            //create list of Categories, make sure distinct so no duplicates.
            ViewBag.foodCategory = new SelectList(CategoryList);


            //LINQ Query to retrieve all fridge contents
            var fridgeItem = from i in db.Fridges
                             select i;
            if (!String.IsNullOrEmpty(foodCategory))
            {
                fridgeItem = fridgeItem.Where(x => x.Category == foodCategory);
            }

            //title search 

            if (!String.IsNullOrEmpty(searchString))
            {
                fridgeItem = fridgeItem.Where(x => x.Item.Contains(searchString));
            }

            //Orders items by earliest expiry date 

            fridgeItem =  fridgeItem.OrderBy(i => i.ExpiryDate);

           




            return View(fridgeItem);


            
        }

     



        // GET: Main/Details/5
        public ActionResult Details(int? id)
        {
            Fridge fridgeItem = db.Fridges.Find(id);
            return View(fridgeItem);





            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Fridge fridge = db.Fridges.Find(id);
            //if (fridge == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(fridge);
        }

        //// GET: Main/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Main/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Picture,Item,Category,Quantity,Location,ExpiryDate,DaysTillExpiry")] Fridge fridgeItem)
        {

            if (fridgeItem.Picture == null)
            {
                fridgeItem.Picture = "https://cdn.pixabay.com/photo/2013/07/12/19/05/no-food-154333_960_720.png";
            }


            if (ModelState.IsValid)
            {
                db.Fridges.Add(fridgeItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // return to create view screen with food item data input entered so can edit
            return View(fridgeItem);
        }

        // GET: Main/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fridge fridgeItem = db.Fridges.Find(id);
            if (fridgeItem == null)
            {
                return HttpNotFound();
            }
            return View(fridgeItem);
        }

        // POST: Main/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Picture,Item,Category,Quantity,Location,ExpiryDate,DaysTillExpiry")] Fridge fridgeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fridgeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fridgeItem);
        }

        // GET: Main/Delete/5
        public ActionResult Delete(int? id)
        {



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fridge fridgeItem = db.Fridges.Find(id);
            if (fridgeItem == null)
            {
                return HttpNotFound();
            }
            return View(fridgeItem);
        }

        // POST: Main/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fridge fridgeItem = db.Fridges.Find(id);
            db.Fridges.Remove(fridgeItem);
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
