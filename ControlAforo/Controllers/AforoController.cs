using ControlAforo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlAforo.Controllers
{
    public class AforoController : Controller
    {

        //Obtenemos el contexto de la base de datos.
        static private PACbdEntities1 db = new PACbdEntities1();

        //Calculamos las entradas y salidas al iniciar la aplicación.
        //static int entradas = db.Control.Where(c => c.entrada == true).Where(c => c.fecha.Equals DateTime.Now).Count();

        static Calculos calc = new Calculos();

        static int entradas = calc.CalcEntradas();

        static int salidas = calc.CalcSalidas();

        //Guardamos el calculo de la entrada y salida.
        static int aforo = entradas - salidas;
        //private int entradas = db.Control.ToList();


        // GET: Local
        public ActionResult VistaSeguridadDosBotones()
        {
            Session.Add("controller", "Aforo");
            Session.Add("method", "VistaSeguridadDosBotones");
            ActualizarVariables();

            Local idL = (Local)Session["userLocal"];
            ViewBag.aforo = aforo;
            ViewBag.aforoM = idL.aforoMax;

            using (var db = new PACbdEntities1())
            {
                Empresa emp = db.Empresa.Find(idL.id_empresa);
                ViewBag.emp = emp.nombre;
            }

            

            return View("VistaSeguridadDosBotones");
        }

        

        public ActionResult VistaUsuarioDosBotones()
        {

            Session.Add("controller", "Aforo");
            Session.Add("method", "VistaUsuarioDosBotones");
            ActualizarVariables();

            Local idL = (Local)Session["userLocal"];
            ViewBag.aforo = aforo;
            ViewBag.aforoM = idL.aforoMax;

            using (var db = new PACbdEntities1())
            {
                Empresa emp = db.Empresa.Find(idL.id_empresa);
                ViewBag.emp = emp.nombre;
            }

            return View("VistaUsuarioDosBotones");
        }

        //Suma 1
        public ActionResult sumar()
        {

            Local idL = (Local)Session["userLocal"];

            Control control = new Control
            {
                fecha = DateTime.Now,
                entrada = true,
                id_local = idL.id
            };

            using (var db = new PACbdEntities1())
            {
                if(aforo < idL.aforoMax)
                {
                    db.Control.Add(control);
                    db.SaveChanges();
                }
            }

            if (aforo < idL.aforoMax){
                aforo++;
                ViewBag.aforo = aforo;
            }

            return RedirectToAction("VistaUsuarioDosBotones");
        }

        //Resta 1
        public ActionResult restar()
        {
            Local idL = (Local)Session["userLocal"];
            
            Control control = new Control
            {
                fecha = DateTime.Now,
                entrada = false,
                id_local = idL.id
            };

            using (var db = new PACbdEntities1())
            {

                if (aforo > 0)
                {
                    db.Control.Add(control);
                    db.SaveChanges();
                }
            }
            if (aforo > 0)
            {
                aforo--;
                ViewBag.aforo = aforo;
            }
            return RedirectToAction("VistaUsuarioDosBotones");
        }


        //Suma la cantidad especificada que ha indicado 
        [HttpPost]
        public ActionResult SumaMuchos(FormCollection collection)
        {
            
            try
            {
                
                int cantidad = int.Parse(collection["nums"]);

                using (var db = new PACbdEntities1())
                {

                    Local idL = (Local)Session["userLocal"];

                    for (int i = 0; i < cantidad; i++)
                    {
                        Control control = new Control
                        {
                            fecha = DateTime.Now,
                            entrada = true,
                            id_local = idL.id
                        };

                        if (aforo >= 0 && aforo < idL.aforoMax && (aforo + cantidad) <= idL.aforoMax)
                        {

                            db.Control.Add(control);
                            db.SaveChanges();

                        }

                        
                    }

                    if (aforo >= 0 && aforo < idL.aforoMax && (aforo + cantidad) <= idL.aforoMax)
                    {

                        aforo += cantidad;
                        ViewBag.aforo = aforo;

                    }

                    return RedirectToAction("VistaSeguridadDosBotones");

                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult RestarMuchos(FormCollection collection)
        {
            try
            {
                int cantidad = int.Parse(collection["numsresta"]);

                using (var db = new PACbdEntities1())
                {

                    Local idL = (Local)Session["userLocal"];

                    for (int i = 0; i < cantidad; i++)
                    {

                        Control control = new Control
                        {
                            fecha = DateTime.Now,
                            entrada = false,
                            id_local = idL.id
                        };

                        if (aforo > 0 && cantidad <= aforo)
                        {
                            db.Control.Add(control);
                            db.SaveChanges();

                        }

                    }

                    if (aforo > 0 && cantidad <= aforo)
                    {
                        aforo -= cantidad;
                        ViewBag.aforo = aforo;
                    }
                    return RedirectToAction("VistaSeguridadDosBotones");

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Local
        public ActionResult VistaUsuarioUnBotonSalida()
        {

            Session.Add("controller", "Aforo");
            Session.Add("method", "VistaUsuarioUnBotonSalida");
            ActualizarVariables();

            Local idL = (Local)Session["userLocal"];
            ViewBag.aforo = aforo;
            ViewBag.aforoM = idL.aforoMax;

            using (var db = new PACbdEntities1())
            {
                Empresa emp = db.Empresa.Find(idL.id_empresa);
                ViewBag.emp = emp.nombre;
            }

            return View("VistaUsuarioUnBotonSalida");
        }

        // GET: Local
        public ActionResult VistaUsuarioUnBotonEntrada()
        {

            Session.Add("controller", "Aforo");
            Session.Add("method", "VistaUsuarioUnBotonEntrada");
            ActualizarVariables();

            Local idL = (Local)Session["userLocal"];
            ViewBag.aforo = aforo;
            ViewBag.aforoM = idL.aforoMax;

            using (var db = new PACbdEntities1())
            {
                Empresa emp = db.Empresa.Find(idL.id_empresa);
                ViewBag.emp = emp.nombre;
            }

            return View("VistaUsuarioUnBotonEntrada");
        }

        public ActionResult VistaSeguridadUnBotonSalida()
        {

            Session.Add("controller", "Aforo");
            Session.Add("method", "VistaSeguridadUnBotonSalida");
            ActualizarVariables();

            Local idL = (Local)Session["userLocal"];
            ViewBag.aforo = aforo;
            ViewBag.aforoM = idL.aforoMax;

            if (ViewBag.aforo >= idL.aforoMax)
            {

                ViewBag.aforoM = "Aforo completo";

            }
            else {

                ViewBag.aforoCompleto = "";


            }

            using (var db = new PACbdEntities1())
            {
                Empresa emp = db.Empresa.Find(idL.id_empresa);
                ViewBag.emp = emp.nombre;
            }

            return View("VistaSeguridadUnBotonSalida");
        }

        public ActionResult VistaSeguridadUnBotonEntrada()
        {

            Session.Add("controller", "Aforo");
            Session.Add("method", "VistaSeguridadUnBotonEntrada");
            ActualizarVariables();

            Local idL = (Local)Session["userLocal"];
            ViewBag.aforo = aforo;
            ViewBag.aforoM = idL.aforoMax;

            using (var db = new PACbdEntities1())
            {
                Empresa emp = db.Empresa.Find(idL.id_empresa);
                ViewBag.emp = emp.nombre;
            }

            return View("VistaSeguridadUnBotonEntrada");
        }

        //Suma 1 Boton
        public ActionResult sumarUnBoton()
        {

            Local idL = (Local)Session["userLocal"];

            Control control = new Control
            {
                fecha = DateTime.Now,
                entrada = true,
                id_local = idL.id
            };

            using (var db = new PACbdEntities1())
            {

                if (aforo >= 0 && aforo < idL.aforoMax) { 
                
                    db.Control.Add(control);
                    db.SaveChanges();
                
                }

            }

            if (aforo >= 0 && aforo < idL.aforoMax)
            {

                aforo++;
                ViewBag.aforo = aforo;

            }

            return RedirectToAction("VistaUsuarioUnBotonEntrada");
        }


        public ActionResult restarUnBoton()
        {

            Local idL = (Local)Session["userLocal"];

            Control control = new Control
            {
                fecha = DateTime.Now,
                entrada = false,
                id_local = idL.id
            };

            using (var db = new PACbdEntities1())
            {
                if (aforo > 0)
                {
                    db.Control.Add(control);
                    db.SaveChanges();

                }

            }

            if (aforo > 0)
            {
                aforo--;
                ViewBag.aforo = aforo;
            }
            return RedirectToAction("VistaUsuarioUnBotonSalida");
        }

        public ActionResult RestarMuchosUnBoton(FormCollection collection)
        {
            try
            {
                int cantidad = int.Parse(collection["numsresta"]);

                using (var db = new PACbdEntities1())
                {

                    Local idL = (Local)Session["userLocal"];

                    for (int i = 0; i < cantidad; i++)
                    {
                        Control control = new Control
                        {
                            fecha = DateTime.Now,
                            entrada = false,
                            id_local = idL.id
                        };
                        if (aforo > 0 && cantidad <= aforo)
                        {
                            db.Control.Add(control);
                            db.SaveChanges();
                        }
                    }

                    if (aforo > 0 && cantidad <= aforo)
                    {
                        aforo -= cantidad;
                        ViewBag.aforo = aforo;
                    }

                }

                return RedirectToAction("VistaSeguridadUnBotonSalida");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult SumaMuchosUnBoton(FormCollection collection)
        {

            try
            {

                int cantidad = int.Parse(collection["nums"]);

                using (var db = new PACbdEntities1())
                {

                    Local idL = (Local)Session["userLocal"];

                    for (int i = 0; i < cantidad; i++)
                    {
                        Control control = new Control
                        {
                            fecha = DateTime.Now,
                            entrada = true,
                            id_local = idL.id
                        };

                        if (aforo >= 0 && aforo < idL.aforoMax && (aforo + cantidad) <= idL.aforoMax) { 
                        
                        db.Control.Add(control);
                        db.SaveChanges();
                        
                        }

                        
                    }
                    if (aforo >= 0 && aforo < idL.aforoMax && (aforo + cantidad) <= idL.aforoMax)
                    {
                    
                        aforo += cantidad;
                        ViewBag.aforo = aforo;
                    
                    }
                    return RedirectToAction("VistaSeguridadUnBotonEntrada");

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        
        private void ActualizarVariables()
        {
            entradas = calc.CalcEntradas();
            salidas = calc.CalcSalidas();
            aforo = entradas - salidas;
        }


    }
}
