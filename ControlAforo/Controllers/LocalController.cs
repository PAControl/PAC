using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControlAforo.Models;

namespace ControlAforo.Controllers
{
    public class LocalController : Controller
    {

        //Obtener el contexto de la BD.
        PACbdEntities db = new PACbdEntities();

        // GET: Local
        public ActionResult Index()
        {
            //Consulta para obtener todos los datos de los locales.
            List<Local> locales = db.Local.ToList();

            return View(locales);
        }

        // GET: Local/Details/5
        public ActionResult Details(int id)
        {

            //List<Local> detalleLocal = db.Local.Where(l => l.id == id).ToList();
            Local detalleLocal = db.Local.Find(id);



            return View(detalleLocal);
        }

        // GET: Local/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Local/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Local/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Local/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Local/Delete/5
        public ActionResult Delete(int id)
        {
            //Eliminar un local
            Local localEliminado = db.Local.Find(id);
            db.Local.Remove(localEliminado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Local/Delete/5
       /* [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Local localEliminado = db.Local.Find(id);
                db.Local.Remove(localEliminado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Local localEliminado = db.Local.Find(id);
                db.Local.Remove(localEliminado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }*/

    }
}
