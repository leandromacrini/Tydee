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
using System.IO;
using Tydee.Models;

namespace Tydee.Controllers
{
    public class UsersController : Controller
    {
        private TydeeDataModelContainer db = new TydeeDataModelContainer();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Import
        public ActionResult Import()
        {
            return View();
        }

        // POST: Users/Import
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileBase json)
        {
            ApiResponse result = new ApiResponse();
            int usersCreated = 0;

            try
            {
                
                // Verify that the user selected a file
                if (json != null && json.ContentLength > 0)
                {
                    StreamReader sr = new System.IO.StreamReader(json.InputStream);

                    while(sr.Peek() >= 0){
                        dynamic data = System.Web.Helpers.Json.Decode(sr.ReadLine());

                        // Create new User

                        User user = new User()
                        {
                            FirstName = data.first_name ?? "No First Name",
                            LastName = data.last_name ?? "No Last Name",
                            CreationDate = Convert.ToDateTime(data.created_at["$date"]),
                            LastAccessDate = Convert.ToDateTime(data.updated_at["$date"]),
                            UserName = data.username,
                            Password = data.username,
                            Email = data.email ?? "No eMail!",
                            SocialCreated = data.social_created ?? false,
                            UserOption = new UserOption()
                            {
                                ShowPhoto = data.custom_fields.show_photo ?? true,
                                CollectionOrder = data.custom_fields.collection_order == "name" ? CollectionOrders.ByName : CollectionOrders.ByCreationDate,
                                DialogOnNew = data.custom_fields.dialog_on_newitem ?? false,
                                DefaultCollectionOption = new CollectionOption()
                                {
                                    AlphaOrder = data.custom_fields.alpha_order_default == "name",
                                    OwnedFirst = data.custom_fields.owned_first_default ?? true,
                                    FavoriteFirst = data.custom_fields.favorite_first_default ?? true,
                                    ShowSearched = data.custom_fields.show_searched_default ?? true,
                                    ShowArchived = data.custom_fields.show_archived_default ?? false,
                                    ShowExchanged = data.custom_fields.show_exchanged_default ?? false,
                                    ShowSold = data.custom_fields.show_sold_default ?? false
                                }
                            }
                        };

                    

                        // Create new Shop

                        Shop shop = new Shop()
                        {
                            Enabled = data.custom_fields.shop_enabled ?? false,
                            PaymentInfo = data.custom_fields.payment_info,
                            ShipmentInfo = data.custom_fields.shipment_info,
                            ShowOwned = data.custom_fields.show_owned ?? false,
                            ShowSalable = data.custom_fields.show_salable ?? true,
                            ShowSearched = data.custom_fields.show_searched ?? true
                        };

                        shop.User = user;

                        db.Users.Add(user);
                        db.Shops.Add(shop);

                        usersCreated++;
                    }
                    if(db.ChangeTracker.HasChanges()) db.SaveChanges();
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
                UserCreated = usersCreated
            };

            return View(result);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Email,UserName,Password,CreationDate,LastAccessDate,FirstName,LastName,SocialCreated")] User user)
        {
            if (ModelState.IsValid)
            {
                //create UserOption and DefaultCollectionOption
                user.UserOption = new UserOption() {
                    ShowPhoto = true,
                    CollectionOrder = CollectionOrders.ByName,
                    DialogOnNew = false,
                    DefaultCollectionOption = new CollectionOption() {
                        AlphaOrder = true,
                        OwnedFirst = true,
                        FavoriteFirst = true,
                        ShowSearched = true,
                        ShowArchived = false,
                        ShowExchanged = false,
                        ShowSold = false
                    }
                };

                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,UserName,Password,CreationDate,LastAccessDate,FirstName,LastName,SocialCreated")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
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
