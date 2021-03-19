﻿using Microsoft.AspNetCore.Authorization;
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
       public int NA=0;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> userManager;
        public ReservationController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }


        

        // GET: ReservationController
        public async Task<ActionResult> Index()
        {


            if (User.IsInRole("Student"))
            {
                AppUser user = await userManager.FindByEmailAsync(User.Identity.Name);
                var reservations = _context.Reservations.Include(r => r.Type).ToList().Where(r => r.UserId == user.Id).OrderBy(r=>r.Status); // include pour afficher le nom de type d'objet type
                return View(reservations);
            }
            else
            {
                var reservations = _context.Reservations.Include(r => r.Type).Include(r => r.User).ToList().OrderBy(r => r.Status);
                return View(reservations);
            }

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
            ViewBag.UserId = userManager.GetUserId(HttpContext.User);  // recuperer id de user en connection pour remplir la valeur de UserId comme proprietaire de cette reservation crée
            IEnumerable<ReservationType> types = _context.ReservationTypes.ToList(); // pour charger les types et les afficher sur input select
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

            ViewBag.UserId = userManager.GetUserId(HttpContext.User);
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
            ViewBag.UserId = userManager.GetUserId(HttpContext.User);
            IEnumerable<ReservationType> types = _context.ReservationTypes.ToList();
            ViewBag.Types = types;
            return View(reservation);
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
         public async Task <ActionResult> Edit(int id, Reservation reservation)
        {
            
            var idUser = reservation.UserId;
            AppUser user = await userManager.FindByIdAsync(idUser);
            var idType = reservation.TypeId;
            var type = _context.ReservationTypes.Find(idType);
            

            if (ModelState.IsValid)
            {

                _context.Entry(reservation).State = EntityState.Modified;


                if (reservation.Status == Status.Accepted)
                {

                    user.NRA++;
                    if (type.TotalR< type.NumberA)
                    {
                        type.TotalR++;
                        _context.SaveChanges();
                    }
                    else
                    {
                        return View("../Error/NotFound", "The Type is FULL");
                    }
                    _context.SaveChanges();

                   
                    

                    
                }
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            //if (reservation.Status == Status.Accepted)
            //{

            //    if (ModelState.IsValid)
            //    {

            //        _context.Entry(reservation).State = EntityState.Modified;


            //        if (reservation.Status != Status.Accepted)
            //        {

            //            user.NRA--;


            //            _context.SaveChanges();
            //        }

            //        _context.SaveChanges();
            //        return RedirectToAction(nameof(Index));
            //    }

            //}
            //else
            //{
            //    if (ModelState.IsValid)
            //    {

            //        _context.Entry(reservation).State = EntityState.Modified;


            //        if (reservation.Status == Status.Accepted)
            //        {

            //            user.NRA++;


            //            _context.SaveChanges();
            //        }

            //        _context.SaveChanges();
            //        return RedirectToAction(nameof(Index));
            //    }


            //}
            ViewBag.UserId = userManager.GetUserId(HttpContext.User);
            IEnumerable<ReservationType> types = _context.ReservationTypes.ToList();
            ViewBag.Types = types;
            return View(reservation);

        }

        // GET: ReservationController/Delete/5
        public ActionResult Delete()
        {
            return RedirectToAction("Index");
        }

        // POST: ReservationController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation is null)
            {
                return View("../Error/NotFound", $"The reservartion Id : {id} cannot be found");
            }

            var idUser = reservation.UserId;
            AppUser user = await userManager.FindByIdAsync(idUser);
            if (reservation.Status == Status.Accepted)
            {

                user.NRA--;

                _context.SaveChanges();
            }
            _context.Remove(reservation);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        
    }
}
