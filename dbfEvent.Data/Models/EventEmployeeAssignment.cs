using System;
using System.Collections.Generic;

namespace dbfEvent.Data.Models;

public partial class EventEmployeeAssignment
{
    public int AssignmentId { get; set; }

    public int? EventId { get; set; }

    public int? EmployeeId { get; set; }
        
    public string? TaskDescription { get; set; }

    public DateTime? AssignmentDate { get; set; }

    public string? Status { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Event? Event { get; set; }
}
