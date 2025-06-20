using System;
using dbfEvent.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dbfEvent.Web.Models.ViewModel
{
	public class EmployeeCreateVM
	{
        public string? EmployeeName { get; set; }

        public string? Address { get; set; }

        public DateTime? WorkTime { get; set; }

        public string? Role { get; set; }

        public int? Experience { get; set; }

        // Seçilen departmanın ID'si
        public int? DepartmentId { get; set; }
        public int? EventId { get; set; }

        // Dropdown listesini tutacak property
        public IEnumerable<SelectListItem>? Departments { get; set; }
        public IEnumerable<SelectListItem>? Events { get; set; }
    }
}

