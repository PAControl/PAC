using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlAforo.Models
{
    public class Calculos
    {

        //static private PACbdEntities1 db = new PACbdEntities1();
        int entradas;
        int salidas;
        //Local l = (Local)HttpContext.Current.Session["userLocal"];

        public int CalcEntradas()
        {
            Local l = (Local)HttpContext.Current.Session["userLocal"];
           
            using (var db = new PACbdEntities1())
            {
                entradas = (from x in db.Control.ToList()
                            where x.fecha.ToString("dd/MM/yyyy :HH:mm:ss").Contains(DateTime.Now.ToString("dd/MM/yyyy"))
                            where x.entrada == true
                            where x.id_local == l.id
                            select x.id).Count();
            }

            return entradas;

        }

        public int CalcSalidas()
        {

            Local l = (Local)HttpContext.Current.Session["userLocal"];

            using (var db = new PACbdEntities1())
            {
                salidas = (from x in db.Control.ToList()
                           where x.fecha.ToString("dd/MM/yyyy :HH:mm:ss").Contains(DateTime.Now.ToString("dd/MM/yyyy"))
                           where x.entrada == false
                           where x.id_local == l.id
                           select x.id).Count();
            }

            return salidas;
        
        }



    }
}