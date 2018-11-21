using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParcialDos.Models;


namespace ParcialDos.Controllers
{
    public class RecepcionController : Controller
    {
        private Parcial2Entities db = new Parcial2Entities();

        // GET: Recepcion
        public ActionResult Index()
        {
            var recepcion = db.Recepcion.Include(r => r.Clientes).Include(r => r.Laboratorios);
          
            return View(recepcion.ToList());
        }

        // GET: Recepcion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcion recepcion = db.Recepcion.Find(id);
            if (recepcion == null)
            {
                return HttpNotFound();
            }
            return View(recepcion);
        }

        // GET: Recepcion/Create
        public ActionResult Create()
        {
            ViewBag.CliRut = new SelectList(db.Clientes, "CliRut", "CliNombre");
            ViewBag.LabId = new SelectList(db.Laboratorios, "LabId", "LabNombre");
            return View();
        }

        // POST: Recepcion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecId,CliRut,LabId,RecFolio,RecFechaRecepcion,RecFechaEntrega,RecLocalidad,RecCantidadMuestras")] Recepcion recepcion)
        {
            if (ModelState.IsValid)
            {
                
                DateTime Entrega = recepcion.RecFechaRecepcion.Value;
                Entrega.ToShortDateString();
                recepcion.RecFechaEntrega= Entrega.AddDays(2);
                db.Recepcion.Add(recepcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CliRut = new SelectList(db.Clientes, "CliRut", "CliNombre", recepcion.CliRut);
            ViewBag.LabId = new SelectList(db.Laboratorios, "LabId", "LabNombre", recepcion.LabId);
            return View(recepcion);
        }

        // GET: Recepcion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcion recepcion = db.Recepcion.Find(id);
            if (recepcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.CliRut = new SelectList(db.Clientes, "CliRut", "CliNombre", recepcion.CliRut);
            ViewBag.LabId = new SelectList(db.Laboratorios, "LabId", "LabNombre", recepcion.LabId);
            return View(recepcion);
        }

        // POST: Recepcion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecId,CliRut,LabId,RecFolio,RecFechaRecepcion,RecFechaEntrega,RecLocalidad,RecCantidadMuestras")] Recepcion recepcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recepcion).State = EntityState.Modified;
                DateTime Entrega = recepcion.RecFechaRecepcion.Value;
 
                recepcion.RecFechaEntrega = Entrega.AddDays(2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CliRut = new SelectList(db.Clientes, "CliRut", "CliNombre", recepcion.CliRut);
            ViewBag.LabId = new SelectList(db.Laboratorios, "LabId", "LabNombre", recepcion.LabId);
            return View(recepcion);
        }

        // GET: Recepcion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcion recepcion = db.Recepcion.Find(id);
            if (recepcion == null)
            {
                return HttpNotFound();
            }
            return View(recepcion);
        }

        // POST: Recepcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recepcion recepcion = db.Recepcion.Find(id);
            db.Recepcion.Remove(recepcion);
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
