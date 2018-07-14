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
            List<Member> Members_list = context.Member.ToList();
            return View("index",Members_list);
        }

        [ActionName("connect2")]
        public IActionResult Connect ( int ID )
        {
            Member m = context.Member.Find(ID);
            if (m.Equals(null))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("details", m);
            }
            
        }

        //affichage du formulaire
        [HttpGet]
       // [ActionName("Create")]
        public IActionResult CreateMember()
        {
            return View("CreateMember");
        }

        //traitement du formulaire
        [HttpPost]
        public IActionResult CreateMember([ Bind(include:"FirstName, LastName, UserName, Password")] Member m)
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
