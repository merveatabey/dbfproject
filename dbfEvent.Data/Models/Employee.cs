using System;
using System.Collections.Generic;

namespace dbfEvent.Data.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public string? Address { get; set; }

    public DateTime? WorkTime { get; set; }

    public string? Role { get; set; }

    public int? Experience { get; set; }

    public int? EventId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Departmen? Department { get; set; }

    public virtual ICollection<EventEmployeeAssignment> EventEmployeeAssignments { get; set; } = new List<EventEmployeeAssignment>();
}
