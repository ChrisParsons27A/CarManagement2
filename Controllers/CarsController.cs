using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarManagement;

namespace CarManagement.Controllers
{
    public class CarsController : Controller
    {
        private CarManagementEntities db = new CarManagementEntities();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.Colour).Include(c => c.Engine).Include(c => c.Manufacturer).Include(c => c.Model).Include(c => c.Status);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.ColourID = new SelectList(db.Colours, "ColourID", "Name");
            ViewBag.EngineID = new SelectList(db.Engines, "EngineID", "Size");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerId", "Name");
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "Name");
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarID,ManufacturerID,ModelID,EngineID,ColourID,Mileage,Condition,StatusID,ApproxValue,MOTDueDate")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ColourID = new SelectList(db.Colours, "ColourID", "Name", car.ColourID);
            ViewBag.EngineID = new SelectList(db.Engines, "EngineID", "Size", car.EngineID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerId", "Name", car.ManufacturerID);
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "Name", car.ModelID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Name", car.StatusID);
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColourID = new SelectList(db.Colours, "ColourID", "Name", car.ColourID);
            ViewBag.EngineID = new SelectList(db.Engines, "EngineID", "Size", car.EngineID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerId", "Name", car.ManufacturerID);
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "Name", car.ModelID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Name", car.StatusID);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarID,ManufacturerID,ModelID,EngineID,ColourID,Mileage,Condition,StatusID,ApproxValue,MOTDueDate")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColourID = new SelectList(db.Colours, "ColourID", "Name", car.ColourID);
            ViewBag.EngineID = new SelectList(db.Engines, "EngineID", "Size", car.EngineID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerId", "Name", car.ManufacturerID);
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "Name", car.ModelID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Name", car.StatusID);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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
