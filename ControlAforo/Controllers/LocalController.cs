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

       // GET: Local
        public ActionResult Index()
        {
            List<Local> locales;
            using (var context = new PACbdEntities1())
            {
                
                //Consulta para obtener todos los datos de los locales.
                locales = context.Local.Where(l => l.rol == false).ToList();

            }
                

            return View(locales);
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
            using (var context = new PACbdEntities1())
            {
                try
                {

                    foreach (var item in context.Local.ToList())
                    {
                        if (collection["user"] == item.usuario)
                        {
                            ViewBag.UsuarioRep = "Este nombre de usuario ya existe";
                            return View("~/Views/Home/Login.cshtml");
                        }
                    }

                    //Instanciamos el objeto Empresa mediante los datos obtenidos en el formulario.
                    Empresa e = new Empresa();
                    e.nombre = collection["empN"];
                    e.CIF = collection["cif"];

                    //Añadimos la empresa a la base de datos.
                    context.Empresa.Add(e);
                    //Guardamos la base de datos.
                    context.SaveChanges();

                    //Instanciamos el objeto Local mediante los datos obtenidos en el formulario.
                    Local l = new Local();
                    l.usuario = collection["user"];
                    l.contraseña = collection["passwordReg"];
                    l.direccion = collection["dir"];
                    l.telefono = Int32.Parse(collection["tel"]);
                    l.aforoMax = Int32.Parse(collection["afM"]);
                    l.id_empresa = Int32.Parse(e.id.ToString());

                    //Añadimos el local a la base de datos.
                    context.Local.Add(l);
                    //Guardamos la base de datos.
                    context.SaveChanges();

                    Session.Add("userLocal",l);

                    return RedirectToAction("VistaSeguridadDosBotones","Aforo");
                }
                catch
                {
                    return View("Index");
                }
            }
        }

        // GET: Local/Edit/5
        // Nos enviara a la página de edición del local.
        public ActionResult Edit(int id)
        {
            using (var context = new PACbdEntities1())
            {

                //Instanciamos un objeto de tipo Empresa.
                Empresa em = new Empresa();
                //Obtenemos la id de la empresa del local seleccionado.
                int id_empresa = context.Local.Find(id).id_empresa;
                //Creamos le objeto empresa con los datos obtenidos de la BD.
                em = context.Empresa.Find(id_empresa);

                //Almacenamos los atributos de la empresa en los ViewBags para poder usarlos en las vistas.
                ViewBag.IdEmpresa = em.id.ToString();
                ViewBag.NombreEmpresa = em.nombre;
                ViewBag.CIF = em.CIF;

                //Instanciamos un objeto de tipo Local.
                Local lo = new Local();
                //Obtenemos los datos del local seleccionado mediante su id.
                lo = context.Local.Find(id);

                //Almacenamos los atributos del local en los ViewBags para poder usarlo en las vistas.
                ViewBag.IdLocal = lo.id.ToString();
                ViewBag.Usuario = lo.usuario;
                ViewBag.Contraseña = lo.contraseña;
                ViewBag.Direccion = lo.direccion;
                ViewBag.Telefono = lo.telefono.ToString();
                ViewBag.AforoM = lo.aforoMax.ToString();
                ViewBag.IdEmpresa = lo.id_empresa.ToString();

            }
            return View("~/Views/Local/Edit.cshtml");
        }

        // POST: Local/Edit/5
        //Obtenemos los datos del formulario y hacemos el update.
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            using (var context = new PACbdEntities1())
            {

                try
                {

                    //Obtenemos la id de la empresa al enviar los datos del formulario.
                    int idEmpresa = Int32.Parse(collection["idEmpresa"]);

                    //Cambiamos los datos del nombre y CIF de la empresa por los datos obtenidos del formulario.
                    context.Empresa.Find(idEmpresa).nombre = collection["empN"];
                    context.Empresa.Find(idEmpresa).CIF = collection["cif"];

                    //Guardamos los cambios en la BD.
                    context.SaveChanges();

                    //Cambiamos los datos del usuario, contraseña, dirección, teléfono y aforo máximo del local.
                    context.Local.Find(id).usuario = collection["user"];
                    context.Local.Find(id).contraseña = collection["password"];
                    context.Local.Find(id).direccion = collection["dir"];
                    context.Local.Find(id).telefono = Int32.Parse(collection["tel"]);
                    context.Local.Find(id).aforoMax = Int32.Parse(collection["afM"]);

                    //Guardamos los cambios en la BD.
                    context.SaveChanges();


                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Local/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new PACbdEntities1())
            {
                //Instanciamos un local mediante los datos obtenidos de la base de datos usando la id del local.
                Local localEliminado = context.Local.Find(id);

                //Eliminamos el local de la base de datos.
                context.Local.Remove(localEliminado);

                //Guardamos la base de datos.
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //Return a JSON with the data
        public JsonResult Company(int id) {

            Empresa emp = new Empresa();

            using (var context = new PACbdEntities1())
            {

                emp.id = id;
                emp.nombre = context.Empresa.Find(id).nombre.ToString();
                emp.CIF = context.Empresa.Find(id).CIF.ToString();

            }
            
            return Json(new { id = emp.id, name = emp.nombre, cif = emp.CIF }, JsonRequestBehavior.AllowGet); ;

        }

        public ActionResult Filter(FormCollection collection) {
            
            using (var context = new PACbdEntities1())
            {
                if (collection["filter"].ToString().Equals("") || collection["filter"].ToString().Equals(" ") || collection["filter"].ToString().Equals(null))
                {

                    //Consulta para obtener todos los datos de los locales.
                    List<Local> locales = context.Local.Where(l => l.rol == false).ToList();
                    return View("Index", locales);

                }

                else {

                    string dir = collection["filter"].ToString();
                    //Consulta para obtener todos los datos de los locales filtrados por su dirección.
                    List<Local> locales = context.Local.Where(l => l.rol == false)
                        .Where(l => l.direccion.Equals(dir) || l.direccion.Contains(dir)).ToList();
                    return View("Index", locales);

                }
                

            }

            
        }

        public ActionResult CloseSession()
        {

            Session.Remove("userAdmin");

            return RedirectToAction("Login", "Home");
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
