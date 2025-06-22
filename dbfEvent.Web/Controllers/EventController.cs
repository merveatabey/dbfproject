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
            var sanat = _context.Events.Where(e => e.EventType.ToLower() == "sanat").ToList();
            return View(sanat);
        }
        public IActionResult Film()
        {
            var film = _context.Events.Where(e => e.EventType.ToLower() == "film").ToList();
            return View(film);
        }
        public IActionResult Müzik()
        {
            var music = _context.Events.Where(e => e.EventType.ToLower() == "Müzik").ToList();
            return View(music);
        }
        public IActionResult Kitap()
        {
            var kitap = _context.Events.Where(e => e.EventType.ToLower() == "kitap").ToList();
            return View(kitap);
        }
        public IActionResult Teknoloji()
        {
            var teknoloji = _context.Events.Where(e => e.EventType.ToLower() == "teknoloji").ToList();
            return View(teknoloji);
        }
        public IActionResult Diger()
        {
            var diger = _context.Events.Where(d => d.EventType.ToLower() == "Diğer").ToList();
            return View(diger);
        }

    }   
}

