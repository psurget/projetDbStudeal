using System.Collections.Generic;
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
                return View("MemberView", m);
            }           
        }
        /* ======== FIN CONNECT ======== */

        /* ======== CREATE NEW MEMBER ======== */
        [HttpGet]
       // [ActionName("Create")]
        public ActionResult CreateMember()
        {
            return View("MemberForm");
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
        public ActionResult MemberEdit(int id)
        {
            int isTutor = context.Member.Find(id).IsTutor;
            string body = "";
            Member m = context.Member.Find(id);
            List<Tutoring> tuto = context.Tutoring.Where(t => t.TutorId.Equals(id)).ToList();
            if (isTutor.Equals(1))
                {
                    body = "<button asp-action='CreateTuto'>+ Ajouter un tutorat</button>";
                    body += "<h3> Tutorats </h3>";

                    
                    if (tuto.Count > 0)
                    {
                        body += "<table border=1>";
                        foreach (var t in tuto)
                        {
                            body += "<tr>";
                            body += "<td><b>" + t.Subject + "</b></td>";
                            body += "<td>" + t.TimeSlot + "</td>";
                            body += "</tr>";
                        }
                        body += "</table>";
                    }
                }
            return View("MemberForm", m);
        }
        [HttpPost]
        public ActionResult MemberEdit(int Id, Member m)
        {
            context.Update(m);
            return View("MemberView",m);
        }
        /* ======== FIN MEMBER EDIT ======== */

        /* ============== TUTORING METHODS =================*/
        public ActionResult Tutoring()
        {
            List<Tutoring> tuto = context.Tutoring.ToList();
            return View(tuto);
        }

        /* === Creer un tutorat a partir du membre ===*/
        [HttpGet]
        public ActionResult CreateTuto(int tutorId)
        {
            Member m = context.Member.Find(tutorId);
            Tutoring t = new Tutoring();
            t.TutorId = tutorId;
            return View(t);
        } 

        [HttpPost]
        public ActionResult CreateTuto(int Id,[Bind(include:"Subject,TutorId")] Tutoring t)
        {
            if (ModelState.IsValid)
            {
                context.Add(t);
                return View(t);
            }
            return View();
        }

        /* ======== INSCRIPTIONS ======== */
        /* formulaire inscription*/
        [HttpGet]
        public ActionResult Inscription(int Id)
        {
            MeetingRequest mr = context.MeetingRequest.Find(Id);
            return View(mr);
        }
        /* envoie inscription*/
        [HttpPost]
        public ActionResult Inscription(int Id,[Bind(include:"State,StudentId,TimeSlotId")] MeetingRequest mr)
        {
            if (ModelState.IsValid)
            {
                context.Add(mr);
                context.SaveChanges();
                return RedirectToAction("MemberView", mr);
            }
            return View();
        }
        /* ======== FIN INSCRIPTIONS ======== */
    }

}
