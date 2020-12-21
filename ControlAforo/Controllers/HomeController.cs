using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControlAforo.Models;


namespace ControlAforo.Controllers
{
    public class HomeController : Controller
    {
        //Redirección a la vista principal del HomeController
        public ActionResult Index()
        {
            return View();
        }

        // Method to Verify the User/Password Login
        public ActionResult Login(string user, string password)
        {

            if (!string.IsNullOrEmpty(user) || !string.IsNullOrEmpty(password))
            {
                var context = new PACbdEntities1();
                var localUser = context.Local.FirstOrDefault(e => e.usuario == user && e.contraseña == password);

                if (localUser != null && localUser.rol == false)
                {
                    Session.Add("userLocal", localUser);
                    //return View("~/Views/Aforo/VistaSeguridadDosBotones.cshtml");
                    return RedirectToAction("VistaSeguridadDosBotones", "Aforo");
                }
                else if (localUser != null && localUser.rol == true)
                {
                    Session.Add("userAdmin", localUser);
                    return RedirectToAction("Index", "Local");
                }
                else if (localUser == null)
                {
                    ViewBag.Login = "User or password incorrect, please try again.";
                    return View("Login");
                }

            }

            ViewBag.Login = null;
            return View("Login");
        }

        //Redirección a la vista About
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //Redirección a la vista Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Redirección a la vista Configuracion
        public ActionResult Configuracion()
        {          
            return View("Configuracion");
        }

        public ActionResult ChangeCapacity(FormCollection collection) {

            var context = new PACbdEntities1();
            //var localUser = context.Local.Find();

            Local user = (Local)Session["userLocal"];
            user.aforoMax = Int32.Parse(collection["capacity"]);
            //Actualizar el objeto de la session para tener el nuevo aforo máximo.
            ViewBag.Capacity = collection["capacity"];
            Session.Add("userLocal",user);
            context.Local.Find(user.id).aforoMax = Int32.Parse(collection["capacity"]);
            
            //Guardamos los cambios en la BD.
            context.SaveChanges();

            return View("Configuracion");

        }

        //Pantalla de Identificación
        public ActionResult Identificacion()
        {
            return View("Identificacion");
        }

        public ActionResult Identification(FormCollection collection)
        {
            var context = new PACbdEntities1();
            Local user = (Local)Session["userLocal"];

            int capacity = user.aforoMax;

            if (collection["password"] == user.contraseña )
            {

                ViewBag.Capacity = capacity;
                return View("~/Views/Home/Configuracion.cshtml");

            }

            return RedirectToAction(Session["method"].ToString(), Session["controller"].ToString());


        }

        public ActionResult CloseSession() {

            //Eliminar la sesión
            Session.Remove("userLocal");

            return RedirectToAction("Login", "Home");
        }


    }
}