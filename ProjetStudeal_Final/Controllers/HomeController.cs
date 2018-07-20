﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjetStudeal_Final.Models;

namespace ProjetStudeal_Final.Controllers
{
    public class HomeController:Controller
    {
        DBStudealContext context = new DBStudealContext();
        
        public HomeController(DBStudealContext _context)
        {
            this.context = _context;
        }

        public ActionResult Index()
        {
            List<Member> Members_list = context.Member.ToList();
            return View("Index",Members_list);
        }

        /* ============== MEMBER METHODS =================*/

        /* ======== CONNECT ======== */
        [ActionName("connect2")]
        public ActionResult Connect ( int ID )
        {
            Member m = context.Member.Find(ID);

            if (m.Equals(null))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.login = m.Id;
                return View("Details", m);
            }           
        }
        /* ======== FIN CONNECT ======== */

        /* ======== CREATE MEMBER ======== */
        [HttpGet]
       // [ActionName("Create")]
        public ActionResult CreateMember()
        {
            return View("CreateMember");
        }
        [HttpPost]
        public ActionResult CreateMember([ Bind(include:"FirstName, LastName, UserName, Password")] Member m)
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
        /* ======== FIN CREATE MEMBER ======== */

        /* ======== MEMBER EDIT ======== */
        [HttpGet]
        public ActionResult MemberEdit(int Id)
        {
            Member m = context.Member.Find(Id);
            return View(m);
        }
        [HttpPost]
        public ActionResult MemberEdit(int Id, Member m)
        {
            context.Add(m);
            return View(m);
        }
        /* ======== FIN MEMBER EDIT ======== */

        /* ============== TUTORING METHODS =================*/
        public ActionResult Tutoring()
        {
            List<Tutoring> tuto = context.Tutoring.ToList();
            return View(tuto);
        }

        [HttpGet]
        public ActionResult CreateTuto()
        {
            Tutoring t = new Tutoring();
            return View(t);
        }         

        /* ======== INSCRIPTIONS ======== */
        [HttpGet]
        public ActionResult Inscription(int Id)
        {
            MeetingRequest mr = context.MeetingRequest.Find(Id);
            return View(mr);
        }
        [HttpPost]
        public ActionResult Inscription(int Id, [Bind(include: "State, StudentId, TimeSlotId")] MeetingRequest mr)
        {
            if (ModelState.IsValid)
            {
                context.Add(mr);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        /* ======== FIN INSCRIPTIONS ======== */
    }

}
