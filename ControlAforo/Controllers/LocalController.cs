using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlAforo.Controllers
{
    public class LocalController : Controller
    {
        // GET: Local
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GestionLocales()
        {
            return View();
        }

        // GET: Local/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
            return View();
        }

        // POST: Local/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
