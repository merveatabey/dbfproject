using System;
using System.Collections.Generic;

namespace dbfEvent.Data.Models;

public partial class Departmen
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
