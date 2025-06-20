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
    public class EmployeeController : Controller
    {

        private readonly EventManagementDbContext _context;
        public EmployeeController(EventManagementDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _context.Departmens.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();

            var events = _context.Events.Select(e => new SelectListItem
            {
                Value = e.EventId.ToString(),
                Text = e.EventName
            }).ToList();

            var vm = new EmployeeCreateVM
            {
                Departments = departments,
                Events = events
            };
            return View(vm);
            
           
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.DepartmentId == null)
                {
                    ModelState.AddModelError("DepartmentId", "Lütfen bir departman seçiniz.");
                }
                if (vm.EventId == null)
                {
                    ModelState.AddModelError("EventId", "Lütfen bir etkinlik seçiniz.");
                }

                var selectedEvent = _context.Events.FirstOrDefault(e => e.EventId == vm.EventId);
                if(vm.WorkTime < selectedEvent.StartDate || vm.WorkTime > selectedEvent.FinishDate)
                {
                    ModelState.AddModelError("WorkTime", $"Çalışma tarihi etkinlik tarihleri arasında olmalıdır : {selectedEvent.StartDate}-{selectedEvent.FinishDate}");
                }


                // Eğer ModelState bu noktada hatalıysa dropdownları tekrar doldur ve View'e dön
                if (!ModelState.IsValid)
                {
                    vm.Departments = _context.Departmens
                        .Select(d => new SelectListItem
                        {
                            Value = d.DepartmentId.ToString(),
                            Text = d.DepartmentName
                        })
                        .ToList();

                    vm.Events = _context.Events
                        .Select(e => new SelectListItem
                        {
                            Value = e.EventId.ToString(),
                            Text = e.EventName
                        })
                        .ToList();

                    return View(vm);
                }

                var employee = new Employee
                {
                    EmployeeName = vm.EmployeeName,
                    Address = vm.Address,
                    WorkTime = vm.WorkTime,
                    Role = vm.Role,
                    Experience = vm.Experience,
                    DepartmentId = vm.DepartmentId,
                    EventId = vm.EventId
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            // ModelState geçerli değilse dropdownları tekrar doldur
            vm.Departments = _context.Departmens
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.DepartmentName
                })
                .ToList();

            vm.Events = _context.Events
                .Select(e => new SelectListItem
                {
                    Value = e.EventId.ToString(),
                    Text = e.EventName
                })
                .ToList();

            return RedirectToAction("Index");
        }
    
    }
}

