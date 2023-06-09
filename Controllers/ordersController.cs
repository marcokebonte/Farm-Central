﻿using Farm_Central.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Farm_Central.Controllers
{


    [CustomAuthorize(Roles = "farmer,employee")]
    public class ordersController : Controller
    {
        private farm_centralEntities db = new farm_centralEntities();

        // GET: orders
        public ActionResult Index()
        {
            var orders = db.orders.Include(o => o.customer).Include(o => o.product);
            return View(orders.ToList());
        }

        // GET: orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: orders/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "fullname");
            ViewBag.product_id = new SelectList(db.products, "product_id", "name");
            return View();
        }

        // POST: orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,customer_id,product_id,quantity,unit_price,total_price")] order order)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the unit price from the product table based on the product_id
                var product = db.products.Find(order.product_id);
                if (product != null)
                {
                    order.unit_price = product.price;
                    order.total_price = order.unit_price * order.quantity;
                }

                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "fullname", order.customer_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "name", order.product_id);
            return View(order);
        }

        // GET: orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "fullname", order.customer_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "name", order.product_id);
            return View(order);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,customer_id,product_id,quantity,unit_price,total_price")] order order)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the unit price from the product table based on the product_id
                var product = db.products.Find(order.product_id);
                if (product != null)
                {
                    order.unit_price = product.price;
                    order.total_price = order.unit_price * order.quantity;
                }

                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "fullname", order.customer_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "name", order.product_id);
            return View(order);
        }

        // GET: orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order order = db.orders.Find(id);
            db.orders.Remove(order);
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
