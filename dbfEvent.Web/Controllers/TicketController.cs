using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dbfEvent.Data.Models;
using dbfEvent.Web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dbfEvent.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly EventManagementDbContext _context;
        public TicketController(EventManagementDbContext context)
        {
            _context = context;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult BiletAl(int? eventId = null)
        {
            //formda EventId gizli alan olacağından ViewModel'e sadece bunu atıyoruz.
            var vm = new TicketPurchaseVM();

            vm.Events = _context.Events.Select(e => new SelectListItem
            {
                Value = e.EventId.ToString(),
                Text = e.EventName
            }).ToList();


            //eventId parametresi varsa seçili yap
            if (eventId.HasValue)
            {
                vm.EventId = eventId.Value;
            }

            //sabit bilet tiplerini ekle
            vm.TicketTypes = new List<SelectListItem>
            {
                new SelectListItem{Value = "Öğrenci" , Text = "Öğrenci"},
                new SelectListItem{Value = "Yetişkin", Text = "Yetişkin"},
                new SelectListItem{Value = "Vip", Text = "Vip"}
            };


            return View(vm);
        }

        [HttpPost]
        public async Task <IActionResult> BiletAl(TicketPurchaseVM model)
        {
            if (ModelState.IsValid)
            {
                model.Events = _context.Events.Select(e => new SelectListItem
                {
                    Value = e.EventId.ToString(),
                    Text = e.EventName
                }).ToList();

                model.TicketTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Öğrenci", Text = "Öğrenci" },
            new SelectListItem { Value = "Yetişkin", Text = "Yetişkin" },
            new SelectListItem { Value = "Kamu Personel", Text = "Kamu Personel" }
        };

                return View(model);
            }


            model.Events = _context.Events
                       .Select(e => new SelectListItem
                       {
                           Value = e.EventId.ToString(),
                           Text = e.EventName
                       })
                       .ToList();

            //katılımcı oluştur
            var participants = new Participant
            {
                ParticipantName = model.ParticipantName,
                Age = model.Age,
                Email = model.Email,
                Phone = model.Phone,
                Status = "Aktif",
                EventId = model.EventId

            };

            _context.Participants.Add(participants);
            _context.SaveChanges();


            // Boş ve seçilen event, bilet tipi ve aktif olan bileti al (dilersen bilet tipine göre filtre ekleyebilirsin)

            var ticket = _context.Tickets.Where(t => t.EventId == model.EventId && t.ParticipantId == null && t.Status == "Aktif").FirstOrDefault();

            if(ticket == null)
            {
                ModelState.AddModelError("","Bilet Kalmamıştır");


                //dropdown'ları tekrar doldur
                model.Events = _context.Events.Select(e => new SelectListItem
                {
                    Value = e.EventId.ToString(),
                    Text = e.EventName
                }).ToList();

                model.TicketTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Öğrenci", Text = "Öğrenci" },
            new SelectListItem { Value = "Yetişkin", Text = "Yetişkin" },
            new SelectListItem { Value = "Kamu Personel", Text = "Kamu Personel" }
        };

                return View(model);
            }
       

            //boş bilet varsa bilet deki katılımcı id ye katılmcıdaki id yi ata.
            ticket.ParticipantId = participants.ParticipantId;
            ticket.Status = "Rezerve";
            await _context.SaveChangesAsync();

            return RedirectToAction("Bilet Onay", new { ticketId = ticket.TicketId });
        }

        public IActionResult BiletOnay(int ticketId)
        {
            var ticket = _context.Tickets
                .Include(t => t.Participant)
                .Include(t => t.Event)
                .FirstOrDefault(t => t.TicketId == ticketId);


            if (ticketId == null)
                return NotFound();

            return View(ticket);
        }
      
    }
}

