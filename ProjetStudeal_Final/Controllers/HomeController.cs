﻿using System;
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
            return View("Index",Members_list);
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
                ViewBag.login = m.Id;
                return View("Details", m);
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

        [HttpGet]
        public IActionResult MemberEdit(int Id)
        {
            Member m = context.Member.Find(Id);
            return View(m);
        }

        [HttpPost]
        public IActionResult MemberEdit(int Id, Member m)
        {
            context.Add(m);
            return View(m);
        }


        public IActionResult Tutoring()
        {
            List<Tutoring> tuto = context.Tutoring.ToList();
            return View(tuto);
        }

        [HttpGet]
        public IActionResult Inscription(int Id)
        {
            MeetingRequest mr1 = context.MeetingRequest.Find(Id);
            return View(mr1);
        }

        [HttpPost]
        public IActionResult Inscription(int Id, [Bind(include: "State, StudentId, TimeSlotId")] MeetingRequest mr1)
        {
            if (ModelState.IsValid)
            {
                context.Add(mr1);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


    }

}
