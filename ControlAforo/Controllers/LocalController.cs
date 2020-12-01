using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
                //Instanciamos el objeto Empresa mediante los datos obtenidos en el formulario.
                Empresa e = new Empresa();
                e.nombre = collection["empN"];
                e.CIF = collection["cif"];

                //Añadimos la empresa a la base de datos.
                db.Empresa.Add(e);
                //Guardamos la base de datos.
                db.SaveChanges();

                //Instanciamos el objeto Local mediante los datos obtenidos en el formulario.
                Local l = new Local();
                l.usuario = collection["user"];
                l.contraseña = collection["password"];
                l.direccion = collection["dir"];
                l.telefono = Int32.Parse(collection["tel"]);
                l.aforoMax = Int32.Parse(collection["afM"]);
                l.id_empresa = Int32.Parse(e.id.ToString());
                
                //Añadimos el local a la base de datos.
                db.Local.Add(l);
                //Guardamos la base de datos.
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
            //Instanciamos un objeto de tipo Empresa.
            Empresa em = new Empresa();
            //Obtenemos la id de la empresa del local seleccionado.
            int id_empresa = db.Local.Find(id).id_empresa;
            //Creamos le objeto empresa con los datos obtenidos de la BD.
            em = db.Empresa.Find(id_empresa);

            //Almacenamos los atributos de la empresa en los ViewBags para poder usarlos en las vistas.
            ViewBag.IdEmpresa = em.id.ToString();
            ViewBag.NombreEmpresa = em.nombre;
            ViewBag.CIF = em.CIF;

            //Instanciamos un objeto de tipo Local.
            Local lo = new Local();
            //Obtenemos los datos del local seleccionado mediante su id.
            lo = db.Local.Find(id);
            
            //Almacenamos los atributos del local en los ViewBags para poder usarlo en las vistas.
            ViewBag.IdLocal = lo.id.ToString();
            ViewBag.Usuario = lo.usuario;
            ViewBag.Contraseña = lo.contraseña;
            ViewBag.Direccion = lo.direccion;
            ViewBag.Telefono = lo.telefono.ToString();
            ViewBag.AforoM = lo.aforoMax.ToString();
            ViewBag.IdEmpresa = lo.id_empresa.ToString();

            
            return View("~/Views/Local/Edit.cshtml");
        }

        // POST: Local/Edit/5
        //Obtenemos los datos del formulario y hacemos el update.
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                
                //Obtenemos la id de la empresa al enviar los datos del formulario.
                int idEmpresa = Int32.Parse(collection["idEmpresa"]);

                //Cambiamos los datos del nombre y CIF de la empresa por los datos obtenidos del formulario.
                db.Empresa.Find(idEmpresa).nombre = collection["empN"];
                db.Empresa.Find(idEmpresa).CIF = collection["cif"];

                //Guardamos los cambios en la BD.
                db.SaveChanges();

                //Cambiamos los datos del usuario, contraseña, dirección, teléfono y aforo máximo del local.
                db.Local.Find(id).usuario = collection["user"];
                db.Local.Find(id).contraseña = collection["password"];
                db.Local.Find(id).direccion = collection["dir"];
                db.Local.Find(id).telefono = Int32.Parse(collection["tel"]);
                db.Local.Find(id).aforoMax = Int32.Parse(collection["afM"]);
                
                //Guardamos los cambios en la BD.
                db.SaveChanges();


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
            //Instanciamos un local mediante los datos obtenidos de la base de datos usando la id del local.
            Local localEliminado = db.Local.Find(id);

            //Eliminamos el local de la base de datos.
            db.Local.Remove(localEliminado);
            
            //Guardamos la base de datos.
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
