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
        public IActionResult BiletAl(string eventType = null, string eventName = null)
        {

            var allEvents = _context.Events.ToList();

            var vm = new TicketPurchaseVM
            {
                EventType = eventType,
                EventId = !string.IsNullOrEmpty(eventName) ? allEvents.FirstOrDefault(e => e.EventName == eventName)?.EventId ?? 0 : 0,


                EventTypes = allEvents.Select(e => e.EventType).Distinct().Select(type =>
                new SelectListItem
                {
                    Text = type,
                    Value = type
                }
                ).ToList(),



                Events = allEvents.Select(e => new SelectListItem
                {
                    Text = e.EventName,
                    Value = e.EventId.ToString()
                }).ToList(),


                TicketTypes = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Öğrenci", Text = "Öğrenci" },
                    new SelectListItem { Value = "Yetişkin", Text = "Yetişkin" },
                    new SelectListItem { Value = "Vip", Text = "Vip" }
                }
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult BiletAl(TicketPurchaseVM model)
        {

            if (ModelState.IsValid)
            {

                //Dropdownları tekrar doldur

                var allEvents = _context.Events.ToList();

                model.EventTypes = allEvents.Select(e => e.EventType).Distinct().Select(type => new SelectListItem { Text = type, Value = type, Selected = (type == model.EventType) }).ToList();


                //eventtype seçildiyse filtrele
                if (!string.IsNullOrEmpty(model.EventType))
                {
                    model.Events = allEvents.Where(e => e.EventType == model.EventType)
                                            .Select(e => new SelectListItem
                                             {
                                                     Text = e.EventName,
                                                     Value = e.EventId.ToString()
                                             }).ToList();
                }
                else
                {
                    model.Events = new List<SelectListItem>();
                }
              

                model.TicketTypes = new List<SelectListItem>
                {
                    new SelectListItem{Text = "Öğrenci", Value = "Öğrenci"},
                    new SelectListItem{Text = "Yetişkin", Value = "Yetişkin"},
                    new SelectListItem{Text = "Vip", Value = "Vip"}
                };

                return View(model);
            }

            //aktif bilet var mı?
            var ticket = _context.Tickets.FirstOrDefault(t => t.EventId == model.EventId && t.Status.ToLower() == "Aktif");
            if (ticket == null)
            {
                ModelState.AddModelError("", "Biletler Tükenmiştir, İlginiz İçin Teşekkür Ederiz.");
                return View(model);
            }



            //katılmcı kayıt
            var participants = new Participant
            {

                ParticipantName = model.ParticipantName,
                Age = model.Age,
                Email = model.Email,
                Phone = model.Phone,
                EventId = model.EventId,
                Status = "Onaylandı"
            };

            _context.Participants.Add(participants);
            _context.SaveChanges(); // Burada ParticipantId oluşur

            ticket.Status = "Satıldı";
            ticket.ParticipantId = participants.ParticipantId;
            _context.Tickets.Update(ticket);
            _context.SaveChanges();


            return RedirectToAction("BiletOnay");



        }

        public IActionResult BiletOnay()
        {
            return View();
        }
    }
}


