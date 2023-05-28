using Farm_Central.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Farm_Central.Controllers
{


    [CustomAuthorize(Roles = "farmer")]


    public class farmController : Controller
    {
        private farm_centralEntities db = new farm_centralEntities();

        // GET: farm
        public ActionResult Index()
        {
            var farms = db.farms.Include(f => f.farmer);
            return View(farms.ToList());
        }

        // GET: farm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farm farm = db.farms.Find(id);
            if (farm == null)
            {
                return HttpNotFound();
            }
            return View(farm);
        }

        // GET: farm/Create
        public ActionResult Create()
        {
            ViewBag.farmer_id = new SelectList(db.farmers, "farmer_id", "fullname");
            return View();
        }

        // POST: farm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "farm_id,farmer_id,name,location")] farm farm)
        {
            if (ModelState.IsValid)
            {
                db.farms.Add(farm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.farmer_id = new SelectList(db.farmers, "farmer_id", "fullname", farm.farmer_id);
            return View(farm);
        }


        // GET: farm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farm farm = db.farms.Find(id);
            if (farm == null)
            {
                return HttpNotFound();
            }
            ViewBag.farmer_id = new SelectList(db.farmers, "farmer_id", "fullname", farm.farmer_id);
            return View(farm);
        }

        // POST: farm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "farm_id,farmer_id,name,location")] farm farm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.farmer_id = new SelectList(db.farmers, "farmer_id", "fullname", farm.farmer_id);
            return View(farm);
        }

        // GET: farm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farm farm = db.farms.Find(id);
            if (farm == null)
            {
                return HttpNotFound();
            }
            return View(farm);
        }

        // POST: farm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            farm farm = db.farms.Find(id);
            db.farms.Remove(farm);
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
