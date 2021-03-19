using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReservationTypeController : Controller
    {
        private readonly AppDbContext _context;
        public ReservationTypeController(AppDbContext context)
        {
            _context = context;
        }




        // GET: ReservationTypeController
        public ActionResult Index()
        {
            var types = _context.ReservationTypes.ToList();
            return View(types);
        }

        // GET: ReservationTypeController/Details/5
        public ActionResult Details(int id)
        {
            var type = _context.ReservationTypes.Find(id);

            return View(type);
        }

        // GET: ReservationTypeController/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: ReservationTypeController/Create
        [HttpPost]
        public ActionResult Create(ReservationType model)
        {
            if(ModelState.IsValid )
            {
                _context.ReservationTypes.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
            
        }

        // GET: ReservationTypeController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id is null)
            {
                return View("../Error/NotFound", "Please add the type Id in URL");
            }
            var type = _context.ReservationTypes.Find(id);
            if (type is null)
            {
                return View("../Error/NotFound", $"The type Id : {id} cannot be found");
            }

            
            return View(type);
        }

        // POST: ReservationTypeController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ReservationType type)
        {
            if(ModelState.IsValid)
            {
                
                _context.Entry(type).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
                
            }
            return View(type);
        }

        // GET: ReservationTypeController/Delete/5
        public ActionResult Delete()
        {
            return RedirectToAction("Index");
        }

        // POST: ReservationTypeController/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var type = _context.ReservationTypes.Find(id);
            if (type is null)
            {
                return View("../Error/NotFound", $"The type Id : {id} cannot be found");
            }

            _context.Remove(type);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
