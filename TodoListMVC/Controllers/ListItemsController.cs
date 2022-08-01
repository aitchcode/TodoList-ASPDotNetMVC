using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoListMVC.Models;

namespace TodoListMVC.Controllers
{
    public class ListItemsController : Controller
    {
        private TodoListDBContext db = new TodoListDBContext();
        
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var listItems = from i in db.ListItems
                            where i.UserId == id
                            orderby i.Id
                            select i;
            ViewBag.id = id;
            ViewBag.name = user.Name;
            return View(listItems);
        }

        // GET: ListItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListItem listItem = db.ListItems.Find(id);
            if (listItem == null)
            {
                return HttpNotFound();
            }
            return View(listItem);
        }

        // GET: ListItems/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = user.Id;
            return View();
        }

        // POST: ListItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            ListItem item = new ListItem(Convert.ToInt32(form["userId"]), form["text"]);
            db.ListItems.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index/" + form["userId"]);
        }

        // GET: ListItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListItem listItem = db.ListItems.Find(id);
            if (listItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", listItem.UserId);
            return View(listItem);
        }

        // POST: ListItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Text,CheckedStatus")] ListItem listItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/"+ listItem.UserId);
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", listItem.UserId);
            return View(listItem);
        }

        // GET: ListItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListItem listItem = db.ListItems.Find(id);
            if (listItem == null)
            {
                return HttpNotFound();
            }
            return View(listItem);
        }

        // POST: ListItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListItem listItem = db.ListItems.Find(id);
            int userId = listItem.UserId;
            db.ListItems.Remove(listItem);
            db.SaveChanges();
            return RedirectToAction("Index/"+ userId);
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
