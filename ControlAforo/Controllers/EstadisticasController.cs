using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControlAforo.Models;

namespace ControlAforo.Controllers
{
    public class EstadisticasController : Controller
    {
        // GET: Estaditicas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Estadisticas()
        {
            
            DateTime fecha = DateTime.Today;

            ViewBag.estadisticasFecha = fecha.ToString("dd/MM/yyyy");

            string prubDesde;
            string prubHasta;

            List<int> numPersonas = new List<int>();

            using (var db = new PACbdEntities1())
            {
                int y = 6;
                Local lo = (Local)Session["userLocal"];

                for (int i = 6; i < 23; i++)
                {
                    prubDesde = fecha.ToString("dd/MM/yyyy") + " " + y + ":00:00";
                    y++;
                    prubHasta = fecha.ToString("dd/MM/yyyy") + " " + y + ":00:00";

                    var details = from x in db.Control.ToList()
                                  where x.fecha > DateTime.Parse(prubDesde)
                                  where x.fecha < DateTime.Parse(prubHasta)
                                  where x.entrada == true
                                  where x.id_local == lo.id
                                  select x.id;

                    numPersonas.Add(details.Count());
                    
                }

                ViewBag.arrayDeCounts = numPersonas;

            }

            return View("Estadisticas");
        }

        public ActionResult CambiarFecha(FormCollection collection)
        {
            DateTime fecha = DateTime.Parse(collection["fechaUser"]);
           
            ViewBag.estadisticasFecha = fecha.ToString("dd/MM/yyyy");

            string prubDesde;
            string prubHasta;

            List<int> numPersonas = new List<int>();

            using (var db = new PACbdEntities1())
            {

                Local lo = (Local)Session["userLocal"];

                for (int i = 6; i < 23; i++)
                {
                    prubDesde = fecha.ToString("dd/MM/yyyy") + " " + i + ":00:00";

                    prubHasta = fecha.ToString("dd/MM/yyyy") + " " + (i + 1) + ":00:00";

                    var details = from x in db.Control.ToList()
                                  where x.fecha > DateTime.Parse(prubDesde)
                                  where x.fecha < DateTime.Parse(prubHasta)
                                  where x.entrada == true
                                  where x.id_local == lo.id
                                  select x.id;

                    numPersonas.Add(details.Count());

                }

                ViewBag.arrayDeCounts = numPersonas;
            }
            return View("Estadisticas");
        }


    }
}