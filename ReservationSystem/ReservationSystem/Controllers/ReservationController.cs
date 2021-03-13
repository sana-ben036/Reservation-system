using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Controllers
{
    [Authorize(Roles = "Admin,Student")]
    public class ReservationController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ReservationController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }




        // GET: ReservationController
        public ActionResult Index()
        {
            var reservations = _context.Reservations.Include(r=>r.ReservationType).ToList(); // include pour afficher le nom de type d'objet type
            return View(reservations);
        }

        // GET: ReservationController/Details/5
        public ActionResult Details(int id)
        {
            var reservation = _context.Reservations.Find(id);

            return View(reservation);
        }

        // GET: ReservationController/Create
        public IActionResult Create()
        {
            var reservation = new Reservation();
            IEnumerable<ReservationType> types = _context.ReservationTypes.ToList();
            ViewBag.Types = types;
            
            return View(reservation);
        }

        // POST: ReservationController/Create
        
        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            if(ModelState.IsValid)
            {
                _context.Reservations.Add(reservation);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<ReservationType> types = _context.ReservationTypes.ToList();
            ViewBag.Types = types;
            return View(reservation);
        }

        // GET: ReservationController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id is null)
            {
                return View("../Error/NotFound", "Please add the reservation Id in URL");
            }
            var reservation = _context.Reservations.Find(id);
            if (reservation is null)
            {
                return View("../Error/NotFound", $"The reservation Id : {id} cannot be found");
            }


            return View(reservation);
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(reservation).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: ReservationController/Delete/5
        public ActionResult Delete()
        {
            return RedirectToAction("Index");
        }

        // POST: ReservationController/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation is null)
            {
                return View("../Error/NotFound", $"The reservartion Id : {id} cannot be found");
            }

            _context.Remove(reservation);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
