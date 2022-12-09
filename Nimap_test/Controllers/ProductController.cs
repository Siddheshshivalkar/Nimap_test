using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nimap_test.Context;

namespace Nimap_test.Controllers
{
    public class ProductController : Controller
    {
        private productdbEntities db = new productdbEntities();

        // GET: /Product/
        public async Task<ActionResult> Index()
        {
            var tbl_product = db.tbl_product.Include(t => t.tbl_category);
            return View(await tbl_product.ToListAsync());
        }

        // GET: /Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = await db.tbl_product.FindAsync(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_product);
        }

        // GET: /Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.tbl_category, "CategoryID", "CategoryID");
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ProductID,ProductName,CategoryID,CategoryName")] tbl_product tbl_product)
        {
            if (ModelState.IsValid)
            {
                db.tbl_product.Add(tbl_product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.tbl_category, "CategoryID", "CategoryName", tbl_product.CategoryID);
            return View(tbl_product);
        }

        // GET: /Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = await db.tbl_product.FindAsync(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.tbl_category, "CategoryID", "CategoryName", tbl_product.CategoryID);
            return View(tbl_product);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ProductID,ProductName,CategoryID,CategoryName")] tbl_product tbl_product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.tbl_category, "CategoryID", "CategoryName", tbl_product.CategoryID);
            return View(tbl_product);
        }

        // GET: /Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = await db.tbl_product.FindAsync(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tbl_product tbl_product = await db.tbl_product.FindAsync(id);
            db.tbl_product.Remove(tbl_product);
            await db.SaveChangesAsync();
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
