using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tydee.DataModels;
using Tydee.Models;
using System.IO;

namespace Tydee.Controllers
{
    public class CollectionsController : Controller
    {
        private TydeeDataModelContainer db = new TydeeDataModelContainer();

        // GET: Collections
        public async Task<ActionResult> Index()
        {
            var collections = db.Collections.Include(c => c.User);
            return View(await collections.ToListAsync());
        }

        // GET: Collections/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.Collections.FindAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: Users/Import
        public ActionResult Import()
        {
            return View();
        }

        // POST: Collections/Import
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileBase json)
        {
            ApiResponse result = new ApiResponse();
            int collectionsCreated = 0;

            try
            {

                // Verify that the user selected a file
                if (json != null && json.ContentLength > 0)
                {
                    StreamReader sr = new System.IO.StreamReader(json.InputStream);

                    while (sr.Peek() >= 0)
                    {
                        dynamic data = System.Web.Helpers.Json.Decode(sr.ReadLine());

                        //find imported user or continue

                        Collection collection = new Collection()
                        {
                            
                            CollectionOption = new CollectionOption()
                            {

                            }
                        };

                        db.Collections.Add(collection);
                        
                    }
                    if (db.ChangeTracker.HasChanges()) db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                result.Data = ex.StackTrace;
            }

            result.Data = new
            {
                CollectionsCreated = collectionsCreated
            };

            return View(result);
        }

        // GET: Collections/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,EBayName,ShopInfo,UserId")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Collections.Add(collection);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", collection.UserId);
            return View(collection);
        }

        // GET: Collections/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.Collections.FindAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", collection.UserId);
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,EBayName,ShopInfo,UserId")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collection).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", collection.UserId);
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.Collections.FindAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Collection collection = await db.Collections.FindAsync(id);
            db.Collections.Remove(collection);
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
