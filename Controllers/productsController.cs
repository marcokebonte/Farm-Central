using Farm_Central.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Farm_Central.Controllers
{

    [CustomAuthorize(Roles = "farmer,employee")]

    public class productsController : Controller
    {
        private farm_centralEntities db = new farm_centralEntities();

        // GET: products
        public ActionResult Index(string filterBy, string searchString)
        {
            var products = db.products.Include(p => p.farm);

            // Apply filtering based on the selected criteria
            if (!string.IsNullOrEmpty(filterBy))
            {
                if (filterBy == "Type")
                {
                    products = products.Where(p => p.type.Contains(searchString));
                }
                else if (filterBy == "Date")
                {
                    DateTime searchDate;
                    if (DateTime.TryParse(searchString, out searchDate))
                    {
                        products = products.Where(p => p.date_range.HasValue && p.date_range.Value.Date == searchDate.Date);
                    }
                }
                else if (filterBy == "Farm")
                {
                    products = products.Where(p => p.farm.name.Contains(searchString));
                }
            }

            return View(products.ToList());
        }



        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.farm_id = new SelectList(db.farms, "farm_id", "name");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,farm_id,name,type,price,date_range")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.farm_id = new SelectList(db.farms, "farm_id", "name", product.farm_id);
            return View(product);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.farm_id = new SelectList(db.farms, "farm_id", "name", product.farm_id);
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,farm_id,name,type,price,date_range")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.farm_id = new SelectList(db.farms, "farm_id", "name", product.farm_id);
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
