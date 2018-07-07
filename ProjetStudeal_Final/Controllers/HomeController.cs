using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetStudeal_Final.Models;

namespace ProjetStudeal_Final.Controllers
{
    public class HomeController : Controller
    {
        DBStudealContext context = new DBStudealContext();
        
        public HomeController(DBStudealContext _context)
        {
            this.context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Connect ( Member ID )
        {
            var req = from m in context.Member
                      where m.Id.Equals(ID)
                      select m;

            if( req.Count()>0 )
            {
                ViewBag.login = req;
                return View("Details");
            }

            return RedirectToAction("Index");
        }

        //affichage du formulaire
        [HttpGet]
        [ActionName("Create")]
        public IActionResult CreateMember()
        {
            return View();
        }

        //traitement du formulaire
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateMember([Bind(include: "LastName, FirstName, CreationDate")]Member m)
        {
            if (ModelState.IsValid)
            {
                context.Add(m);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

    }
}
