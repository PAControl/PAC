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
        PACbdEntities1 db = new PACbdEntities1();

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
        //Redirección a la vista de formulario para crear un local.
        public ActionResult Create()
        {
            return View("~/Views/Registro/Create.cshtml");
        }

        // POST: Local/Create
        //Tratamiento de los datos del formulario y redirección a la lista de locales.
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                Empresa e = new Empresa();
                e.nombre = collection["empN"];
                e.CIF = collection["cif"];

                db.Empresa.Add(e);
                db.SaveChanges();

                Local l = new Local();
                l.usuario = collection["user"];
                l.contraseña = collection["password"];
                l.direccion = collection["dir"];
                l.telefono = Int32.Parse(collection["tel"]);
                l.aforoMax = Int32.Parse(collection["afM"]);
                l.id_empresa = Int32.Parse(e.id.ToString());
                
                db.Local.Add(l);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Local/Edit/5
        // Nos enviara a la página de edición del local.
        public ActionResult Edit(int id)
        {

            Empresa em = new Empresa();
            int id_empresa = db.Local.Find(id).id_empresa;
            em = db.Empresa.Find(id_empresa);

            Local lo = new Local();
            lo = db.Local.Find(id);

            return View("~/Views/EditarLocal/Editar.cshtml");
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
