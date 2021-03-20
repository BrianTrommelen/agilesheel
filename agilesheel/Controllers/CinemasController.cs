using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Controllers
{
    public class CinemasController : Controller
    {
        // GET: CinemasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CinemasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CinemasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CinemasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CinemasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CinemasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CinemasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CinemasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
