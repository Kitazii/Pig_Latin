using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KBurns_Task2.Models;

namespace KBurns_Task2.Controllers
{
    public class TranslateController : Controller
    {
        // GET: Translate
        [HttpGet]
        public ActionResult Translation()
        {
            return View();
        }

        // POST: Translate
        [HttpPost]
        public ActionResult Translation(Translate model)
        {
            if (ModelState.IsValid)
            {
                model.TranslationToPigLatin();
                return View("Display", model);
            }
            else
            {
                return View();
            }
        }
    }
}