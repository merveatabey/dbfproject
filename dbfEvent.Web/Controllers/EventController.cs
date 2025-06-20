using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dbfEvent.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dbfEvent.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly EventManagementDbContext _context;
        public EventController(EventManagementDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            var events = _context.Events.ToList();
            return View(events);
        }

        public IActionResult Sanat()
        {
            var sanatFests = _context.Events.Where(e => e.EventType.ToLower() == "sanat sergisi").ToList();
            return View(sanatFests);
        }
        public IActionResult Film()
        {
            var filmFests = _context.Events.Where(e => e.EventType.ToLower() == "film festivali").ToList();
            return View(filmFests);
        }
        public IActionResult Müzik()
        {
            var musicFests = _context.Events.Where(e => e.EventType.ToLower() == "müzik festivali").ToList();
            return View(musicFests);
        }
        public IActionResult Kitap()
        {
            var kitapFests = _context.Events.Where(e => e.EventType.ToLower() == "kitap fuarı").ToList();
            return View(kitapFests);
        }
        public IActionResult Teknoloji()
        {
            var teknoFest = _context.Events.Where(e => e.EventType.ToLower() == "teknoloji zirvesi").ToList();
            return View(teknoFest);
        }

    }   
}

